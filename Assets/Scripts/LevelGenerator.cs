using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    //public Tile[] palette;
    //public Tilemap tileMap; //wait shit i dont need to do this in runtime
    //public Grid grid;
    public Sprite[] levelAssets;
    // Start is called before the first frame update
    void Start()
    {
        int[,] levelMap =
        {
            {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
            {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
            {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
            {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
            {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
            {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
            {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
            {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
            {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
            {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
            {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
            {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
            {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
            {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
            {0,0,0,0,0,0,5,0,0,0,4,0,0,0}
        };
        Debug.Log(levelMap.GetUpperBound(0));
        Debug.Log(levelMap.GetUpperBound(1));
        for (int y = 0; y < levelMap.GetUpperBound(0); y++)
        {
            for (int x = 0; x < levelMap.GetUpperBound(1); x++)
            {
                int tileData = levelMap[y, x];
                Vector3Int pos = new Vector3Int(x - 8, 8 - y, 0);
                if (tileData == 0) continue;
                else if (tileData < 5)
                {
                    //tileMap.SetTile(pos, palette[tileData - 1]);
                    //Tile tile = tileMap.GetTile(pos) as Tile;
                    int rot = 0;

                    if (tileData % 2 == 0)
                    {
                        if (levelMap[y + (y + 1 == levelMap.GetUpperBound(0) ? y + 1 : y - 1), x] % 5 == 0)
                        {

                        }
                    }
                    Instantiate(levelAssets[0], pos, Quaternion.identity);

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
