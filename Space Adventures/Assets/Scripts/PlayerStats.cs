using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=ibuaWPmAn9g
//https://gamedev.stackexchange.com/questions/110958/what-is-the-proper-way-to-handle-data-between-scenes
//https://www.sitepoint.com/saving-data-between-scenes-in-unity/
public static class PlayerStats
{

    public static float playerHealth = 100;
    public static float maxHealth = 100;
    public static float multiplierToDealDamage = 1;
    public static float multiplierToReceiveDamage = 1;
    public static float baseDamage = 5;
    public static float playerFuel = 50;
    public static float maxFuel = 100;
    public static float fuelMultiplier = 1;
    public static float fuelShipConsumption = 0.01f;
    public static int dnaSampleCount = 0;
    public static string currentGalaxy = "Milky Way";
}
