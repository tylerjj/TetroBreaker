using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{   
    // config params
    [SerializeField] protected AudioClip breakSound;
    [SerializeField] protected GameObject sparklesVFX;
    [SerializeField] Sprite[] hitSprites;

    // state variables
    [SerializeField] int timesHit;

    // cached reference
    protected Level level;
    GameSession gameStatus;
    SpriteRenderer spriteRenderer;


    private void Start()
    {

        SetCachedReferences();

        timesHit = 0;

        UpdateSpriteForCurrentHitCount();

        
        CountBreakableObjects();
    }

    protected void SetCachedReferences()
    {
        level = FindObjectOfType<Level>();
        gameStatus = FindObjectOfType<GameSession>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void CountBreakableObjects()
    {
        // If not part of a larger shape
        if (!transform.parent.CompareTag("Shape"))
        {
            // and if breakable
            if (gameObject.CompareTag("Breakable"))
            {
                // add to count of breakable objects
                level.CountBreakableObjects();
            }
        }
    }

    private void ChangeCurrentSprite(Sprite newSprite)
    {
        spriteRenderer.sprite = newSprite;
    }

    private void UpdateSpriteForCurrentHitCount()
    {
        if (hitSprites[timesHit] != null)
        {
            ChangeCurrentSprite(hitSprites[timesHit]);
        } else
        {
            Debug.LogError("Block sprite is missing from array. GameObject Name: " + gameObject.name);
        }
    }

    protected void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(sparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 2f);
    }
    
    public void Damage()
    {
        
        timesHit++;
        int maxHits = hitSprites.Length;

        gameStatus.AddToScore();

        if (timesHit >= maxHits)
        {
            Break();
        } else UpdateSpriteForCurrentHitCount();
      
    }

    public void Break()
    {
        if (!transform.parent.CompareTag("Shape")) 
        { 
            AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position, .2f);
            TriggerSparklesVFX();
            level.BreakableObjectDestroyed();
        }
        
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if not part of a larger shape
        if (!transform.parent.CompareTag("Shape"))
        {
            // and if breakable
            if (gameObject.CompareTag("Breakable"))
            {
                // damage this breakable object. 
                Damage();
            }
        }
    }



}
