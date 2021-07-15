using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    [SerializeField] static int totalHealth = 3;

    public int currentHealth { get; set; }

    [SerializeField] public Sprite defaultBlock;

    [SerializeField] public Sprite damagedBlock_1;

    [SerializeField] public Sprite damagedBlock_2;

    private void Start()
    {
        currentHealth = totalHealth;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damage();
    }
    public void Damage() 
    {
        Debug.Log("Damage has been called.");
        currentHealth--;
        Debug.Log("Current Health: " + currentHealth);  

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
            Destroy(gameObject);
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
