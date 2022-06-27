using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    SceneLoader instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                Scene scene = SceneManager.GetSceneByBuildIndex(i);
                if (!scene.IsValid())
                {
                    Debug.Log("Load scene " + i + " => " + scene.path);
                    SceneManager.LoadScene(i, LoadSceneMode.Additive);
                }
                else
                {
                    Debug.LogWarning("Did not load scene " + i + " => " + scene.name + " (already open)");
                }
            }

            Destroy(gameObject, 1);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
