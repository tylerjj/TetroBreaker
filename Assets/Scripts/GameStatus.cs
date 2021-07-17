using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatus : MonoBehaviour
{
    // config params
    [Range(.1f, 10f)][SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBreakableObjectDamaged = 83;
    [SerializeField] TextMeshProUGUI scoreText;

    // state variables
    [SerializeField] int currentScore = 0; //Serialized for debugging purposes.

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
}
