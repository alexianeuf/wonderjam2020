﻿using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] [Tooltip("Player max health")]
    public float _maxHealth = 100.0f;
    [SerializeField]
    public float currentHealth;

    private void Awake()
    {
        currentHealth = _maxHealth;
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        PerlinCameraShake.instance.Trauma = 0.5f;

        if (currentHealth < 0)
        {
            GameManager.instance.GetComponent<MenuController>().LaunchGameOverMenu(DeathCause.LowHealth);
        }
    }

    public void Heal(float percent)
    {
        currentHealth += _maxHealth * (percent / 100);
        
        if (currentHealth > _maxHealth)
            currentHealth = _maxHealth;
    }

    public bool IsPlayerAlive()
    {
        return currentHealth > 0;
    }
}