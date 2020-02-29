using System;
using System.Collections;
using UnityEngine;

public class PlayerFrenzyController : MonoBehaviour
{
    [SerializeField] [Tooltip("Max frenzy level")]
    private float _maxFrenzyLevel = 100f;

    [SerializeField] [Tooltip("Level of decrease of frenzy current level every 5 seconds")]
    private float _frenzyDecrease = 2.5f;

    [SerializeField] [Tooltip("Level of increase when the player hit an innocent")]
    private float _frenzyIncrease = 10;

    [SerializeField] private float currentFrenzyLevel;

    public float FrenzyLevel => currentFrenzyLevel;
    public float MaxFrenzyLevel => _maxFrenzyLevel;

    private bool isInDecrease;

    public void IncreaseFrenzyLevel()
    {
        currentFrenzyLevel += _frenzyIncrease;

        if (currentFrenzyLevel > _maxFrenzyLevel)
        {
            currentFrenzyLevel = _maxFrenzyLevel;
            // TODO : if not in Frenzy mode enable it
            Debug.Log("Oh no ! I killed to many people !");
        }
    }

    private void Update()
    {
        if (!isInDecrease && currentFrenzyLevel > 0)
        {
            StartCoroutine(DecreaseFrenzy());
        }
    }

    private IEnumerator DecreaseFrenzy()
    {
        isInDecrease = true;
        currentFrenzyLevel -= _frenzyDecrease;

        if (currentFrenzyLevel < 0)
        {
            currentFrenzyLevel = 0;
            // TODO : if in Frenzy mode disable it
            Debug.Log("I've been a nice person, I'm better now !");
        }

        yield return new WaitForSeconds(5.0f);
        
        isInDecrease = false;
    }
}