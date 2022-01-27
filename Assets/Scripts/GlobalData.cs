using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[InitializeOnLoad]
public static class GlobalData
{
    public static int numLevels = new int();
    public static float defaultPlayerSpeed = new float();
    public static float defaultPlayerJumpSpeed = new float();
    public static float laserBulletSpeed = new float();
    public static int[][] roomStarCoinThresholds;
    public static Vector2[][] roomPositions;
    public static List<Tuple<int, int, Gem.GemType[]>> gemTypesInRooms;

    static GlobalData()
    {
        // Misc game data
        numLevels = 100;
        defaultPlayerSpeed = 10f;
        defaultPlayerJumpSpeed = 100f;
        laserBulletSpeed = 12f;

        roomStarCoinThresholds = new int[][]
        {
            //new int[] { 1, 2, 3, 6 },
            new int[] { 1, 1, 1, 1 },
            new int[] { 0 },
            new int[] { 0 }
        };

        roomPositions = new Vector2[][]
        {
            //Level 1
            new Vector2[] 
            { 
                new Vector2(-9.72f, -1.53f),  new Vector2(12.18f, -1.53f), new Vector2(33.84f, -3.42f), new Vector2(55.46f, -3.42f),
                new Vector2(76.33f, -3.42f), new Vector2(97.3f, -0.54f), new Vector2(121.19f, -3.42f), new Vector2(140.07f, -3.42f),
                new Vector2(161.42f, -3.42f), new Vector2(182.46f, -1.53f)
            }
        };

        gemTypesInRooms = new List<Tuple<int, int, Gem.GemType[]>>()
        {
            new Tuple<int, int, Gem.GemType[]>(1, 10, new Gem.GemType[] { Gem.GemType.Blue, Gem.GemType.Red, Gem.GemType.Green } )
        };


    }

}