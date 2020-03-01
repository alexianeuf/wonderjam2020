using Managers;
using System;
using System.Collections;
using PostProcess;
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

    [SerializeField] [Tooltip("Post process to update")]
    private RedVision _redVision;

    public float FrenzyLevel => currentFrenzyLevel;
    public float MaxFrenzyLevel => _maxFrenzyLevel;

    private bool isInDecrease;

    private void Start()
    {
        Radio.instance.CalmMode();
    }

    public void IncreaseFrenzyLevel()
    {
        currentFrenzyLevel += _frenzyIncrease;

        if (currentFrenzyLevel > _maxFrenzyLevel)
        {
            currentFrenzyLevel = _maxFrenzyLevel;

            _redVision.UpdatePostProcess(_maxFrenzyLevel);
            
            if (!FrenzyManager.isFrenzy)
                FrenzyManager.instance.OnFrenzyStart();
        }
    }

    private void Update()
    {
        if (!isInDecrease && currentFrenzyLevel > 0)
        {
            StartCoroutine(DecreaseFrenzy());
        }

        if (!FrenzyManager.isFrenzy)
            _redVision.UpdatePostProcess(currentFrenzyLevel, _maxFrenzyLevel);
    }

    private IEnumerator DecreaseFrenzy()
    {
        isInDecrease = true;
        currentFrenzyLevel -= _frenzyDecrease;

        if (currentFrenzyLevel < 0)
        {
            currentFrenzyLevel = 0;
            if (FrenzyManager.isFrenzy)
                FrenzyManager.instance.OnFrenzyExit();
        }

        yield return new WaitForSeconds(5.0f);

        isInDecrease = false;
    }
}