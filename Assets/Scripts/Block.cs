using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{   
    // config params
    [SerializeField] protected AudioClip breakSound;
    [SerializeField] protected GameObject sparklesVFX;
    [SerializeField] protected Sprite[] hitSprites;

    // state variables
    [SerializeField] protected int timesHit;

    // cached reference
    protected Level level;
    protected GameSession gameStatus;
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
            /*
            // and if breakable
            if (gameObject.CompareTag("Breakable"))
            {
                // add to count of breakable objects
                level.CountBreakableObjects();
            }
            */

            // TODO: Maybe Powerups shouldn't be required in order to clear a level.
            if (!gameObject.CompareTag("Shape") && !gameObject.CompareTag("Unbreakable"))
            {
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

    public virtual void Break()
    {
        Debug.Log("Default Break occurred...");
        DefaultBehaviourOnBreak();

        Destroy(gameObject);
    }
    protected void DefaultBehaviourOnBreak()
    {
        if (transform.parent.CompareTag("Shape")) 
        {
            int remainingHits = hitSprites.Length - timesHit;
            while (remainingHits > 0)
            {
                gameStatus.AddToScore();
                remainingHits--;
            }
        } else
        {   
            AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position, .2f);
            TriggerSparklesVFX();
            // TODO: If we don't want powerups to be required objectives,
            // this needs to change.
            level.BreakableObjectDestroyed();
           
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision on "+name+" occured from :"+collision.gameObject.tag);
        // if not part of a larger shape
        if (!transform.parent.CompareTag("Shape") && !CompareTag("Unbreakable"))
        {            
            // if stone
            if (CompareTag("Stone"))
            {
                // Bomb damages stone
                if (collision.gameObject.CompareTag("Bomb"))
                {
                    //Debug.Log("Stone damaged.");
                    Damage();
                }
            }
            else if (collision.gameObject.CompareTag("Bomb") || 
                collision.gameObject.CompareTag("LightningV") || 
                collision.gameObject.CompareTag("LightningH"))
            {
                    gameStatus.AddToScore();
                    // Explosion breaks breakable blocks.
                    Break();
                    return;
            }
            else if (collision.gameObject.CompareTag("Ball"))
            {
                    // Ball damages breakable blocks. 
                    Damage();
            }
        }
    }
}
