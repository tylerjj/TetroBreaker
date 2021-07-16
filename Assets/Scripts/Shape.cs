using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{   
    [SerializeField] AudioClip breakSound; 
    [SerializeField] public Sprite defaultBlock;
    [SerializeField] public Sprite damagedBlock_1;
    [SerializeField] public Sprite damagedBlock_2;
    [SerializeField] static int totalHealth = 3;
    
    // cached reference
    Level level;

    public int currentHealth { get; set; }



    private void Start()
    {
        level = FindObjectOfType<Level>();
        currentHealth = totalHealth;
        level.CountBreakableObjects();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damage();
    }
    public void Damage() 
    {
        currentHealth--;
        

        // Iterate through children and call Damage().
        foreach (Transform child in transform)
        {
            //// What I wish I could do:
            // Block child = (Block)child.gameObject;
            // child.Damage();


            DamageBlock(child.gameObject);

        }            
        if (currentHealth == 0)
        {
            level.BreakBreakableObjects();
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position, .2f);
        }
    }

    public void DamageBlock(GameObject gameObj)
    {
        if (currentHealth == 2)
        {
            gameObj.GetComponent<SpriteRenderer>().sprite = damagedBlock_1;
        }
        else if (currentHealth == 1)
        {
            gameObj.GetComponent<SpriteRenderer>().sprite = damagedBlock_2;
        }
        else
        {
            Destroy(gameObj);
        }
    }
}
