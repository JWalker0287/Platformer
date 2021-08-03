using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public Vector3 offset = new Vector3(0,1,-35);


    void Update()
    {

        transform.position = target.position + offset;


    }
}