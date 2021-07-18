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
    Ball ball;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<Ball>();
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
            return ball.transform.position.x;
        } else
        {
            Vector2 mousePosInUnits = Input.mousePosition / (float)Screen.width * screenWidthInUnits;
            return mousePosInUnits.x;
        }
    }
}