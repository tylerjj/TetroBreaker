using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @Author Tyler Johnston 07/24/2021
 */

public class Shape : Block
{
    // config params
    [SerializeField] Block[] blocks;

    private void Start()
    {
        SetCachedReferences();
        CountBreakableObjects();
    }


    private void Update()
    {
        if (transform.childCount == 0)
        {
            Break();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
          Damage();
        }
    }    
    
    private void CountBreakableObjects()
    {
        level.CountBreakableObjects();
    }
    
    new public void Damage() 
    {
        // Iterate through blocks and call Damage().
        foreach (Block block in blocks)
        {
            block.Damage();
        }
    }

    new public void Break()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position, .2f);
        TriggerSparklesVFX();
        level.BreakableObjectDestroyed();
        Destroy(gameObject);
    }
}
