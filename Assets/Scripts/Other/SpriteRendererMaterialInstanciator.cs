using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteRendererMaterialInstanciator : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Material material = new Material(GetMaterialToInstatiate());
        spriteRenderer.material = material;
    }

    protected virtual Material GetMaterialToInstatiate()
    {
        return spriteRenderer.material;
    }
}
