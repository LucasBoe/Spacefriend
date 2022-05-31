using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryItem_InteractionListener : MonoBehaviour, IInteractionListener
{
    [SerializeField] ItemData data;

    public void Interact()
    {
        PlayerServiceProvider.SetPlayerItemInHand(data, transform);
        Destroy(gameObject);
    }
}
