﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceAdventures
{
    public class GameManager : MonoBehaviour
{
    public Player player;
    public PlayerMovement playerMovement;

    public static GameManager Instance { get; private set; } // static singleton
    
    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }


    }
    }
