using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    const float PLAYER_MAX_HP = 100;
    const int PLAYER_BREAK_COUNT = 4;
    const int PLAYER_PORTION_COUNT = 1;
    const bool PLAYER_DIE = false;

    const float SECONDS = 0;
    const float MINUTE = 0;

    public static float playerMaxHp { get; set; }
    public static float playerHp { get; set; }
    public static int playerBreakCount { get; set; }
    public static int playerPortionCount { get; set; }
    public static bool playerDie { get; set; }

    public static float seconds { get; set; }
    public static float minute { get; set; }


    public static void ResetValue()
    {
        playerMaxHp = PLAYER_MAX_HP;
        playerHp = playerMaxHp;
        playerBreakCount = PLAYER_BREAK_COUNT;
        playerPortionCount = PLAYER_PORTION_COUNT;
        playerDie = PLAYER_DIE;
        seconds = SECONDS;
        minute = MINUTE;
    }
}
