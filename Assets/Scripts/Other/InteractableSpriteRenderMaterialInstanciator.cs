using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSpriteRenderMaterialInstanciator : SpriteRendererMaterialInstanciator
{
    protected override Material GetMaterialToInstatiate()
    {
        return GameReferenceHolder.Instance.InteractableMaterial;
    }
}
