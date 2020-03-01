﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject PauseUiGameObject;
    public GameObject GameOverUiGameObject;
    private bool isInPause = false;

    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(0);
    }

    public void OnContinue()
    {
        Time.timeScale = 1f;

        OnPauseRequested();
    }

    public void OnPauseRequested()
    {
        isInPause = !isInPause; 
        PauseUiGameObject.SetActive(isInPause);

        var playerInput = Player.instance.GetComponent<PlayerInput>();

        if (isInPause)
        {
            playerInput.actions.FindActionMap("UI").Enable();
            playerInput.actions.FindActionMap("Player").Disable();

            Time.timeScale = 0f;
        }
        else
        {
            playerInput.actions.FindActionMap("UI").Disable();
            playerInput.actions.FindActionMap("Player").Enable();
            
            Time.timeScale = 1f;
        }
    }

    public void LaunchGameOverMenu()
    {
        GameOverUiGameObject.SetActive(true);
        
        Time.timeScale = 0f;

        var playerInput = Player.instance.GetComponent<PlayerInput>();

        playerInput.actions.FindActionMap("UI").Enable();
        playerInput.actions.FindActionMap("Player").Disable();
    }
}