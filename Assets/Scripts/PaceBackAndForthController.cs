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
    }
}
