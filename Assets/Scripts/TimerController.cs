using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public int maxTime = 30;
    public int currentTime;
    private IEnumerator coroutine = null;
    
    void Start()
    {
        currentTime = maxTime;
    }

    IEnumerator Timer()
    {
        while(currentTime > 0)
        {
            yield return new WaitForSeconds(1);
            currentTime--;
        }
       
        GameManager.instance.GetComponent<MenuController>().LaunchGameOverMenu(DeathCause.TimeOut);
    }

    public void ResetTimer()
    {
        if(coroutine != null)
            StopCoroutine(coroutine);
        
        coroutine = Timer();
        currentTime = maxTime;
        StartCoroutine(coroutine);
    }
}
