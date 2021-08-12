using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class SyncBoxCollider : MonoBehaviour
{

#if UNITY_EDITOR
    void Update()
    {
        if (Application.isPlaying) return;

        BoxCollider2D box = GetComponent<BoxCollider2D>();
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        if (box.size != sprite.size) 
        {
            box.size = sprite.size;
        }
    }
#endif
}