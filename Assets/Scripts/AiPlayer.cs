using Pathfinding;
using UnityEngine;

public class AiPlayer : Player
{   
    public static event System.Action SwitchColider = () => {};
    float maxVelocity = 115f;
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
            
            Path path = ball.seeker.GetCurrentPath();
            Vector3 lastCorrectPoint = new Vector3(1,1,0);
            if(path.vectorPath.Count <= 2) {
                Vector2 vec = new Vector2(path.vectorPath[1].x - ball.transform.position.x, path.vectorPath[1].y - ball.transform.position.y).normalized;
                ball.Shoot(Vector2.Distance(ball.transform.position, path.vectorPath[3]) * Random.Range(1.8f, 3f),vec);
            } else {
                
                SwitchColider.Invoke();
                ball.ballColider.enabled = false;
                foreach (var item in path.vectorPath) {

                    // dla kazdego punku na trasie do dolka szuka najodleglejszej mozliwej kolizji
                    Vector2 q = new Vector2(item.x - ball.transform.position.x, item.y - ball.transform.position.y).normalized;
                    float distance = Vector2.Distance(ball.transform.position, item);
                    RaycastHit2D hit = Physics2D.Raycast(new Vector2(ball.transform.position.x, ball.transform.position.y),q , distance);
                    
                    if(hit.collider != null) {
                        Debug.Log("Ball angle: " + Vector2.Angle(new Vector2(ball.transform.position.x, ball.transform.position.y), hit.point));
                        Debug.DrawLine(new Vector2(ball.transform.position.x, ball.transform.position.y), hit.point, Color.white, 10f);
                        Vector2 bounceDir = bounceDirection(q);
                        Debug.DrawLine(hit.point, bounceDir, Color.red, 10f);
                        // Debug.DrawLine(hit.point, bounceDir, Color.red, 10f);
                        // Debug.DrawLine(hit.point, bounceDir, Color.red, 10f);
                       
                        // Vector2.Angle()
                        // Vector3 rayPosition = new Vector3(transform.position.x, headHeight, transform.position.z);
                        // Vector2 leftRayRotation = Vector2.AngleAxis(10, hit.point);
                        // Vector3 rightRayRotation = Quaternion.AngleAxis(fovAngle, transform.up) * transform.forward;
                        
                        // //Constructing rays
                        // Ray rayCenter = new Ray(rayPosition, transform.forward);
                        // Ray rayLeft = new Ray(hit.point, leftRayRotation);
                        // Ray rayRight = new Ray(rayPosition, rightRayRotation);

                        // ustawia kierunek uderzenia, uwzgledaniajac losowy blad celuje w punkt ostatniego "widzianego przez pilke" miejsca bez kolizji
                        q = new Vector2(lastCorrectPoint.x - ball.transform.position.x, lastCorrectPoint.y - ball.transform.position.y).normalized;
                        float skaleOfFails = 0.001f;
                        float random = Random.Range(1, 100) * skaleOfFails;
                        if(q.x > 0) {
                            q.x += random;
                        } else {
                            q.x += -random;
                        }
                        if(q.y > 0) {
                            q.y += random;
                        } else {
                            q.y += -random;
                        }

                        // ustawia sile uderzenia, uwzgledaniajac losowy blad
                        float velocity = Vector2.Distance(ball.transform.position, lastCorrectPoint) * Random.Range(2.4f, 5f);
                        
                        // jesli pilka znajduje sie na piasku, ustaw sile udezenia na maksymalna
                        SwitchColider.Invoke();
                        hit = Physics2D.Raycast(new Vector2(ball.transform.position.x, ball.transform.position.y),q , distance);
                        if(hit.collider!=null && (hit.collider.tag=="Sand" || hit.collider.tag=="Ramp")) {
                            velocity = maxVelocity;
                        }

                        // jesli wartosc predkosci przekroczy wartosc max dla gracza, ustaw maksymalna wartosc na sztywno
                        if(velocity > maxVelocity) {
                            velocity = maxVelocity;
                        }

                        ball.Shoot(velocity, q);
                        ball.ballColider.enabled = true;
                        return;
                    }
                    lastCorrectPoint = item;
                }
                ShootAnyway(lastCorrectPoint);
            }
        }
    }

    void ShootAnyway(Vector2 lastCorrectPoint) {
        float distance = Vector2.Distance(ball.transform.position, lastCorrectPoint);
        Vector2 q = new Vector2(lastCorrectPoint.x - ball.transform.position.x, lastCorrectPoint.y - ball.transform.position.y).normalized;
        float skaleOfFails = 0.001f;
        float random = Random.Range(1, 100) * skaleOfFails;
        if(q.x > 0) {
            q.x += random;
        } else {
            q.x += -random;
        }
        if(q.y > 0) {
            q.y += random;
        } else {
            q.y += -random;
        }
        float velocity = Vector2.Distance(ball.transform.position, lastCorrectPoint) * Random.Range(2.4f, 5f);
        SwitchColider.Invoke();
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(ball.transform.position.x, ball.transform.position.y),q , distance);
        if(hit.collider!=null && (hit.collider.tag=="Sand" || hit.collider.tag=="Ramp")) {
            velocity = maxVelocity;
        }
        if(velocity > maxVelocity) {
            velocity = maxVelocity;
        }
        ball.Shoot(velocity, q);
        ball.ballColider.enabled = true;
        return;
    }

    // bot strzelajacy w badny? TODO moze sie cos wymysli 
    Vector2 bounceDirection(Vector2 vec) {
        if(vec.x>0 && vec.y>0) {
            vec.y = -vec.y;
        } else if (vec.x>0 && vec.y<0) {
            vec.x = -vec.x;
        } else if (vec.x<0 && vec.y<0) {
            vec.y = -vec.y;
        } else if (vec.x<0 && vec.y>0) {
            vec.x = -vec.x;
        }
        return vec;
    }
}