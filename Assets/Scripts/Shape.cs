using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : Block
{
    [SerializeField] Block[] blocks;
    int maxHits;
    private void Start()
    {
        SetCachedReferences();
        SetMaxHits();
        timesHit = 0;
        level.CountBreakableObjects();
    }
    private void SetMaxHits()
    {
        if (blocks[0] != null)
        {
            maxHits = blocks[0].getMaxHits();
        }
        else Debug.LogError("Child blocks are missing. GameObject Name: " + gameObject.name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damage();
    }
    new public void Damage() 
    {
        timesHit++;
        // Iterate through blocks and call Damage().
        foreach (Block block in blocks)
        {
            block.Damage();
        }
        if (timesHit >= maxHits) {
            Break();
        }
    }
}
