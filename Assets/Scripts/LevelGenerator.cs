using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
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
                bool hWall = (tileData == 2 || tileData == 4 || tileData == 7); //can simplify later
                int vOffset = 0;
                if (tileData == 0) continue;
                else if (tileData < 5)
                {
                    rot = RotateTile(near, tileData, hWall, pos);
                    if (hWall)
                    {
                        if (near[0] && near[2])
                        {
                            rot = 90;
                            vOffset = 180;
                        }
                        else if (near[1] && near[3])
                        {
                            rot = (!Edge(pos) && tileData == 2 && y != maxY - 2) ? 180 : 0; //bit of a sketchy solution. i'll update here if i figure something out
                        }
                        else if ((near[0] || near[2]) && !(near[1] || near[3]))
                            rot = 90;
                    }
                    else
                    {
                        if (!Edge(pos) && tileData == 1) tileData = 8;
                        if(near[0] && near[1] && near[2] && near[3])
                        {
                            near = new bool[] { IsWall(pos, 1, -1), IsWall(pos, 1, 1), IsWall(pos, -1, 1), IsWall(pos, -1, -1) };
                            //if(IsWall(pos))
                            //for (int i = 0; i < near.Length; i++)
                            //{

                            //}
                            if (!near[0])
                                rot = 90;
                            else if (!near[1])
                                rot = 0;
                            else if (!near[2])
                                rot = 270;
                            else if (!near[3])
                                rot = 180;
                        }
                        else if (near[0] && near[1])
                            rot = 90;
                        else if (near[1] && near[2])
                            rot = 0;
                        else if (near[2] && near[3])
                            rot = 270;
                        else if (near[3] && near[0])
                            rot = 180;
                        else rot = 90;
                    }
                }

                if (y != maxY - 1)
                {
                    Instantiate(levelAssets[tileData - 1], new Vector3(x - maxX, maxY - y - 2), Quaternion.AngleAxis(rot, Vector3.forward));
                    Instantiate(levelAssets[tileData - 1], new Vector3(maxX - x - 1, maxY - y - 2), Quaternion.AngleAxis(hWall ? rot + vOffset : 270 - rot, Vector3.forward));
                }
                Instantiate(levelAssets[tileData - 1], new Vector3(x - maxX, y - maxY), Quaternion.AngleAxis(hWall ? 180 + rot + vOffset : 90 - rot, Vector3.forward));
                Instantiate(levelAssets[tileData - 1], new Vector3(maxX - x - 1, y - maxY), Quaternion.AngleAxis(hWall ? 180 + rot : 180 + rot, Vector3.forward));
            }
        }
    }
    private bool IsWall(Vector2Int p, int checkX, int checkY)
    {
        Vector2Int p2 = new Vector2Int(p.x + checkX, p.y + checkY);
        if (p2.x < levelMap.GetUpperBound(1) + 1 && p2.x >= 0 && p2.y < levelMap.GetUpperBound(0) + 1 && p2.y >= 0)
        {
            if ((levelMap[p2.y, p2.x] % 5 == 0 || levelMap[p2.y, p2.x] == 6)) return false;
            else return true;
        }
        else return false;
    }
    private bool Edge(Vector2Int p)
    {
        if (p.x == levelMap.GetUpperBound(1) || p.x == 0 || p.y == levelMap.GetUpperBound(0) || p.y == 0)
        {
            return true;
        }
        else return false;
    }
    private int RotateTile(bool[] near, int tileData, bool isWall, Vector2Int pos)
    {
        if (isWall)
        {
            if (near[0] && near[2])
                return 90;
            else if (near[1] && near[3])
                return 0;
            else if ((near[0] || near[2]) && !(near[1] || near[3]))
                return 90;
        }
        else
        {
            if (near[0] && near[1])
                return 90;
            else if (near[1] && near[2])
                return 0;
            else if (near[2] && near[3])
                return 270;
            else if (near[3] && near[0])
                return 180;
            else return 90;
        }
        return 0;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
