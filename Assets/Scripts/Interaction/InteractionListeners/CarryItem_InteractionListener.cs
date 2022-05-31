using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryItem_InteractionListener : MonoBehaviour, IInteractionListener
{
    [SerializeField] ItemData data;
    [SerializeField] bool disableInsteadOfDestroy = false;

    public void Interact()
    {
        PlayerServiceProvider.CollectItemToHand(data, transform);

        if (disableInsteadOfDestroy)
            gameObject.SetActive(false);
        else
            Destroy(gameObject);
    }
}
