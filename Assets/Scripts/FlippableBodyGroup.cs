using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippableBodyGroup : MonoBehaviour
{
    
    void Start ()
    {
        if (transform.localRotation.y != 0) Flip();
    }

    [ContextMenu("Flip")]
    void Flip()
    {
        for (int i = transform.childCount-1; i >= 0; i--)
        {
            Transform t = transform.GetChild(i);
            transform.SetParent(null);
            t.Rotate(Vector3.up * 180);
            SpriteRenderer sprite = t.GetComponent<SpriteRenderer>();
            sprite.flipX = !sprite.flipX;
            PolygonCollider2D poly = t.GetComponent<PolygonCollider2D>();
            Vector2[] points = poly.points;
            for (int j = 0; j < points.Length; j++)
            {
                points[j] = new Vector2(-points[j].x, points[j].y);
            }
            poly.points = points;
        }
    }
}
