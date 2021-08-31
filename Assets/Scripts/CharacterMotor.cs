using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paraphernalia.Components;

public class CharacterMotor : MonoBehaviour
{
    public float speed = 2;
    public float jumpHeight = 3;
    public LayerMask envLayer;
    bool _onGround;
    public bool onGround {
        get { return _onGround; }
        private set { _onGround = value; }
    }

    [Tooltip("Does the character point to the right (1) or left (-1) normally")]
    [Range(-1,1)] public int naturalDirection = 1;

    Rigidbody2D body;
    Animator anim;
    float xInput = 0;
    public string jumpPuffParticles = "JumpPuff";
    public string jumpSmearParticles = "Smear";

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
        if (!onGround) return;

        anim.SetTrigger("jump");
        ParticleManager.Play(jumpPuffParticles, transform.position);
        float jumpVelocity = Mathf.Sqrt(-2 * Physics2D.gravity.y * body.gravityScale * jumpHeight);
        body.velocity = new Vector2(body.velocity.x, jumpVelocity);
        ParticleManager.Play(jumpSmearParticles, transform.position, body.velocity);
    }

    public void CancelJump()
    {
        if (onGround) return;

        float jumpVelocity = Mathf.Sqrt(-2 * Physics2D.gravity.y * body.gravityScale * 0.5f);
        if (body.velocity.y > jumpVelocity) body.velocity = new Vector2(body.velocity.x, jumpVelocity);
    }
    
    void Update ()
    {
        GroundCheck();

        if (xInput != 0) transform.right = Vector2.right * xInput * naturalDirection;

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
