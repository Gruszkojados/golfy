using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class AiPlayer : Player
{   
    public static event System.Action SwitchColider = () => {};
    float maxVelocity = 115f;
    int exceptionCount = 0;
    struct directionStruct {
        public Vector2 dir;
        public Vector2 wallHit;
        public Vector2 secWallHit;
        public float cost;
        public directionStruct(Vector2 dir, Vector2 wallHit, Vector2 secWallHit, float cost) {
            this.dir = dir;
            this.wallHit = wallHit;
            this.secWallHit = secWallHit;
            this.cost = cost;
        }
    }
    public override void StartTurn() {
        base.StartTurn();
        AstarPath.active.Scan();
        var hole = ball.gameObject.transform.GetChild(0).position;
        var i = ball.transform.position;
        ball.seeker.StartPath(i, hole, OnPathComplete);
    }
    public void OnPathComplete (Path p) {
        if (p.error) {
            Debug.Log("No path: " + p.errorLog);
        } else {
            Vector2 ballPosition = ball.transform.position;
            bool isBounce = false;
            Path path = ball.seeker.GetCurrentPath();
            Vector3 lastCorrectPoint = new Vector3(1,1,0);
            if(path.vectorPath.Count <= 4) {
                Vector2 vec = new Vector2(path.vectorPath[1].x - ballPosition.x, path.vectorPath[1].y - ballPosition.y).normalized;
                ball.Shoot(Vector2.Distance(ballPosition, path.vectorPath[1]) * Random.Range(2f, 3f),vec);
            } else {
                try {
                    exceptionCount = 0;
                    
                    SwitchColider.Invoke();
                    ball.ballColider.enabled = false;
                    foreach (var item in path.vectorPath) {

                        // For all points, search for the farthest seen point 
                        Vector2 finalDirection = new Vector2(item.x - ballPosition.x, item.y - ballPosition.y).normalized;
                        float distance = Vector2.Distance(ballPosition, item);
                        RaycastHit2D hit = Physics2D.CircleCast(ballPosition, 0.93f, finalDirection, distance);
                        //Debug.DrawLine(ballPosition, item, Color.red, 100f);

                        if(hit.collider != null) {
                            
                            if(path.vectorPath.IndexOf(item)+2<path.vectorPath.Count) {
                                Vector2 position = path.vectorPath[path.vectorPath.IndexOf(item)+2];
                            
                            
                                ball.targetBlock.SetPosition(position);
                                ball.targetBlock.ChangeStatus();


                                int RaysToShoot = 720;
                                float angle = 0;
                                List<directionStruct> directionsList = new List<directionStruct>();
                                List<directionStruct> secondDirectionsList = new List<directionStruct>();
                                for (int i=0; i<RaysToShoot; i++) {
                                    float x = Mathf.Sin(angle);
                                    float y = Mathf.Cos(angle);
                                    angle +=  2 * Mathf.PI / RaysToShoot;

                                    Vector2 direction = new Vector2 (x, y);
                                    RaycastHit2D wallHit = Physics2D.CircleCast(ballPosition, 0.93f, direction, 100f);
                                    Vector2 reflectDirection = Vector2.Reflect(direction, wallHit.normal);
                                    RaycastHit2D reflectHit = Physics2D.Raycast(wallHit.point, reflectDirection, 100f);
                                    
                                    if(reflectHit.collider != null) {
                                        if(reflectHit.collider.tag=="BallTarget") {
                                            //Debug.DrawLine(ballPosition, wallHit.point, Color.yellow, 10f);
                                            //Debug.DrawLine(wallHit.point, reflectHit.point, Color.blue, 10f);
                                            if(wallHit.collider.tag!="BallTarget") {
                                                float cost = Vector2.Distance(ballPosition, wallHit.point) + Vector2.Distance(wallHit.point, reflectHit.point);
                                                directionStruct dirCost = new directionStruct(direction.normalized, wallHit.point, reflectHit.point, cost);
                                                directionsList.Add(dirCost);
                                            }
                                        }
                                    }
                                    Vector2 secondReflectDirection = Vector2.Reflect(reflectDirection, reflectHit.normal);
                                    RaycastHit2D secondReflectHit = Physics2D.Raycast(reflectHit.point, secondReflectDirection, 100f);
                                    if(secondReflectHit.collider != null) {
                                        if(secondReflectHit.collider.tag=="BallTarget") {
                                            //Debug.DrawLine(ballPosition, wallHit.point, Color.yellow, 10f);
                                            //Debug.DrawLine(wallHit.point, reflectHit.point, Color.blue, 10f);
                                            //Debug.DrawLine(reflectHit.point, secondReflectHit.point, Color.red, 10f);
                                            if(wallHit.collider.tag!="BallTarget") {
                                                float cost = Vector2.Distance(ballPosition, wallHit.point) 
                                                    + Vector2.Distance(wallHit.point, reflectHit.point)
                                                    + Vector2.Distance(reflectHit.point, secondReflectHit.point);
                                                directionStruct dirCost = new directionStruct(direction.normalized, wallHit.point, secondReflectHit.point, cost);
                                                directionsList.Add(dirCost);
                                            }
                                        }
                                    }
                                }

                                if(directionsList.Count > 0) {
                                    directionStruct costTemp = directionsList[0];
                                    foreach (directionStruct dir in directionsList)
                                    {   
                                        if(costTemp.cost>dir.cost) {
                                            costTemp = dir;
                                        }
                                    }
                                    //Debug.DrawLine(ballPosition, costTemp.wallHit, Color.black, 10f);
                                    Debug.DrawLine(costTemp.wallHit, costTemp.secWallHit, Color.black, 10f);
                                    isBounce = true;
                                    if(costTemp.cost < 60) {
                                        finalDirection = costTemp.dir;
                                    } else {
                                        finalDirection = new Vector2(lastCorrectPoint.x - ballPosition.x, lastCorrectPoint.y - ballPosition.y).normalized;
                                    }
                                    
                                } else {
                                    finalDirection = new Vector2(lastCorrectPoint.x - ballPosition.x, lastCorrectPoint.y - ballPosition.y).normalized;
                                }
                                ball.targetBlock.ChangeStatus();
                            }
                            // Set shoot direction by using last seen poit
                            
                            finalDirection = randomizeDirection(finalDirection);
                            Debug.DrawRay(ballPosition, finalDirection*30, Color.red, 10f);
                            // Seting shoot force with random fault
                            float velocity = 0;
                            if(isBounce) {
                                velocity = maxVelocity;
                            } else {
                                velocity = Vector2.Distance(ballPosition, lastCorrectPoint) * Random.Range(2.4f, 5f);
                            }
                            
                            // If ball standing on snad or ramp, then change force to max
                            SwitchColider.Invoke();
                            hit = Physics2D.Raycast(new Vector2(ballPosition.x, ballPosition.y),finalDirection , distance);
                            if(hit.collider!=null && (hit.collider.tag=="Sand" || hit.collider.tag=="Ramp")) {
                                velocity = maxVelocity;
                            }

                            if(velocity > maxVelocity) {
                                velocity = maxVelocity;
                            }

                            ball.ballColider.enabled = true;
                            ball.Shoot(velocity, finalDirection);
                            return;
                        }
                        lastCorrectPoint = item;
                        if(hit.collider == null && path.vectorPath.Count==path.vectorPath.IndexOf(item)+1) {
                            ball.ballColider.enabled = true;
                            ball.Shoot(maxVelocity, finalDirection);
                        }
                    }
                } catch (System.Exception e) {
                    exceptionCount++;
                    if(exceptionCount > 10) {
                        Debug.Log("ERROR: " + e.Message + " " + e.StackTrace);
                    } else {
                        Debug.Log("Ai error number: " + exceptionCount);
                        OnPathComplete(path);
                        throw;
                    }
                }
            }
        }
    }
    Vector2 randomizeDirection(Vector2 finalDirection) { // Adding random direction fault
        float skaleOfFails = 0.00095f;
        float random = Random.Range(1, 100) * skaleOfFails;
        if(finalDirection.x > 0) {
            finalDirection.x += random;
        } else {
            finalDirection.x += -random;
        }
        if(finalDirection.y > 0) {
            finalDirection.y += random;
        } else {
            finalDirection.y += -random;
        }
        return finalDirection;
    }
}