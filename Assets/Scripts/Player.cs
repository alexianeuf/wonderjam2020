using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static GameObject instance;
    void Start()
    {
        instance = this.gameObject;
        HUD.HUDInstance.InitHUD();
    }
}
