using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Block
{
    [SerializeField] GameObject effect;

    public override void Break()
    {
        Debug.Log("New Break Occurred...");
        DefaultBehaviourOnBreak();
        PowerUpBehaviour();
        Destroy(gameObject);
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

    }

    private void LightningVerticalBlock()
    {

    }

    private void LightningHorizontalBlock()
    {

    }
}
