using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAwayEnemyController : MonoBehaviour
{
    public float alertDistance = 10;
    public float jumpAwayDistance = 3;

    float attackStart;

    CharacterMotor motor;
    Animator anim;
    void Awake ()
    {
        motor = GetComponent<CharacterMotor>();
        anim = GetComponent<Animator>();
    }

    void Update ()
    {
        Vector3 diff = PlayerController.player.transform.position - transform.position;
        if (motor.onGround) motor.Move(0);
        else motor.Move(-Mathf.Sign(diff.x));

        bool attacking = anim.GetBool("attacking");

        float distSq = diff.sqrMagnitude;
        if (attacking)
        {
            if (Time.time - attackStart > 3) anim.SetBool("attacking", false);
        }
        else if (distSq < jumpAwayDistance * jumpAwayDistance)
        {
            if (motor.onGround)
            {
                if (Random.value < 0.75f) motor.Jump();
                else 
                {
                    attackStart = Time.time;
                    anim.SetBool("attacking", true);
                }
            }
        }
        else if (distSq < alertDistance * alertDistance)
        {
            anim.SetBool("alert", true);
        }
        else
        {
            anim.SetBool("alert", false);
        }
    }
}
