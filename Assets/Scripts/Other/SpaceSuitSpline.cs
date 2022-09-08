using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceSuitSpline : MonoBehaviour
{
    [SerializeField] SplineCreator creator;
    Transform playerTransform;
    private void Start()
    {
        playerTransform = ServiceProvider.Player.GetPlayerTransform();
    }
    private void Update()
    {
        if (ServiceProvider.Player.GetCurrentRoom().Data.IsSpace)
            creator.DrawSpline(playerTransform.position + Vector3.up, transform.position);
        else
            creator.DrawSpline(transform.position, transform.position);
    }
}
