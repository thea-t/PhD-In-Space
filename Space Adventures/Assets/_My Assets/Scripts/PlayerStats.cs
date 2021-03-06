using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=ibuaWPmAn9g
//https://gamedev.stackexchange.com/questions/110958/what-is-the-proper-way-to-handle-data-between-scenes
//https://www.sitepoint.com/saving-data-between-scenes-in-unity/
public static class PlayerStats
{
    // Creating public static variables to carry information between scenes
    public static List<string> completedPlanets = new List<string>();
    public static float playerHealth = 100;
    public static float playerFuel = 100;
    public static float multiplierToGatherFuel = 1;
    public static float fuelShipConsumption = 0.1f;
    public static int multiplierToReceiveDamage;
    public static int maxHealth = 100;
    public static int maxFuel = 100;
    public static int damageToDeal = 5;
    public static int bulletSpeed = 5;
    public static int dnaSampleCount = 0;
    public static int gainHealthOnEnemyDeath = 10;
    public static Levels currentLevel = Levels.MilkyWay;
    

}
