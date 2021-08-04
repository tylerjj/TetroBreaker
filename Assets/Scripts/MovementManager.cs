using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  @Author Tyler Johnston 07/24/2021
 */
public class MovementManager : MonoBehaviour
{
    // config params
    [SerializeField] public uint timeToDelayMovementStart = 0;

    [Range(-1, 1)] [SerializeField] public int moveDirectionY = 0;
    [SerializeField] public float distanceToMoveY = 1f;
    [SerializeField] public float moveIntervalY = 1f;
    
    [Range(-1, 1)] [SerializeField] public int moveDirectionX = 0;
    [SerializeField] public float distanceToMoveX = 1f;
    [SerializeField] public float moveIntervalX = 3f;
    
    [Range(-1, 1)] [SerializeField] public int rotateDirectionZ = 0;
    [SerializeField] public float degreesToRotateZ = 90f;
    [SerializeField] public float rotateIntervalZ = 5f;

    [SerializeField] public bool rotateZMoveXRandomAction = false;
    [SerializeField] public float randomActionInterval = 8f;

    Rigidbody2D myRigidbody2D;

    private void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(DelayedStart(timeToDelayMovementStart));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator DelayedStart(uint seconds)
    {
        yield return new WaitForSecondsRealtime((float) seconds);
        StartMyMovements();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void StartMyMovements()
    {
        StartCoroutine(DelayedMoveOnYAxis(moveDirectionY, distanceToMoveY, moveIntervalY));
        if (rotateZMoveXRandomAction)
        {
            StartCoroutine(RotateZMoveXRandomAction(randomActionInterval));
        }
        else
        {
            StartCoroutine(DelayedMoveOnXAxis(moveDirectionX, distanceToMoveX, moveIntervalX));
            StartCoroutine(DelayedRotateOnZAxis(rotateDirectionZ, degreesToRotateZ, rotateIntervalZ));
        }
    }

    IEnumerator DelayedMoveOnYAxis(int moveDirection, float distanceToMove, float moveInterval)
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(moveInterval);
            MoveOnYAxis(moveDirection, distanceToMove);
        }

    }
    private void MoveOnYAxis(int moveDirection, float distanceToMove)
    {
        /*
        Vector3 myPosition = transform.position;
        myPosition.y += moveDirection * distanceToMove;
        transform.position = myPosition;
        */
        if (moveDirection != 0)
        {
            Vector2 myPosition = transform.position;
            myPosition.y += moveDirection * distanceToMove;
            myRigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            myRigidbody2D.MovePosition(myPosition);
            StartCoroutine(FreezePosition());
        }

    }

    IEnumerator FreezePosition()
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        myRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    IEnumerator DelayedMoveOnXAxis(int moveDirection, float distanceToMove, float moveInterval)
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(moveInterval);
            MoveOnXAxis(moveDirection, distanceToMove);
        }
    }
    private void MoveOnXAxis(int moveDirection, float distanceToMove)
    {
        Vector3 myPosition = transform.position;
        myPosition.x += moveDirection * distanceToMove;
        transform.position = myPosition;
    }

    IEnumerator DelayedRotateOnZAxis(int rotateDirection, float degreesToRotate, float rotateInterval)
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime (rotateInterval);
            RotateOnZAxis(rotateDirection, degreesToRotate);
        }
    }

    private void RotateOnZAxis(int rotateDirection, float degreesToRotate)
    {
        transform.Rotate(0f, 0f, degreesToRotate*rotateDirection);
    }


    /**
     * NOTE: This function is a bit of a special snowflake.
     * Every 'actionInterval' # of seconds, this function will 
     * either rotate a random Z direction by the config parameter 
     * 'degreesToRotateZ', or it will move a random X direction
     * by the config parameter 'distanceToMoveX'. 
     * 
     * NOTE 2: This was used primarily while testing tetrominos
     * and rigidbody physics. Since that approach didn't work out,
     * this method *shouldn't* wind up in use. Very tempted to strip
     * it from this file. In a future version, I very well may.
    **/
    IEnumerator RotateZMoveXRandomAction(float actionInterval)
    {
        while (true)
        {
            
            yield return new WaitForSecondsRealtime(actionInterval);

            Debug.Log("In the Random Action Method!");
            int randomAction = UnityEngine.Random.Range(0, 2);
            int randomDirection = UnityEngine.Random.Range(-1, 2);
        
            if (randomAction == 0f)
            {
                Debug.Log("RandomDirection = " + randomDirection + ", Rotating");
                // Rotate on Z axis
                RotateOnZAxis(randomDirection, degreesToRotateZ);

            } else
            {
                Debug.Log("RandomDirection = " + randomDirection + ", Moving");
                // Shift on X axis
                MoveOnXAxis(randomDirection, distanceToMoveX);
            }
        }

    }
}
