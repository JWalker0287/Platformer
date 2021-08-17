using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class SyncBoxCollider : MonoBehaviour
{

#if UNITY_EDITOR
    public float inset = 0.1f;

    void Update()
    {
        if (Application.isPlaying) return;

        BoxCollider2D box = GetComponent<BoxCollider2D>();
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        Vector2 targetSize = sprite.size - Vector2.one * inset;
        if (box.size != targetSize) 
        {
            box.size = targetSize;
        }
    }
#endif
}