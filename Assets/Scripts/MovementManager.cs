using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    //[SerializeField] bool isMovingYAxis = false;
    [Range(-1, 1)] [SerializeField] int moveDirectionY = 0;
    [SerializeField] float distanceToMoveY = 1f;
    [SerializeField] float moveIntervalY = 4f;
    

    // [SerializeField] bool isMovingXAxis = false;
    [Range(-1, 1)] [SerializeField] int moveDirectionX = 0;
    [SerializeField] float distanceToMoveX = 1f;
    [SerializeField] float moveIntervalX = 7f;

    [Range(-1, 1)] [SerializeField] int rotateDirectionZ = 0;
    [SerializeField] float degreesToRotateZ = 90f;
    [SerializeField] float rotateIntervalZ = 10f;

    [SerializeField] bool rotateZMoveXRandomAction = false;
    [SerializeField] float randomActionInterval = 8f;
    
    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DelayedMoveOnYAxis(int moveDirection, float distanceToMove, float moveInterval)
    {
        while (true)
        {
            yield return new WaitForSeconds(moveInterval);
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
            yield return new WaitForSeconds(moveInterval);
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
            yield return new WaitForSeconds(rotateInterval);
            RotateOnZAxis(rotateDirection, degreesToRotate);
        }
    }

    private void RotateOnZAxis(int rotateDirection, float degreesToRotate)
    {
        transform.Rotate(0f, 0f, degreesToRotate);
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
            
            yield return new WaitForSeconds(actionInterval);

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
