using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Block
{

    public bool broken = false;
    public override void Break()
    {
        if (!broken) { 
        DefaultBehaviourOnBreak();
        PowerUpBehaviour();
        broken = true;
        Destroy(gameObject, .1f);
        }
       
    }
    protected void PowerUpBehaviour()
    {
        string tag = gameObject.tag;
        switch (tag)
        {
            case "Life":
                LifeBlock();
                break;
            case "Bomb":
                BombBlock();
                break;
            case "LightningH":
                LightningHorizontalBlock();
                break;
            case "LightningV":
                LightningVerticalBlock();
                break;
            case "MultiBall":
                MultiBallBlock();
                break;
        }
        return;
    }

    private void LifeBlock()
    {
        gameStatus.AddReserveBall();
    }

    private void MultiBallBlock()
    {
        gameStatus.AddToBallsCollected();
        level.LoadBall();
    }

    private void BombBlock()
    {
        BoxCollider2D myCollider = GetComponent<BoxCollider2D>();
        myCollider.size = new Vector2(myCollider.size.x*3,myCollider.size.y*3);

    }
    private void LightningVerticalBlock()
    {
        BoxCollider2D myCollider = GetComponent<BoxCollider2D>();
        myCollider.size = new Vector2(myCollider.size.x, myCollider.size.y * 24);
    }

    private void LightningHorizontalBlock()
    {
        BoxCollider2D myCollider = GetComponent<BoxCollider2D>();
        myCollider.size = new Vector2(myCollider.size.x *32f, myCollider.size.y);
    }
}
