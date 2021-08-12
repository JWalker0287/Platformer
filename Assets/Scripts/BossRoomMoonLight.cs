using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomMoonLight : MonoBehaviour
{

    public float maxAlpha = 0.45f;

    SpriteRenderer spriteRenderer;
    Color color;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();        
        color = spriteRenderer.color;
        color.a = 0;
        spriteRenderer.color = color;
    }

    IEnumerator BrightenDarken()
    {
        for (float i = 0f; i < maxAlpha; i += Time.deltaTime)
        {
            color.a = i;
            spriteRenderer.color = color;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(5);

        for (float i = 0.50f; i >= 0f; i -= Time.deltaTime)
        {
            color.a = i;
            spriteRenderer.color = color;
            yield return new WaitForEndOfFrame();
        }
        
        color.a = 0;
        spriteRenderer.color = color;
        yield return new WaitForSeconds(5);
    }
}
