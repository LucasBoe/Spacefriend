using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonLoader : MonoBehaviour
{
    public GameObject[] ToInstatiate;

    public void CreateAllSingletons()
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
