using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{   
    // config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject sparklesVFX;
    [SerializeField] public Sprite defaultBlock;
    [SerializeField] public Sprite damagedBlock_1;
    [SerializeField] public Sprite damagedBlock_2;
    [Range(1, 3)][SerializeField] int maxHits = 3;

    // state variables
    public int hitsRemaining;

    // cached reference
    Level level;
    GameSession gameStatus;
    SpriteRenderer spriteRenderer;


    private void Start()
    {

        SetCachedReferences();
        
        hitsRemaining = maxHits;

        UpdateSpriteForCurrentHealth();

        CountBreakableObjects();
    }

    private void SetCachedReferences()
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

    private void UpdateSpriteForCurrentHealth()
    {
        
        if (hitsRemaining == 3)
        {
            ChangeCurrentSprite(defaultBlock);
        }
        else if (hitsRemaining == 2)
        {
            ChangeCurrentSprite(damagedBlock_1);
        }
        else if (hitsRemaining == 1)
        {
            ChangeCurrentSprite(damagedBlock_2);
        } 
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(sparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 2f);
    }   
    public void Damage()
    {
        
        hitsRemaining--;   
        gameStatus.AddToScore();

        if (hitsRemaining <= 0)
        {
            Break();
        } else
        {
            UpdateSpriteForCurrentHealth();
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
