using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{   
    // config params
    [SerializeField] AudioClip breakSound; 
    [SerializeField] public Sprite defaultBlock;
    [SerializeField] public Sprite damagedBlock_1;
    [SerializeField] public Sprite damagedBlock_2;
    [SerializeField] int totalHealth = 3;

    // state variables
    public int currentHealth;

    // cached reference
    Level level;
    GameStatus gameStatus;
    SpriteRenderer spriteRenderer;


    

    private void Start()
    {
        
        level = FindObjectOfType<Level>();
        gameStatus = FindObjectOfType<GameStatus>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        currentHealth = totalHealth;

        if (!transform.parent.CompareTag("Shape"))
        {
            // Behavior when this block is NOT a piece of a larger shape.
            level.CountBreakableObjects();
        }
    }

    public void changeCurrentSprite(Sprite newSprite)
    {
        spriteRenderer.sprite = newSprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.parent.CompareTag("Shape")) {
            // Behavior when this block is a piece of a larger shape.
        }
        else
        {
            Damage();
        }
    }

    public void Damage()
    {
        
        currentHealth--;   
        gameStatus.AddToScore();

        if (currentHealth == 2)
        {
            changeCurrentSprite(damagedBlock_1);
        }
        else if (currentHealth == 1)
        {
            changeCurrentSprite(damagedBlock_2);
        }
        else
        {
            level.BreakableObjectDestroyed();
            AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position, .2f);
            Destroy(gameObject);
        }
    }
}
