using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    // config params
    [Range(.1f, 10f)][SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBreakableObjectDamaged = 83;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI atPlayText;
    [SerializeField] TextMeshProUGUI reserveText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] bool autoPlayEnabled = false;

    // state variables
    [SerializeField] int currentScore = 0; //Serialized for debugging purposes.
    [SerializeField] int ballsCollected = 1; // Serialized for debugging purposes.
    [SerializeField] int reserveBalls = 2; // Serialized for debugging purposes.
    [SerializeField] string levelName; // Serialized for debugging purposes. 
    [SerializeField] int ballsLoadedIntoScene = 0;
    [SerializeField] int ballsAtPlay = 1;

    private void Awake()
    {
        // If a GameSession already exists, destroy yourself (they are already 'the one'.)
        // If there's no GameSession, then go on and boot up (you'll be 'the one'.), and don't
        // destroy yourself on load. 
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        } else
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        scoreText.text = currentScore.ToString();
        UpdateBallsAtPlay();
        reserveText.text = "Lives: " + (reserveBalls + 1).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;

    }

    private void UpdateBallsAtPlay()
    {
        ballsAtPlay = ballsCollected + ballsLoadedIntoScene;
        atPlayText.text = "Balls At Play: " + ballsAtPlay.ToString();
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        levelName = scene.name;
        levelText.text = levelName;
    }
    public void AddToScore()
    {
        currentScore += pointsPerBreakableObjectDamaged;
        scoreText.text = currentScore.ToString();
    }

    public void AddToBallsCollected()
    {
        ballsCollected++;
        UpdateBallsAtPlay();
    }

    // NOTE: Sloppy solution for a really minor bug.
    public void AddToBallsCollectedAtLevelEnd()
    {
        ballsCollected++;
    }
    public void RemoveBallFromCollection()
    {
        ballsCollected--;
        UpdateBallsAtPlay();
    }

    public int GetNumberOfBallsCollected()
    {
        return ballsCollected;
    }

    public bool SpendReserveBallToKeepPlaying()
    {
        if (reserveBalls == 0)
        {
            reserveText.text = "Lives: " + (reserveBalls).ToString();
            return false;
        }
        else
        {
            reserveBalls--;
            reserveText.text = "Lives: "+(reserveBalls + 1).ToString();
            return true;
        }
    }

    public void AddReserveBall()
    {
        reserveBalls++;
        reserveText.text = "Lives: "+(reserveBalls + 1).ToString();
    }

    public void BallLoadedIntoScene()
    {
        ballsLoadedIntoScene++;
        UpdateBallsAtPlay();
    }
    public void BallRemovedFromScene()
    {
        ballsLoadedIntoScene--;
        UpdateBallsAtPlay();
    }
    

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return autoPlayEnabled;
    }
}
