using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static GameObject instance;
    void Start()
    {
        HUD.HUDInstance.InitHUD();
    }

    private void Awake()
    {
        instance = this.gameObject;
    }
}
