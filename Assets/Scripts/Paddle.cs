using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    [SerializeField] float screenWidthInUnits = 16f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Get relative position on screen of mouse.
        Vector2 mousePosInUnits = Input.mousePosition / (float) Screen.width * screenWidthInUnits;
        Debug.Log("Mouse Position in Units: "+mousePosInUnits);
        transform.position = new Vector2(mousePosInUnits.x, transform.position.y);

    }
}
