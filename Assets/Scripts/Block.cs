using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{   
    [SerializeField] AudioClip breakSound; 
    [SerializeField] public Sprite defaultBlock;
    [SerializeField] public Sprite damagedBlock_1;
    [SerializeField] public Sprite damagedBlock_2;
    [SerializeField] int totalHealth = 3;
    
    public int currentHealth { get; set; }



    private void Start()
    {
        currentHealth = totalHealth;
    }
    public void changeCurrentSprite(Sprite newSprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
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
        if (currentHealth == 2)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = damagedBlock_1;
        }
        else if (currentHealth == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = damagedBlock_2;
        }
        else
        {
            
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position, .2f);
        }
    }
}
