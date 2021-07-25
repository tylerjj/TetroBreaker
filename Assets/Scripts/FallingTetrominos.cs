using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  @Author Tyler Johnston 07/24/2021
 *  
 * This script is brute force at best. I wouldn't be against taking
 * a weekend to try refactoring it into something more efficient 
 * (object pooling instead of an endless Instantiate/Destroy loop)
 * and removing the copy/pasted code *see the SpawnLane config params*.
 * 
 * Ultimately, this script provides flavor to the game feel. But there
 * are many more sticky notes on my Desktop that are demanding my attention,
 * and then I need to pivot into another project. So this guy may not get
 * much love.
 * 
 * Until next time....Sorry for creating this frankenscript.
 * 
 * - Tyler
 */
public class FallingTetrominos : MonoBehaviour
{
    // config params : all lanes
    [SerializeField] float dropBlockFromThisYPos = 16f;
    [Range(0, 360)][SerializeField] float angleRotation = 90;
    [SerializeField] int maxRotations = 4;

    // config params : lane 01 
    [SerializeField] float timeToFirstSpawnLane01 = 2f;
    [SerializeField] float spawnIntervalLane01 = 5f;
    [SerializeField] float fallUnitsPerSecondLane01 = .5f;
    [SerializeField] float lowBoundXSpawnLane01 = .8f;
    [SerializeField] float upperBoundXSpawnLane01 = 2f;

    // config params : lane 02
    [SerializeField] float timeToFirstSpawnLane02 = 0f;
    [SerializeField] float spawnIntervalLane02 = 5f;
    [SerializeField] float fallUnitsPerSecondLane02 = .5f;
    [SerializeField] float lowBoundXSpawnLane02 = 7.9f;
    [SerializeField] float upperBoundXSpawnLane02 = 8.1f;

    // config params : lane 03
    [SerializeField] float timeToFirstSpawnLane03 = 3f;
    [SerializeField] float spawnIntervalLane03 = 5f;
    [SerializeField] float fallUnitsPerSecondLane03 = .5f;
    [SerializeField] float lowBoundXSpawnLane03 = 14f;
    [SerializeField] float upperBoundXSpawnLane03 = 15.2f;

    // config params : our template tetrominos, stripped of all
    // other components, save sprite renderers and MovementManagers.
    [SerializeField] MovementManager[] templates;

    private void Start()
    {        
        // Disable the template tetrominos that we use
        // as a reference for instantiation.
        templates = DisableTemplates(templates);
        
        // For Tyler's original config params: Left Lane of Descent
        StartCoroutine(ShapeSpawner(timeToFirstSpawnLane01, spawnIntervalLane01, 
            fallUnitsPerSecondLane01, lowBoundXSpawnLane01, upperBoundXSpawnLane01));
        
        // For Tyler's original config params: Center Lane of Descent
        StartCoroutine(ShapeSpawner(timeToFirstSpawnLane02, spawnIntervalLane02, 
            fallUnitsPerSecondLane02, lowBoundXSpawnLane02, upperBoundXSpawnLane02));

        // For Tyler's original config params: Right Lane of Descent
        StartCoroutine(ShapeSpawner(timeToFirstSpawnLane03, spawnIntervalLane03, 
            fallUnitsPerSecondLane03, lowBoundXSpawnLane03, upperBoundXSpawnLane03));
    }



    private MovementManager[] DisableTemplates(MovementManager[] templates)
    {
        foreach(MovementManager shape in templates)
        {
            shape.enabled = false;
        }
        return templates;
    }

    private MovementManager GetRandomShape(MovementManager[] shapes)
    {
        if (shapes.Length <= 0)
        {
            return null;
        }
        else
        {
            int index = UnityEngine.Random.Range(0, shapes.Length);
            return shapes[index];
        }
    }

    private MovementManager GetCopyOfShape(MovementManager shape)
    {
        MovementManager copy = Instantiate(shape);
        copy.transform.SetParent(transform);
        return copy;
    }

    private float GetRandomXPos(float low, float high)
    {
        return Random.Range(low, high);
    }

    private int GetRandomAngleFactor(int low, int high)
    {
        return Random.Range(low, high + 1);
    }

    private MovementManager SetShapeTransform(MovementManager shape, float x, float y, float rot)
    {
        shape.transform.position = new Vector2(x, y);
        shape.transform.Rotate(0f, 0f, rot);
        return shape;
    }
    private MovementManager ActivateShape(MovementManager shape)
    {
        shape.gameObject.SetActive(true);
        return shape;
    }

    // TODO: Bad Programmer Alert: Going to be hardcoding values here. 
    private MovementManager ActivateShapeDescent(float descentInterval, MovementManager shape)
    {
        /*
        MovementManager mover = shape.GetComponent<MovementManager>();
        mover.moveDirectionY = -1;
        mover.moveIntervalY = descentInterval;
        mover.enabled = true;
        return shape;
        */

        shape.moveDirectionY = -1;
        shape.moveIntervalY = descentInterval;
        shape.enabled = true;
        return shape;
    }
    private void SpawnRandomDescendingShape(float descentInterval, float low, float high)
    {
        // Get a random xPos between low and high
        float randomXPos = GetRandomXPos(low, high);
        // Get a random value between 1 and 4. This is our randomAngleFactor.
        int randomAngleFactor = GetRandomAngleFactor(0, maxRotations);

        // Get a random tetromino shape.
        MovementManager templateTetromino = GetRandomShape(templates);
        if (templateTetromino == null)
        {
            throw new System.Exception("Couldn't get a template tetromino " +
                "from the array of tetrominos.");
        }

        // Instantiate a copy of it.
        MovementManager tetromino = GetCopyOfShape(templateTetromino);

        // Set the instantiated shape's location to be randomXPos, dropBlockFromThisYPos.
        // Set the instantiated shape's rotation to be 90 degrees * randomAngleFactor.
        tetromino = SetShapeTransform(tetromino, randomXPos, dropBlockFromThisYPos, angleRotation * (float)randomAngleFactor);

        // Set the shape to active.
        tetromino = ActivateShape(tetromino);

        // Set the shape's movement manager to descend every 1 seconds.
        // Set the shape's movement manager to active.
        tetromino = ActivateShapeDescent(descentInterval, tetromino);

        // Give the shape ample time to descend the whole screen, then destroy it.
        Destroy(tetromino.gameObject, dropBlockFromThisYPos);
    }

    IEnumerator ShapeSpawner(float timeToFirstSpawn, float spawnDelayTime, float descentInterval, float lowBoundX, float highBoundX)
    {
        yield return new WaitForSecondsRealtime(timeToFirstSpawn);
        SpawnRandomDescendingShape(descentInterval, lowBoundX, highBoundX);
        while (true)
        {
            yield return new WaitForSecondsRealtime(spawnDelayTime);
            SpawnRandomDescendingShape(descentInterval, lowBoundX, highBoundX);
        }
        
    }
}
