using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    //public Tile[] palette;
    //public Tilemap tileMap; //unfinished; will implement later
    //public Grid grid;
    public GameObject[] levelAssets;
    private int[,] levelMap;
    // Start is called before the first frame update
    void Start()
    {
        levelMap = new int[,]
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
        int maxX = levelMap.GetUpperBound(1) + 1;
        int maxY = levelMap.GetUpperBound(0) + 1;
        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                int tileData = levelMap[y, x];
                Vector2Int pos = new Vector2Int(x, y);
                bool[] near = new bool[] { IsWall(pos, 0, -1), IsWall(pos, 1, 0), IsWall(pos, 0, 1), IsWall(pos, -1, 0) };
                int rot = 0;
                if (tileData == 0) continue;
                else if (tileData < 5)
                {
                    rot = RotateTile(near, tileData);
                }
                //Tilemap (unfinished)
                //tileMap.SetTile(pos, palette[tileData - 1]); 
                //Tile tile = tileMap.GetTile(pos) as Tile;

                //Prefab
                Instantiate(levelAssets[tileData - 1], new Vector3(x - maxX, maxY - y - 1), Quaternion.AngleAxis(rot, Vector3.forward));
                Instantiate(levelAssets[tileData - 1], new Vector3(x - maxX, y - maxY), Quaternion.AngleAxis(tileData % 2 == 0 ? rot : 90 - rot, Vector3.forward));
                Instantiate(levelAssets[tileData - 1], new Vector3(maxX - x - 1, maxY - y - 1), Quaternion.AngleAxis(tileData % 2 == 0 ? rot : 270 - rot, Vector3.forward));
                Instantiate(levelAssets[tileData - 1], new Vector3(maxX - x - 1, y - maxY), Quaternion.AngleAxis(tileData % 2 == 0 ? rot : 180 + rot, Vector3.forward));
            }
        }
    }
    private bool IsWall(Vector2Int p, int checkX, int checkY)
    {
        Vector2Int p2 = new Vector2Int(p.x + checkX, p.y + checkY);
        if (p2.x < levelMap.GetUpperBound(1) && p2.x >= 0 && p2.y < levelMap.GetUpperBound(0) && p2.y >= 0)
        {
            if (levelMap[p2.y, p2.x] % 5 == 0 || levelMap[p2.y, p2.x] == 6) return false;
        }
        else return false;
        return true;
    }
    private int RotateTile(bool[] near, int tileData)
    {
        if (tileData % 2 == 0)
        {
            if (near[0] && near[2])
            {
                return 90;
            }
            else if (near[1] && near[3])
            {
                return 0;
            }
            else if ((near[0] || near[2]) && !(near[1] || near[3]))
            {
                return 90;
            }
        }
        else
        {
            if (near[0] && near[1])
            {
                return 90;
            }
            if (near[2] && near[3])
            {
                return 270;
            }
            if (near[3] && near[0])
            {
                return 180;
            }            
        }
        return 0;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
