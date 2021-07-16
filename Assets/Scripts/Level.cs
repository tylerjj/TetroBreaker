using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    [SerializeField] int breakableObjects; // Serialized for debugging purposes

    public void CountBreakableObjects()
    {
        breakableObjects++;
    }

    public void BreakBreakableObjects()
    {
        breakableObjects--;
    }
}
