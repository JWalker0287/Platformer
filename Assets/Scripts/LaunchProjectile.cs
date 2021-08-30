using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paraphernalia.Components;

public class LaunchProjectile : MonoBehaviour
{

    ProjectileLauncher gun;
    void Awake ()
    {
        gun = GetComponentInChildren<ProjectileLauncher>();
    }

    public void Fire ()
    {
        gun.Shoot(gun.transform.right);
    }
}
