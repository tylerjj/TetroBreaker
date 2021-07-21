using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    [SerializeField] string[] loseTriggeringTags;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("The following entered the LoseCollider: " + other.tag);
        foreach (string tag in loseTriggeringTags)
        {
            if (tag.Equals(other.tag))
            {
                SceneManager.LoadScene("Game Over");
            }
        }
    }

}

