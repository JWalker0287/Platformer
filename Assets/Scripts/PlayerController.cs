using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController player;
    
    public LayerMask interactLayer;

    bool inLight;

    CharacterMotor motor;
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
    }
}
