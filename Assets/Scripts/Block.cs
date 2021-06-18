using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    Color color;
    [SerializeField] Sprite currentSprite;
    [SerializeField] bool isWeakSpot;
    public float pixelsPerUnit { get; set; }
    public float pixelSize { get; set; }

    private void Start()
    {
       
    }

    private void Awake()
    {
        pixelsPerUnit = currentSprite.pixelsPerUnit;
        pixelSize = currentSprite.rect.width;
        Debug.Log("pixelsPerUnit: " + pixelsPerUnit);
        Debug.Log("pixelSize: " + pixelSize);
    }

    private void OnHit()
    {

    }

}
