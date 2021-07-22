using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    // state vars
    [SerializeField] int breakableObjects; // Serialized for debugging purposes
    [SerializeField] int liveBalls = 0; // Serialized for debugging purposes
    
    // config params
    SceneLoader sceneLoader;
    Shape[] shapes;
    Paddle paddle;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        // When level starts up, it will make sure that all shapes in the scene aren't moving
        // until there's a live ball. 
        shapes = FindObjectsOfType<Shape>();
        NoLiveBalls();
        paddle = FindObjectOfType<Paddle>();
    }

    private void OnLiveBalls()
    {
        foreach (Shape shape in shapes)
        {
            shape.GetComponent<MovementManager>().enabled = true;
        }
    }

    private void NoLiveBalls()
    {
        foreach (Shape shape in shapes) {
            shape.GetComponent<MovementManager>().enabled = false;
        }
    }

    public void BallLaunched()
    {
       
        liveBalls++;
        paddle.UpdateBallsToBeTracked();
        Debug.Log("Ball Lauched. Total Balls Live: " + liveBalls);
        if (liveBalls == 1)
        {
            OnLiveBalls();
        }
    }

    public void BallDied()
    {
        
        liveBalls--;
        paddle.UpdateBallsToBeTracked();
        Debug.Log("Ball died. Total Balls Live: " + liveBalls);
        if (liveBalls <= 0)
        {
            GameOver();
        }
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
            StartCoroutine(ShortDelayBeforeLoadNextScene(.5f));
            
        }
    }

    IEnumerator ShortDelayBeforeLoadNextScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        NextLevel();
    }

    private void NextLevel()
    {
        sceneLoader.LoadNextScene();
    }

    private void GameOver()
    {
        sceneLoader.LoadGameOver();
    }
}
