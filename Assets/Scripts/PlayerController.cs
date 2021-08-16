using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paraphernalia.Components;

public class PlayerController : MonoBehaviour
{
    public static PlayerController player;
    
    public GameObject sword;

    public GameObject gameManager;

    public float speed = 2;
    public float jumpHeight = 3;
    public LayerMask envLayer;
    public LayerMask interactLayer;

    bool inLight;
    bool onGround;

    Rigidbody2D body;
    ProjectileLauncher fireball;
    MagicController magic;
    SwordController sDurability;
    HealthController health;
    Animator anim;

    public Vector3 defaultPosition;
    
    void Awake ()
    {
        defaultPosition = transform.position;
        if (player == null) 
        {
            player = this;
            DontDestroyOnLoad(gameObject);
            health = GetComponent<HealthController>();
            body = GetComponent<Rigidbody2D>();
            fireball = GetComponentInChildren<ProjectileLauncher>();
            magic = GetComponent<MagicController>();
            sDurability = GetComponent<SwordController>();
            anim = GetComponent<Animator>();
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    void OnEnable ()
    {
        if (player != this) return;
        health.onDeath += GameManager.GameOver;
    }   

    void OnDisable ()
    {
        if (player != this) return;
        health.onDeath -= GameManager.GameOver;
    }

    public static void Revive ()
    {
        player.health.health = player.health.maxHealth;
        player.gameObject.SetActive(true);
    }

    void InteractCheck ()
    {
        if (!Input.GetButtonDown("Submit")) return;

        Collider2D c = Physics2D.OverlapCircle(transform.position, 0.5f, interactLayer);
        if (c != null)
        {
            Interactable i = c.GetComponent<Interactable>();
            if (i.CanInteract()) i.Interact();
        }
    }

    void Update()
    {
        InteractCheck();
        GroundCheck();
        float x = Input.GetAxis("Horizontal");
        if (x != 0) transform.right = Vector2.right * x;

        body.velocity = new Vector2(x * speed, body.velocity.y);

        if (onGround && Input.GetButtonDown("Jump"))
        {
            float jumpVelocity = Mathf.Sqrt(-2 * Physics2D.gravity.y * body.gravityScale * jumpHeight);
            body.velocity = new Vector2(body.velocity.x, jumpVelocity);
            AudioManager.PlayEffect("Jump");
        }

        if (Input.GetButtonDown("Fire2") && magic.mana > 0 && fireball.Shoot(fireball.transform.right) > 0) 
        {
        
            fireball.Shoot(fireball.transform.right);

            magic.UsedMagic();
            
        }
        
        if (Input.GetButtonDown("Fire1") && sDurability.durability > 0) 
        {
        
            sDurability.UsedSword();
            anim.SetTrigger("sword");

            AudioManager.PlayEffect("SwordWoosh");
            
        }

        //Debug.Log(magic.mana);
        // Debug.Log(sDurability.durability);

        anim.SetFloat("speed", Mathf.Abs(body.velocity.x));

    }

    void GroundCheck ()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.05f, envLayer);
        onGround = (colliders.Length > 0);
    }
}
