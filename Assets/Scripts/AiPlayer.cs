using Pathfinding;
using UnityEngine;

public class AiPlayer : Player
{   
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
            if(path.vectorPath.Count <= 4) {
                Vector2 vec = new Vector2(path.vectorPath[3].x - ball.transform.position.x, path.vectorPath[3].y - ball.transform.position.y).normalized;
                ball.Shoot(Vector2.Distance(ball.transform.position, path.vectorPath[3]) * Random.Range(1.8f, 3f),vec);
            } else {
                ball.ballColider.enabled = false;
                foreach (var item in path.vectorPath) {

                    // dla kazdego punku na trasie do dolka szuka najodleglejszej mozliwej kolizji
                    Vector2 q = new Vector2(item.x - ball.transform.position.x, item.y - ball.transform.position.y).normalized;
                    float distance = Vector2.Distance(ball.transform.position, item);
                    RaycastHit2D hit = Physics2D.Raycast(new Vector2(ball.transform.position.x, ball.transform.position.y),q , distance);

                    if(hit.collider != null) {
                        Debug.DrawLine(new Vector2(ball.transform.position.x, ball.transform.position.y), hit.point, Color.white, 10f);
                        Debug.DrawLine(hit.point, bounceDirection(q), Color.red, 10f);

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
                        
                        // jesli pilka znajduje sie na piasku, zwiksz sile udezenia
                        hit = Physics2D.Raycast(new Vector2(ball.transform.position.x, ball.transform.position.y),q , distance);
                        if(hit.collider.tag=="Sand") {
                            velocity *= 2;
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
            }
        }
    }

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