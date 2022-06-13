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
            Transform newInstance = Instantiate(gameObject).transform;
            newInstance.SetParent(marker);
            newInstance.SetAsFirstSibling();
        }
        marker.SetAsFirstSibling();

        Destroy(gameObject);
    }
}
