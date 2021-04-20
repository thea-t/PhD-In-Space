using AmazingAssets.CurvedWorld;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceAdventures
{
    public class GameManager : MonoBehaviour
    {
        public PlayerShip playerShip;
        public CurvedWorldController curvedWorldController;
        public static GameManager Instance { get; private set; } // static singleton

        void Awake()
        {
            if (Instance == null) { Instance = this; }
            else { Destroy(gameObject); }
        }


    }
}
