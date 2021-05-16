using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=ibuaWPmAn9g
//https://gamedev.stackexchange.com/questions/110958/what-is-the-proper-way-to-handle-data-between-scenes
//https://www.sitepoint.com/saving-data-between-scenes-in-unity/
public static class PlayerStats
{
    public static List<string> completedPlanets = new List<string>();
    public static float playerHealth = 100;
    public static float playerFuel = 50;
    public static float maxHealth = 100;
    public static float maxFuel = 100;
    public static float baseDamage = 5;
    public static float multiplierToDealDamage = 0;
    public static int multiplierToReceiveDamage;
    public static float multiplierToGatherFuel = 1;
    public static float bulletSpeed = 5;
    public static float fuelShipConsumption = 5;
    public static int dnaSampleCount = 0;
    public static Levels currentLevel = Levels.MilkyWay;

}
