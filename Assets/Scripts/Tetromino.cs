using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    List<Block> blocks;
    [SerializeField] Block _blockPrefab;
    [SerializeField] float _pixelSize;
    [SerializeField] float _pixelsPerUnit;
    [SerializeField] Sprite _defaultBlock;
    [SerializeField] Sprite _damagedBlock1;
    [SerializeField] Sprite _damagedBlock2;
    [SerializeField] Color color;
    private float _offset;
    // Start is called before the first frame update
    void Start()
    {
        blocks = new List<Block>();
        Block temp = Instantiate(_blockPrefab, transform.position, Quaternion.identity);
        _pixelSize = temp.pixelSize;
        _pixelsPerUnit = temp.pixelsPerUnit;
        Destroy(temp);

        Debug.Log("Tetromino _PixelSize: " + _pixelSize);
        Debug.Log("Tetromino _PixelsPerUnit: " + _pixelsPerUnit);

        _offset = _pixelSize / _pixelsPerUnit;

        InitializeBlocks();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeBlocks()
    {
        Debug.Log("Offset: " + _offset);
        Block temp = Instantiate(_blockPrefab, transform.position + new Vector3(0, 0, 0), Quaternion.identity);
        blocks.Add(temp);
        temp = Instantiate(_blockPrefab, transform.position + new Vector3(_offset, 0, 0), Quaternion.identity);
        blocks.Add(temp);
        temp = Instantiate(_blockPrefab, transform.position + new Vector3(0, -1f*_offset, 0), Quaternion.identity);
        blocks.Add(temp);
        temp = Instantiate(_blockPrefab, transform.position + new Vector3(_offset, -1f*_offset, 0), Quaternion.identity);
        blocks.Add(temp);
    }

    private void SetWeakSpot(int index)
    {

    }

    public void Damage()
    {

    }


}
