using System;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    /// <summary>
    ///
    /// Base for a following Enemy
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] [Tooltip("Enemy movement speed")]
        protected float _speed = 3.0f;

        [SerializeField] [Tooltip("Enemy rotation speed")]
        private float _angularSpeed = 120f;

        [SerializeField] [Tooltip("Set the max distance for attacking")]
        protected float _maxAttackDistance = 10.0f;

        [SerializeField] [Tooltip("Distance from player where the enemy is destroyed")]
        private float _maxDistanceFromPlayer = 50.0f;

        [SerializeField] [Tooltip("Damage done when attack success")]
        protected float _damage = 20.0f;

        protected bool IsAttacking;

        protected Transform Player;
        private bool isPlayerNotNull = true;

        protected NavMeshAgent NavMeshAgent;

        void Awake()
        {
            Player = GameObject.FindWithTag("Player").transform;
            NavMeshAgent = GetComponent<NavMeshAgent>();

            if (Player == null)
            {
                Debug.LogWarning("Please add a player to the scene so enemy can follow them.");
                isPlayerNotNull = false;
            }

            if (NavMeshAgent == null)
            {
                Debug.LogError("The enemy must have a NavMeshAgent");
            }

            NavMeshAgent.speed = _speed;
            NavMeshAgent.angularSpeed = _angularSpeed;
        }

        // Update is called once per frame
        void Update()
        {
            if (isPlayerNotNull)
            {
                if (Vector3.Distance(Player.position, transform.position) > _maxDistanceFromPlayer)
                {
                    Destroy(this.gameObject);
                    return;
                }

                FollowPlayer();

                bool maxDis = Vector3.Distance(Player.position, transform.position) < _maxAttackDistance;
                // So the player has a chance to save himself
                bool minDis = Vector3.Distance(Player.position, transform.position) > 1.0f;

                if (!IsAttacking && maxDis && minDis)
                {
                    AttackPlayer();
                }
            }
        }

        private void FollowPlayer()
        {
            NavMeshAgent.SetDestination(Player.position);
        }

        protected abstract void AttackPlayer();

        private void OnCollisionStay(Collision other)
        {
            if (other.collider.CompareTag("Player"))
                // Contact with the player the enemy doesn't need to move anymore
                NavMeshAgent.SetDestination(transform.position);
        }
    }
}