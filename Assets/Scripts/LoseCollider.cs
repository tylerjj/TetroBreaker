using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("The following entered the LoseCollider: " + other.tag);
        // When Ball enters LoseCollider, load Game Over scene. 
            
        /*
            
        // SceneLoader: (This technically works, but only since our next scene technically is our gameover scene.
        // Best practice would probably be to add a separate public LoadLoseScene(); to the sceneLoader. 
        SceneLoader sceneLoader = GameObject.Find("Scene Loader").GetComponent<SceneLoader>();
        sceneLoader.LoadNextScene();
        
         */

         // My Commentary: Why are we doing scene management *ANYWHERE* other than the SceneLoader?
         SceneManager.LoadScene("Game Over");
    }
}

