﻿using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD HUDInstance;

    [SerializeField] private Slider m_healthSlider;
    [SerializeField] private CircleJauge m_fuelSlider;
    [SerializeField] private Text m_timerText;
    [SerializeField] private Text m_scoreText;

    private PlayerHealthController healthComponent;
    private PlayerFuelController fuelComponent;
    private TimerController timerComponent;
    private PlayerScoreController scoreComponent;
    
    
    private void Awake()
    {
        HUDInstance = this;
    }

    public void InitHUD()
    {
        healthComponent = Player.instance.GetComponent<PlayerHealthController>();
        fuelComponent = Player.instance.GetComponent<PlayerFuelController>();
        timerComponent = GameManager.instance.GetComponent<TimerController>();
        scoreComponent = Player.instance.GetComponent<PlayerScoreController>();

        m_healthSlider.maxValue = healthComponent._maxHealth;
        m_healthSlider.value = healthComponent.currentHealth;

        m_fuelSlider.UpdateValue(fuelComponent.currentFuelQuantity, fuelComponent._maxFuelQuantity);
    }

    // Update is called once per frame
    void Update()
    {
        m_healthSlider.value = healthComponent.currentHealth;
        m_fuelSlider.UpdateValue(fuelComponent.currentFuelQuantity, fuelComponent._maxFuelQuantity);
        m_timerText.text = timerComponent.currentTime.ToString() + "''";
        m_scoreText.text = scoreComponent.m_score.ToString();
    }
}