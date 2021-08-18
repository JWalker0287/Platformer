using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController player;
    
    public LayerMask interactLayer;
    public ParticleSystem dustTrailParticles;

    bool inLight;

    CharacterMotor motor;
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
            motor = GetComponent<CharacterMotor>();
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
        float x = Input.GetAxis("Horizontal");
        motor.Move(x);
        if (Input.GetButtonDown("Jump")) motor.Jump();

        if (Input.GetButtonDown("Fire2") && magic.mana > 0 && fireball.Shoot(fireball.transform.right) > 0) 
        {
            fireball.Shoot(fireball.transform.right);
            magic.UsedMagic();
            anim.SetTrigger("magic");
        }
        
        if (Input.GetButtonDown("Fire1") && sDurability.durability > 0) 
        {
            sDurability.UsedSword();
            anim.SetTrigger("sword");
        }

        if ((motor.onGround && Mathf.Abs(body.velocity.x) > 2) || body.velocity.y > 0.5f)
        {
            if (dustTrailParticles.isStopped) dustTrailParticles.Play();
        }
        else if (dustTrailParticles.isPlaying) dustTrailParticles.Stop();
    }
}
