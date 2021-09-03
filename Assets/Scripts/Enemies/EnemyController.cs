using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float turnAroundTime = 0;
    public float turnAroundInterval = 3;

    public float followRadius = 10;
    public float attackRadius = 2;
    Vector2 leftRight;
    bool goingRight;

    Animator anim;
    CharacterMotor motor;

    void Awake()
    {
        anim = GetComponent<Animator>();
        motor = GetComponent<CharacterMotor>();
        leftRight = Vector2.right;
        goingRight = true;
    }

    void Update()
    {
        if (PlayerController.player == null) return;

        Vector2 diff =  PlayerController.player.transform.position - transform.position;
        float xDist = Mathf.Abs(diff.x);
        if (xDist < attackRadius)
        {
            if (anim) anim.SetTrigger("sword");
            leftRight.x = 0;
        }
        else if(xDist < followRadius)
        {
            if (anim) anim.ResetTrigger("sword");
            UpdateChase(diff);
        }
        else
        {
            UpdateWander();
        }
        motor.Move(leftRight.x);

    }
    void UpdateWander()
    {
        if (turnAroundTime > turnAroundInterval)
        {
            goingRight = !goingRight;
            leftRight = goingRight ? Vector2.right : Vector2.left;
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
    
    void OnDrawGizmos ()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(followRadius*2, 1, 0));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(attackRadius*2, 1, 0));
    }
}
