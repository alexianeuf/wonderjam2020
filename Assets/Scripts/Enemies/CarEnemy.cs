using System;
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

        [SerializeField] [Tooltip("Sound launch when hit the player")]
        private AudioClip _collidSound;

        protected override void AttackPlayer()
        {
            // if speed is not the chargeSpeed
            if (!hasCharged && Math.Abs(_chargeSpeed - _speed) > 0.01)
            {
                // Set the attack speed
                IsAttacking = true;
                previousSpeed = _speed;
                _speed = _chargeSpeed;
            }
            else if (Vector3.Distance(Player.position, transform.position) > _maxAttackDistance)
            {
                // The enemy goes back to its initial speed
                _speed = previousSpeed;
                previousSpeed = _chargeSpeed;
                IsAttacking = false;
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                AudioSource.PlayClipAtPoint(_collidSound, other.transform.position, 1.0f);
                GameObject player = other.transform.parent.gameObject;
                PlayerHealthController playerHealthController = player.GetComponent<PlayerHealthController>();

                if (playerHealthController != null)
                    playerHealthController.Damage(_damage);

                NavMeshAgent.SetDestination(transform.position);

                StartCoroutine(ReduceSpeed());
            }

            // Damage the player
        }

        private IEnumerator ReduceSpeed()
        {
            hasCharged = true;
            if (previousSpeed > 0)
                _speed = previousSpeed / 2.0f;
            yield return new WaitForSeconds(2f);
            _speed = previousSpeed;
            previousSpeed = _chargeSpeed;
            hasCharged = false;
            IsAttacking = false;
        }
    }
}