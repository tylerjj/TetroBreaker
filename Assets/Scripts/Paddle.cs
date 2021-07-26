using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Lecture 54 Optional TODO: Set configuration parameters in a way
// that is dynamic, as opposed to serialized and semi-hardcoded. 


public class Paddle : MonoBehaviour
{

    // configuration parameters
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float minXPos = 1f;
    [SerializeField] float maxXPos = 15f;

    // cached references
    Ball[] balls;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        balls = FindObjectsOfType<Ball>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), minXPos, maxXPos);
        transform.position = paddlePos;

    }

    private float GetXPos()
    {
        if (gameSession.IsAutoPlayEnabled())
        {
            Ball lowestBall = null;
            foreach (Ball ball in balls)
            {
                if (ball != null)
                {
                    if (lowestBall == null)
                    {
                        lowestBall = ball;
                    } else if (ball.transform.position.y <= lowestBall.transform.position.y)
                    {
                        lowestBall = ball;
                    }
                }
            }
            //Debug.Log("PaddleAutoXPos: " + lowestBall.transform.position);
            return lowestBall.transform.position.x;
        } else
        {
            Vector2 mousePosInUnits = Input.mousePosition / (float)Screen.width * screenWidthInUnits;
            return mousePosInUnits.x;
        }
    }

    public void UpdateBallsToBeTracked()
    {
        balls = FindObjectsOfType<Ball>();
    }
}