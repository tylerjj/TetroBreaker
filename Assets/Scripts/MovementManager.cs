using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    // config params
    [SerializeField] uint timeToDelayMovementStart = 0;

    [Range(-1, 1)] [SerializeField] int moveDirectionY = 0;
    [SerializeField] float distanceToMoveY = 1f;
    [SerializeField] float moveIntervalY = 1f;
    
    [Range(-1, 1)] [SerializeField] int moveDirectionX = 0;
    [SerializeField] float distanceToMoveX = 1f;
    [SerializeField] float moveIntervalX = 3f;
    
    [Range(-1, 1)] [SerializeField] int rotateDirectionZ = 0;
    [SerializeField] float degreesToRotateZ = 90f;
    [SerializeField] float rotateIntervalZ = 5f;

    [SerializeField] bool rotateZMoveXRandomAction = false;
    [SerializeField] float randomActionInterval = 8f;
    

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
        Vector3 myPosition = transform.position;
        myPosition.y += moveDirection * distanceToMove;
        transform.position = myPosition;
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
