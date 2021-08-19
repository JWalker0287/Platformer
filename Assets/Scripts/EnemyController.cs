using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float turnAroundTime = 0;
    public float turnAroundInterval = 3;
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
        float distSq = diff.sqrMagnitude;
        if (distSq < 2)
        {
            if (anim) anim.SetTrigger("sword");
            leftRight.x = 0;
        }
        else if(distSq < 10)
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
}
