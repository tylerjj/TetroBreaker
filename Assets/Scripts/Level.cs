using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    [SerializeField] int breakableObjects; // Serialized for debugging purposes

    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }
    public void CountBreakableObjects()
    {
        breakableObjects++;
    }

    public void BreakableObjectDestroyed()
    {
        breakableObjects--;
        if (breakableObjects <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}
