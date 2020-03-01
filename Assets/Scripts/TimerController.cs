using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public int maxTime = 30;
    public int currentTime;
    private IEnumerator coroutine;
    
    void Start()
    {
        currentTime = maxTime;
        coroutine = Timer();
    }

    IEnumerator Timer()
    {
        while(currentTime > 0)
        {
            yield return new WaitForSeconds(1);
            currentTime--;
        }
       
        GameManager.instance.GetComponent<MenuController>().LaunchGameOverMenu();
    }

    public void ResetTimer()
    {
        StopCoroutine(coroutine);
        coroutine = Timer();
        currentTime = maxTime;
        StartCoroutine(coroutine);
    }
}
