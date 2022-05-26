using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRendererMaterialInstanciator))]
public class RoomSpriteRenderer : MonoBehaviour
{
    Material material;
    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }
    public void SetAlpha(float alpha)
    {
        Color c = material.color;
        c.a = alpha;
        material.color = c;
    }
}
