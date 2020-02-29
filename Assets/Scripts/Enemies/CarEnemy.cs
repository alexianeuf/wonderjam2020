﻿using System;
using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class CarEnemy : Enemy
    {
        private float previousSpeed;
        private bool hasCharged;

        [SerializeField] [Tooltip("Speed when charge the player")]
        private float _chargeSpeed = 5.0f;
        
        protected override void AttackPlayer()
        {
            // if speed is not the chargeSpeed
            if (!hasCharged && Math.Abs(_chargeSpeed - _speed) > 0.01)
            {
                // Set the attack speed
                isAttacking = true;
                previousSpeed = _speed;
                _speed = _chargeSpeed;
            }
            else if(Vector3.Distance(player.position, transform.position) > _maxAttackDistance)
            {
                // The enemy goes back to its initial speed
                _speed = previousSpeed;
                previousSpeed = _chargeSpeed;
                isAttacking = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            StartCoroutine(ReduceSpeed());
            // Damage the player
        }

        private IEnumerator ReduceSpeed()
        {
            hasCharged = true;
            _speed = previousSpeed / 2.0f;
            yield return new WaitForSeconds(2f);
            _speed = previousSpeed;
            previousSpeed = _chargeSpeed;
            hasCharged = false;
            isAttacking = false;
        }
    }
}