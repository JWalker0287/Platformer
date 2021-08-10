using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D body;
    public float turnAroundTime = 0;
    public float turnAroundInterval = 3;
    public float speed = 2;
    Vector2 leftRight;
    bool goingRight;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        leftRight = Vector2.right;
        goingRight = true;
    }

    void Update()
    {
        if (PlayerController.player == null) return;
        
        Vector2 diff =  PlayerController.player.transform.position - transform.position;
        if(Mathf.Abs(diff.sqrMagnitude) < 10)
        {
            UpdateChase(diff);
        }
        else
        {
            UpdateWander();
        }
        body.velocity = new Vector2(speed * leftRight.x, body.velocity.y);

    }
    void UpdateWander()
    {
        if (turnAroundTime > turnAroundInterval)
        {
            if(goingRight)
            {
                leftRight = Vector2.left;
                transform.right = Vector2.left;
                goingRight = false;
            }
            else
            {
                leftRight = Vector2.right;
                transform.right = Vector2.right;
                goingRight = true;
            }
            turnAroundTime = 0;
        }
        turnAroundTime += Time.deltaTime;
    }
    void UpdateChase(Vector2 d)
    {
        d = d.normalized;
        transform.right = new Vector2(d.x,0);
        leftRight = new Vector2(d.x,0); 
    }
}
