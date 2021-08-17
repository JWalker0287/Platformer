using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    public float speed = 2;
    public float jumpHeight = 3;
    public LayerMask envLayer;
    bool onGround;
    Rigidbody2D body;
    Animator anim;
    float xInput = 0;

    void Awake ()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void Move(float x)
    {
        xInput = x;
    }

    public void Jump()
    {
        if (onGround)
        {
            anim.SetTrigger("jump");
            float jumpVelocity = Mathf.Sqrt(-2 * Physics2D.gravity.y * body.gravityScale * jumpHeight);
            body.velocity = new Vector2(body.velocity.x, jumpVelocity);
        }
    }
    
    void Update ()
    {
        GroundCheck();

        if (xInput != 0) transform.right = Vector2.right * xInput;

        body.velocity = new Vector2(xInput * speed, body.velocity.y);

        anim.SetFloat("speed", Mathf.Abs(body.velocity.x));
        anim.SetFloat("yVelocity", body.velocity.y);
        anim.SetBool("onGround", onGround);
    }

    void GroundCheck ()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.05f, envLayer);
        onGround = (colliders.Length > 0);
    }
}
