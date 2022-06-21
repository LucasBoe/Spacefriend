using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceSuitSpline : MonoBehaviour
{
    [SerializeField] SplineCreator creator;
    PlayerMode playerMode;
    Transform playerTransform;
    private void Start()
    {
        playerMode = PlayerServiceProvider.GetPlayerMode();
        playerTransform = PlayerServiceProvider.GetPlayerTransform();
    }
    private void Update()
    {
        if (playerMode.IsInSpace)
            creator.DrawSpline(playerTransform.position + Vector3.up, transform.position);
        else
            creator.DrawSpline(transform.position, transform.position);
    }
}
