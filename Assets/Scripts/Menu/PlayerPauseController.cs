using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPauseController : MonoBehaviour
{
    public void OnPause()
    {
        GameManager.instance.GetComponent<MenuController>().OnPauseRequested();
    }
}
