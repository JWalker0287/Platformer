using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaceBackAndForthController : MonoBehaviour
{
    CharacterMotor motor;
    void Awake ()
    {
        motor = GetComponent<CharacterMotor>();
    }
    
    void Update()
    {
        motor.Move(transform.right.x * motor.naturalDirection);

        Collider2D c = Physics2D.OverlapPoint(
            transform.position + 
            transform.right * motor.naturalDirection * 0.5f - 
            new Vector3(0, 0.1f, 0)
        );
        if (c == null) TurnAround(); 
    }

    void OnCollisionEnter2D (Collision2D c) 
    {
        foreach (ContactPoint2D contact in c.contacts)
        {
            if (Vector2.Dot(transform.right * motor.naturalDirection, contact.normal) < -0.707f)
            {
                TurnAround();
            }
        }
    }

    void TurnAround ()
    {
        transform.right = -transform.right;
        motor.Move(transform.right.x * motor.naturalDirection);
    }
}
