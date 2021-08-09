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

    public GameObject sword;

    public MagicController magic;

    public SwordController sDurability;

    public static PlayerController player;

    void Start()
    {
        
        body = GetComponent<Rigidbody2D>();
        fireball = GetComponentInChildren<ProjectileLauncher>();
        magic = GetComponent<MagicController>();
        sDurability = GetComponent<SwordController>();

        player = this;

        sword.SetActive(false);

    }

    void Update()
    {
        GroundCheck();
        float x = Input.GetAxis("Horizontal");
        if (x != 0) transform.right = Vector2.right * x;

        body.velocity = new Vector2(x * speed, body.velocity.y);

        if (onGround && Input.GetButtonDown("Jump"))
        {
            float jumpVelocity = Mathf.Sqrt(-2 * Physics2D.gravity.y * body.gravityScale * jumpHeight);
            body.velocity = new Vector2(body.velocity.x, jumpVelocity);
        }

        if (Input.GetButtonDown("Fire2") && magic.mana > 0 && fireball.Shoot(fireball.transform.right) > 0) 
        {
        
            fireball.Shoot(fireball.transform.right);

            magic.UsedMagic();
            
        }
        
        if (Input.GetButtonDown("Fire1") && sDurability.durability > 0) 
        {
        
            StartCoroutine("SwingSword");
            
        }

        //Debug.Log(magic.mana);
        // Debug.Log(sDurability.durability);

    }

    IEnumerator SwingSword()
    {

        sword.SetActive(true);

        sDurability.UsedSword();

        yield return new WaitForSeconds(0.5f);

        sword.SetActive(false);

    }

    void GroundCheck ()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.05f, envLayer);
        onGround = (colliders.Length > 0);
    }
}
