using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paraphernalia.Components;

public class FlyingLancerController : MonoBehaviour
{
    public float idleSpeed = 2;
    public float idleRange = 5;
    public float dashSpeed = 15;
    public float followRadius = 10;
    public float interval = 1;
    public float throwOffset = 4;
    Rigidbody2D body;
    Animator anim;
    CircleCollider2D circleCollider;
    ProjectileLauncher launcher;

    void Awake ()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();
        launcher = GetComponentInChildren<ProjectileLauncher>();
    }

    public void ThrowLance ()
    {
        launcher.Shoot(launcher.transform.right, body.velocity);
    }

    IEnumerator Start ()
    {
        Vector2 pos = transform.position;
        while (enabled)
        {
            Vector2 targetPos = pos + Random.insideUnitCircle * idleRange;
            for (float t = 0; t <= interval; t += Time.deltaTime)
            {
                Vector2 dir = targetPos - (Vector2)transform.position;
                dir.Normalize();
                body.velocity = Vector2.Lerp(body.velocity, dir * idleSpeed, Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            
            Vector3 diff = PlayerController.player.transform.position - transform.position - Vector3.up;
            float sign = Mathf.Sign(diff.x);
            if (diff.sqrMagnitude < followRadius * followRadius)
            {
                transform.right = Vector3.right * sign;
                pos = (Vector2)PlayerController.player.transform.position - Vector2.right * sign * throwOffset;
                if (Random.value < 0.75f) pos += Vector2.up * throwOffset;
            }

            if (Mathf.Abs(diff.y) < 1 && Mathf.Abs(diff.x) < followRadius * 0.5f)
            {
                anim.SetTrigger("dash");
                yield return new WaitForSeconds(0.3f);
                body.velocity = Vector2.right * sign * dashSpeed;
                ParticleManager.Play("Dash", transform.position, body.velocity);
                yield return new WaitForSeconds(1.5f);
            }
            
            diff = (Vector3)pos - transform.position;
            if (diff.sqrMagnitude < 1)
            {
                anim.SetTrigger("throw");
                yield return new WaitForSeconds(1);
            }
        }
    }


    void OnDrawGizmos ()
    {
        if (circleCollider == null) circleCollider = GetComponent<CircleCollider2D>();

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + (Vector3)circleCollider.offset, idleRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (Vector3)circleCollider.offset, followRadius);
    }
}
