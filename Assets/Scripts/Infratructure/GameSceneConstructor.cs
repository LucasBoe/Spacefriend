using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneConstructor : MonoBehaviour
{
    private static GameSceneConstructor instance; 
    public GameObject[] ToInstatiate;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            //Spawn instances
            Transform marker = new GameObject("==SINGLETONS==").transform;
            foreach (GameObject gameObject in ToInstatiate)
            {
                Transform newInstance = Instantiate(gameObject).transform;
                newInstance.SetParent(marker);
                newInstance.SetAsFirstSibling();
            }
            marker.SetAsFirstSibling();

            Destroy(gameObject, 1);

        } else
        {
            Destroy(instance);
            return;
        }
    }
}
