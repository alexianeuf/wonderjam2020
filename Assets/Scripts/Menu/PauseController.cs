using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject PauseUiGameObject;
    private bool isInPause = false;

    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnContinue()
    {
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
        }
        else
        {
            playerInput.actions.FindActionMap("UI").Disable();
            playerInput.actions.FindActionMap("Player").Enable();
        }
    }
}
