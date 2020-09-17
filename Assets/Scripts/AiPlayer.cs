using Pathfinding;
using UnityEngine;

public class AiPlayer : Player
{   
    public override void StartTurn() {
        base.StartTurn();
        
        var hole = ball.gameObject.transform.GetChild(0).position;
        var i = ball.transform.position;
        ball.seeker.StartPath (i, hole, OnPathComplete);
    }

    public void OnPathComplete (Path p) {
        if (p.error) {
            Debug.Log("No path");
        } else {
                
            // ball.Shoot(Random.Range(50,200), new Vector2(path.path[0].position.x, path.path[0].position.y));

            // Debug.Log("Before");
            // Debug.Log("x: " + path.vectorPath[10].x + " y: " + path.vectorPath[10].y);


            // Debug.Log("After");
            // Debug.Log("x: " + q.x + " y: " + q.y);
            // ball.Shoot(Random.Range(50,200), new Vector2(q.x, q.y));
 
            Path path = ball.seeker.GetCurrentPath();
            // Physics2D.Raycast()
            int licznik = 0;
            ball.ballColider.enabled = false;
            Vector3 lastCorrectPoint = new Vector3(1,1,0);
            foreach (var item in path.vectorPath) {
                Quaternion q = new Quaternion(item.x - ball.transform.position.x, item.y - ball.transform.position.y, 0 , 0);
                q = q.normalized;
                licznik++;
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(ball.transform.position.x, ball.transform.position.y), new Vector2(q.x, q.y), Vector2.Distance(ball.transform.position, item));
                if(hit.collider != null) {
                    q = new Quaternion(lastCorrectPoint.x - ball.transform.position.x, lastCorrectPoint.y - ball.transform.position.y, 0 , 0);
                    q = q.normalized;

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
                    
                    ball.Shoot(Vector2.Distance(ball.transform.position, lastCorrectPoint) * Random.Range(1.5f, 2.5f), new Vector2(q.x, q.y));
                    ball.ballColider.enabled = true;
                    return;
                }
                lastCorrectPoint = item;
            } 
        }
    }
}