using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 offSet = new Vector3(0,0,-5);
    public float lerpSpeed = 5;

    void Start()
    {
        transform.position = player.position + offSet;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + offSet, Time.deltaTime * lerpSpeed);
    }
}
