using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceGuardController : MonoBehaviour
{
    Animator anim;
    CharacterMotor motor;
    public float followRadius = 10;

    void Awake()
    {
        anim = GetComponent<Animator>();
        motor = GetComponent<CharacterMotor>();
    }

    void Update()
    {
        Vector2 diff =  PlayerController.player.transform.position - transform.position;
        float distSq = diff.sqrMagnitude;
        if (distSq < 2)
        {
            if (anim) anim.SetTrigger("Attack");
        }
        else if(distSq < 10)
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
}
