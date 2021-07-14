using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // config parameters
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 10f;

    // state
    Vector2 paddleToBallVector;
    Boolean hasLaunched = false;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        GetComponent<Rigidbody2D>().simulated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasLaunched)
        {
            // Keep Ball Glued to Paddle
            LockToPaddle();
            // On MouseClick, Launch Ball. 
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
        // On left-click
        if (Input.GetMouseButtonDown(0))
        {
            hasLaunched = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(xPush, yPush);
            GetComponent<Rigidbody2D>().simulated = true;
        }
    }

    private void LockToPaddle()
    {
        // Get Paddle Position. 
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        // Update Ball Position relative to Paddle Position. 
        transform.position = paddlePos + paddleToBallVector;
    }
}
