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

    static GlobalData()
    {
        // Misc game data
        numLevels = 100;
        defaultPlayerSpeed = 10;
        defaultPlayerJumpSpeed = 100;
    }

}