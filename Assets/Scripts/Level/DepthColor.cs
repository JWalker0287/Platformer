using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DepthColor : MonoBehaviour
{
    [Tooltip("0 is near, 0.5 is player, 1 is far")] public Gradient colors = new Gradient();
    
#if UNITY_EDITOR
    void Update()
    {
        if (Application.isPlaying) return;

        Camera c = Camera.main;
        float z = transform.position.z;
        float farZ = c.transform.position.z + c.farClipPlane;
        float nearZ = c.transform.position.z + c.nearClipPlane;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = colors.Evaluate((z-nearZ)/(farZ-nearZ));
    }
#endif
}
