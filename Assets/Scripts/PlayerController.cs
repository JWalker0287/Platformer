using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2;
    public float jumpHeight = 3;
    public LayerMask envLayer;

    bool inLight;

    bool onGround;
    Rigidbody2D body;
    ProjectileLauncher fireball;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        fireball = GetComponentInChildren<ProjectileLauncher>();

    }

    void Update()
    {
        if (Time.timeScale == 0) return;
        
        GroundCheck();
        float x = Input.GetAxis("Horizontal");
        if (x != 0) transform.right = Vector2.right * x;

        body.velocity = new Vector2(x * speed, body.velocity.y);

        if (onGround && Input.GetButtonDown("Jump"))
        {
            float jumpVelocity = Mathf.Sqrt(-2 * Physics2D.gravity.y * body.gravityScale * jumpHeight);
            body.velocity = new Vector2(body.velocity.x, jumpVelocity);
        }

        if (Input.GetButton("Fire1") && fireball.Shoot(fireball.transform.right) > 0)
        {
            // do stuff if gun fires
        }

    }

    void GroundCheck ()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.05f, envLayer);
        onGround = (colliders.Length > 0);
    }
}
