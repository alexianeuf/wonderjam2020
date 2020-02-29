﻿using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Items
{
    public class Barrel : MonoBehaviour
    {
        [SerializeField] [Tooltip("Percentage of fuel to restore")] [Range(25, 100)]
        private float _percentOfFuel;

        [SerializeField] [Tooltip("Sound launch when pick up")]
        private AudioClip _pickUpSound;

        private void OnTriggerEnter(Collider other)
        {
            GameObject player = other.transform.parent.gameObject;
            if (player.CompareTag("Player"))
            {
                PlayerFuelController fuelController = player.GetComponent<PlayerFuelController>();
                PlayerMovementController movementController = player.GetComponent<PlayerMovementController>();
                PlayerInput playerInput = player.GetComponent<PlayerInput>();
                
                AudioSource.PlayClipAtPoint(_pickUpSound, Camera.main.transform.position);

                if (!movementController.CanMove)
                    movementController.CanMove = true;

                if (playerInput.defaultActionMap == "UI")
                {
                    playerInput.defaultActionMap = "Player";
                    // TODO : deactivate game over screen ?
                }

                fuelController.RestoreFuel(_percentOfFuel);
                
                Destroy(gameObject);
            }
        }
    }
}
