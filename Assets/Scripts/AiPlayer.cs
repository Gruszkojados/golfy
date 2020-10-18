using Pathfinding;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AiPlayer : Player
{   
    public override void StartTurn() {
        base.StartTurn();
        
        AstarPath.active.Scan();
        var hole = ball.gameObject.transform.GetChild(0).position;
        var i = ball.transform.position;
        ball.seeker.StartPath(i, hole, OnPathComplete);
    }

    public IEnumerator botWait() {
        yield return new WaitForSeconds(1);
        
    }
    
    public void OnPathComplete (Path p) {
        if (p.error) {
            Debug.Log("No path: " + p.errorLog);
        } else {
            Path path = ball.seeker.GetCurrentPath();
            
            Vector3 lastCorrectPoint = new Vector3(1,1,0);
            if(path.vectorPath.Count <= 4) {
                Quaternion q = new Quaternion(path.vectorPath[3].x - ball.transform.position.x, path.vectorPath[3].y - ball.transform.position.y, 0 , 0).normalized;
                ball.Shoot(Vector2.Distance(ball.transform.position, path.vectorPath[3]) * Random.Range(1.8f, 3f), new Vector2(q.x, q.y));
            } else {
                ball.ballColider.enabled = false;
                foreach (var item in path.vectorPath) {
                    Quaternion q = new Quaternion(item.x - ball.transform.position.x, item.y - ball.transform.position.y, 0 , 0).normalized;
                    float distance = Vector2.Distance(ball.transform.position, item);
                    RaycastHit2D hit = Physics2D.Raycast(new Vector2(ball.transform.position.x, ball.transform.position.y), new Vector2(q.x, q.y), distance);
                    if(hit.collider != null) {
                        q = new Quaternion(lastCorrectPoint.x - ball.transform.position.x, lastCorrectPoint.y - ball.transform.position.y, 0 , 0).normalized;
                        
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
                        ball.Shoot(Vector2.Distance(ball.transform.position, lastCorrectPoint) * Random.Range(2.4f, 5f), new Vector2(q.x, q.y));
                        ball.ballColider.enabled = true;
                        return;
                    }
                    lastCorrectPoint = item;
                }
            }
        }
    }
}