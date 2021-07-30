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
        } else if (collision.gameObject.CompareTag("Bomb") || 
            collision.gameObject.CompareTag("LightningV") || 
            collision.gameObject.CompareTag("LightningH"))
        {
            BreakChildren();
        }
    }    
    
    private void CountBreakableObjects()
    {
        level.CountBreakableObjects();
    }
    
    new private void Damage() 
    {
        /*
        // Iterate through blocks and call Damage().
        foreach (Block block in blocks)
        {
            block.Damage();
        }
        */
        StartCoroutine(DamageBlocks());
    }
    IEnumerator DamageBlocks()
    {
        // Iterate through blocks and call Damage().
        foreach (Block block in blocks)
        {
            yield return new WaitForSeconds(.1f);
            if (block != null)
            {
                block.Damage();
            }
            
        }
    }
    new private void Break()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position, .2f);
        TriggerSparklesVFX();
        level.BreakableObjectDestroyed();
        Destroy(gameObject);
    }

    private void BreakChildren()
    {
            foreach (Block block in blocks)
            {
                if (block != null)
                {
                    block.Break();
                }
            }
    }
}
