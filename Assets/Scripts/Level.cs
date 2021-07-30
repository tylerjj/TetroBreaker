using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    // config params
    [SerializeField] GameObject ballPrefab;

    // state vars
    [SerializeField] int breakableObjects; // Serialized for debugging purposes
    [SerializeField] int liveBalls = 0; // Serialized for debugging purposes
    [SerializeField] bool loadedPaddle = false;

    // cached component references
    SceneLoader sceneLoader;
    Shape[] shapes;
    Paddle paddle;
    GameSession gameSession;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        // When level starts up, it will make sure that all shapes in the scene aren't moving
        // until there's a live ball. 
        shapes = FindObjectsOfType<Shape>();
        NoLiveBalls();
        paddle = FindObjectOfType<Paddle>();
        gameSession = FindObjectOfType<GameSession>();
        LoadBall();
    }

    public void LoadBall()
    {
        if (!loadedPaddle)
        {
            gameSession.RemoveBallFromCollection();
            GameObject ball = Instantiate(ballPrefab);
            ball.SetActive(true);
            loadedPaddle = true;
            paddle.UpdateBallsToBeTracked();
        }
    }
        
    private void OnLiveBalls()
    {
        foreach (Shape shape in shapes)
        {
            if (shape != null)
            {
                shape.GetComponent<MovementManager>().enabled = true;
            }
        }
    }

    private void NoLiveBalls()
    {
        foreach (Shape shape in shapes) {
            if (shape != null)
            {
                shape.GetComponent<MovementManager>().enabled = false;
            }
            
        }
    }

    public void BallLaunched()
    {
       
        liveBalls++;
        loadedPaddle = false;
        if (gameSession.GetNumberOfBallsCollected() >= 1)
        {
            LoadBall();
        }
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
        if (liveBalls <= 0 && !loadedPaddle)
        {
            if (gameSession.SpendReserveBallToKeepPlaying())
            {
                gameSession.AddToBallsCollected();
                LoadBall();
            }
            else
            {
                GameOver();
            }
           
        }
    }

    public void ShapeCrossedLoseCollider()
    {
        if (!gameSession.SpendReserveBallToKeepPlaying())
        {
            GameOver();
        }
        BreakableObjectDestroyed();
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
            StartCoroutine(ShortDelayBeforeLoadNextScene(.1f));
            
        }
    }

    IEnumerator ShortDelayBeforeLoadNextScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        NextLevel();
    }

    private void NextLevel()
    {
        for(int i = 0; i < liveBalls; i++)
        {
            gameSession.AddToBallsCollectedAtLevelEnd();
        }
        sceneLoader.LoadNextScene();
    }

    private void GameOver()
    {
        sceneLoader.LoadGameOver();
    }
}
