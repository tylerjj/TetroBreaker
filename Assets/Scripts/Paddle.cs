using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Lecture 54 Optional TODO: Set configuration parameters in a way
// that is dynamic, as opposed to serialized and semi-hardcoded. 


public class Paddle : MonoBehaviour
{

    // configuration parameters
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float minMousePosX = 1f;
    [SerializeField] float maxMousePosX = 15f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // Get relative position on screen of mouse.
        Vector2 mousePosInUnits = Input.mousePosition / (float)Screen.width * screenWidthInUnits;
        //Debug.Log("Mouse Position in Units: " + mousePosInUnits);
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(mousePosInUnits.x, minMousePosX, maxMousePosX);
        transform.position = paddlePos;

    }
}