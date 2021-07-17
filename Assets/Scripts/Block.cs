using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{   
    // config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject sparklesVFX;
    [SerializeField] Sprite[] hitSprites;
    [Range(1, 3)][SerializeField] protected int maxHits = 3;

    // state variables
    protected int timesHit;

    // cached reference
    protected Level level;
    GameSession gameStatus;
    SpriteRenderer spriteRenderer;


    private void Start()
    {

        SetCachedReferences();

        timesHit = 0;

        if (gameObject.CompareTag("Breakable")) {
            UpdateSpriteForCurrentHitCount();
        }
        
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

    public void ChangeCurrentSprite(Sprite newSprite)
    {
        spriteRenderer.sprite = newSprite;
    }

    private void UpdateSpriteForCurrentHitCount()
    {
        ChangeCurrentSprite(hitSprites[timesHit]);
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(sparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 2f);
    }   
    public void Damage()
    {
        
        timesHit++;   
        gameStatus.AddToScore();

        if (timesHit >= maxHits)
        {
            if (!transform.parent.CompareTag("Shape"))
            {
                Break();
            }
            
        } else
        {
            UpdateSpriteForCurrentHitCount();
        }
    }

    public void Break()
    {
            level.BreakableObjectDestroyed();
            AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position, .2f);
            TriggerSparklesVFX();
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
