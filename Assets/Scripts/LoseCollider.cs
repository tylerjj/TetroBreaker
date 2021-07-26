using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    // config param
    // [SerializeField] string[] loseTriggeringTags;

    // cached ref
    Level level;

    private void Start()
    {
        level = FindObjectOfType<Level>();
    }
    private void OnBallCollision(string tag)
    {
        if (tag.Equals("Ball"))
        {
            level.BallDied();
        }
    }

    private void OnShapeCollision(string tag)
    {
        if (tag.Equals("Shape"))
        {
            level.ShapeCrossedLoseCollider();
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene("Game Over");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("The following entered the LoseCollider: " + other.tag);
        OnBallCollision(other.tag);
        OnShapeCollision(other.tag);
        Destroy(other.gameObject);
    }


}

