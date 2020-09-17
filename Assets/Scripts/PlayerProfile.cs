using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerProfile
{
    public static int levelIndex;
    public static Gamemode gameMode;
}

public enum Gamemode
{
    singlePlayer = 1,
    bot = 0
}
