using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadAllScenes()
    {
        Debug.Log("Start: SceneLoader");
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


        Debug.Log("End: SceneLoader");
        Destroy(gameObject, 1);
    }
}
