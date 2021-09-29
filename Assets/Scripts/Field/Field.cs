using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public List<GameObject> tilePrefabs;
    public Vector2 min;
    public Vector2 max;
    public Vector2 size;
    public int row;
    public int column;

    public List<GameObject> tiles = new List<GameObject>();
    public LayerMask tileMask;

    bool isInit = false;

    int tileNumber = 0;

    
    public Vector2 curMin;
    public Vector2 curMax;

    private void Start()
    {
        //Debug.Log("Start Field Setup");
        tileMask = LayerMask.GetMask("Tile");
        curMin = min;
        curMax = max;
        Debug.Log(curMin);
        Debug.Log(curMax);
        for(int r = 0; r < row; ++r)
        {
            for(int c = 0; c < column; ++c)
            {
                Vector3 pos = min;
                pos.x += size.x * c;
                pos.y += size.y * r;

                GameObject go = Instantiate(tilePrefabs[Random.Range(0, tilePrefabs.Count)],pos,Quaternion.Euler(-90f,0f,0f),transform);
                go.name = tileNumber.ToString();
                tileNumber++;
                
                //Debug.Log($"min : {min}, size.x : {size.x} size.y : {size.y}, pos : {pos}");
                //go.transform.position = pos;
                //go.transform.parent = transform;
                tiles.Add(go);
            }
        }
        isInit = true;
    }

    public void UpdateSector(GameObject newSector)
    {
        if (!isInit) return;
        //Debug.Log($"Update About {newSector.name}");
        Vector3 center = newSector.transform.position;

        curMin.x = center.x - (column / 2) * size.x;
        curMin.y = center.y - (row / 2) * size.y;

        curMax.x = center.x + (column / 2) * size.x;
        curMax.y = center.y + (row / 2) * size.y * 1.5f;

        for(int i = 0; i < tiles.Count; ++i)
        {
            Vector3 pos = tiles[i].transform.position;
            if (pos.x < curMin.x || pos.x > curMax.x ||
                pos.y < curMin.y || pos.y > curMax.y)
            {
                //Debug.Log($"Destroy {tiles[i].name}");
                Destroy(tiles[i]);
                tiles.RemoveAt(i);
            }
        }
        //Debug.Log($"newMin : ({newMin.x},{newMin.y}) sizeY : {size.y} , newMax : ({newMax.x},{newMax.y}) sizeX : {size.x}");
        for (float yPos = curMin.y; yPos <= curMax.y; yPos += size.y)
        {
            for (float xPos = curMin.x; xPos <= curMax.x; xPos += size.x)
            {
                Collider[] colliders = Physics.OverlapBox(new Vector3(xPos, yPos), Vector3.one, Quaternion.identity, tileMask);
                if (colliders.Length > 0)
                    continue;
                GameObject go = Instantiate(tilePrefabs[Random.Range(0, tilePrefabs.Count)], new Vector3(xPos, yPos),Quaternion.Euler(-90f,0f,0f),transform);
                go.transform.name = tileNumber.ToString();
                tileNumber++;
                tiles.Add(go);
                
            }
        }
    }
}
