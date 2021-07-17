using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    // config params
    [Range(.1f, 10f)][SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBreakableObjectDamaged = 83;
    [SerializeField] TextMeshProUGUI scoreText;

    // state variables
    [SerializeField] int currentScore = 0; //Serialized for debugging purposes.

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
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {



        scoreText.text = currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddToScore()
    {
        currentScore += pointsPerBreakableObjectDamaged;
        scoreText.text = currentScore.ToString();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
