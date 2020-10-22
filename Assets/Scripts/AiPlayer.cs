using Pathfinding;
using UnityEngine;


public class AiPlayer : Player
{   
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
            if(path.vectorPath.Count <= 4) {
                Vector2 q = new Vector2(path.vectorPath[3].x - ball.transform.position.x, path.vectorPath[3].y - ball.transform.position.y).normalized;
                ball.Shoot(Vector2.Distance(ball.transform.position, path.vectorPath[3]) * Random.Range(1.8f, 3f),q);
            } else {
                ball.ballColider.enabled = false;
                foreach (var item in path.vectorPath) {
                    Vector2 q = new Vector2(item.x - ball.transform.position.x, item.y - ball.transform.position.y).normalized;
                    float distance = Vector2.Distance(ball.transform.position, item);
                    RaycastHit2D hit = Physics2D.Raycast(new Vector2(ball.transform.position.x, ball.transform.position.y),q , distance);
                    if(hit.collider != null) {
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
                        ball.Shoot(Vector2.Distance(ball.transform.position, lastCorrectPoint) * Random.Range(2.4f, 5f),q);
                        ball.ballColider.enabled = true;
                        return;
                    }
                    lastCorrectPoint = item;
                }
            }
        }
    }
}