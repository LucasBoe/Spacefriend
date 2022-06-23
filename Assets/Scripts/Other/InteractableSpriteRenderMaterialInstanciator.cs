using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class InteractableSpriteRenderMaterialInstanciator : MonoBehaviour
{
    [SerializeField] MaterialType materialType;

    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Material material = new Material(GetMaterialToInstatiate());
        spriteRenderer.material = material;
    }

    private Material GetMaterialToInstatiate()
    {
        if (materialType == MaterialType.Interactable)
            return GameReferenceHolder.Instance.InteractableMaterial;
        else
            return spriteRenderer.material;
    }

    private enum MaterialType
    {
        Interactable,
        Local,
    }
}
