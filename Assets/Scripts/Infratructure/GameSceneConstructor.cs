using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneConstructor : MonoBehaviour
{
    public GameObject[] ToInstatiate;
    private void Awake()
    {
        //Spawn instances
        Transform marker = new GameObject("==SINGLETONS==").transform;
        foreach (GameObject gameObject in ToInstatiate)
        {
            Instantiate(gameObject).transform.SetAsFirstSibling();
        }
        marker.SetAsFirstSibling();

        Destroy(gameObject);
    }
}
