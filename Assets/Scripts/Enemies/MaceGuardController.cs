using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceGuardController : MonoBehaviour
{
    Animator anim;
    CharacterMotor motor;
    public float attackRadius = 2;
    public float followRadius = 10;

    void Awake()
    {
        anim = GetComponent<Animator>();
        motor = GetComponent<CharacterMotor>();
    }

    void Update()
    {
        Vector2 diff =  PlayerController.player.transform.position - transform.position;
        float xDist = Mathf.Abs(diff.x);
        if (xDist < attackRadius)
        {
            if (anim) anim.SetTrigger("Attack");
        }
        else if(xDist < followRadius)
        {
            if (anim) anim.SetBool("EngagementRange", true);
            if (anim) anim.SetTrigger("Alerted");
            motor.Move(diff.normalized.x);
        }
        else
        {
            if (anim) anim.SetBool("EngagementRange", false);
        }
    }

    void OnDrawGizmos ()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(followRadius*2, 1, 0));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(attackRadius*2, 1, 0));
    }
}
