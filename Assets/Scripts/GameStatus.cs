using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    // config params
    [Range(.1f, 10f)][SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBreakableObjectDamaged = 83;

    // state variables
    [SerializeField] int currentScore = 0; //Serialized for debugging purposes.

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddToScore()
    {
        currentScore += pointsPerBreakableObjectDamaged;
    }
}
