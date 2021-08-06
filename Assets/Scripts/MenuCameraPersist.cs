using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCameraPersist : MonoBehaviour
{
    int scenesPlayed = 0;

    private void Awake()
    {
        int cameraCount = FindObjectsOfType<Camera>().Length;
        Debug.Log("Cameras Found: " + cameraCount);
        if (cameraCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals("Description Screen"))
        {
            scenesPlayed = 2;
        }
        else
        {
            scenesPlayed++;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scenesPlayed > 2)
        {
            Destroy(gameObject);
        }
    }
}
