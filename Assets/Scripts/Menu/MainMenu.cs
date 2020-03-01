using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject m_mainMenu;
    [SerializeField]
    private GameObject m_creditMenu;
    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void DisplayCredit()
    {
        m_mainMenu.SetActive(false);
        m_creditMenu.SetActive(true);
        
//        m_creditMenu.GetComponentInChildren<EventSystem>().SetSelectedGameObject(m_creditMenu.GetComponentInChildren<EventSystem>().firstSelectedGameObject);
        m_creditMenu.GetComponentInChildren<EventSystem>().UpdateModules();
    }

    public void GoToMainMenu()
    {
//        m_mainMenu.SetActive(true);
//        m_creditMenu.SetActive(false);
//        
////        m_mainMenu.GetComponentInChildren<EventSystem>().SetSelectedGameObject(m_mainMenu.GetComponentInChildren<EventSystem>().firstSelectedGameObject);
//        m_mainMenu.GetComponentInChildren<EventSystem>().UpdateModules();

        SceneManager.LoadScene(0);
    }
}
