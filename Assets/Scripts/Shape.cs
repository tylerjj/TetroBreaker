using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : Block
{
    [SerializeField] Block[] blocks;

    private void Start()
    {
        SetCachedReferences();
        timesHit = 0;
        level.CountBreakableObjects();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damage();
    }
    new public void Damage() 
    {
        timesHit++;
        // Iterate through children and call Damage().
        foreach (Block block in blocks)
        {
            block.Damage();
        }
        if (timesHit >= maxHits) {
            Break();
        }
    }
}
