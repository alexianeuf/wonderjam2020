﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject instance;
    void Start()
    {
        instance = gameObject;
    }
}
