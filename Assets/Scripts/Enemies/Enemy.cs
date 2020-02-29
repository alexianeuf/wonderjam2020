using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    /// <summary>
    ///
    /// Base for a following Enemy
    /// </summary>
    // [RequireComponent(typeof(NavMeshAgent))]
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
        
        protected bool isAttacking;

        protected Transform player;
        private bool isplayerNotNull = true;
        
        private NavMeshAgent navMeshAgent;

        // Start is called before the first frame update
        private void Start()
        {
        }

        void Awake()
        {
            player = GameObject.FindWithTag("Player").transform;
            navMeshAgent = GetComponent<NavMeshAgent>();

            if (player == null)
            {
                Debug.LogWarning("Please add a player to the scene so enemy can follow them.");
                isplayerNotNull = false;
            }
            

            if (navMeshAgent == null)
            {
                Debug.LogError("The enemy must have a NavMeshAgent");
            }

            navMeshAgent.speed = _speed;
            navMeshAgent.angularSpeed = _angularSpeed;
        }

        // Update is called once per frame
        void Update()
        {
            if (isplayerNotNull)
            {
                if (Vector3.Distance(player.position, transform.position) > _maxDistanceFromPlayer)
                {
                    Destroy(this.gameObject);
                    return;
                }
                
                FollowPlayer();
                
                bool maxDis = Vector3.Distance(player.position, transform.position) < _maxAttackDistance;
                // So the player has a chance to save himself
                bool minDis = Vector3.Distance(player.position, transform.position) > 1.0f;
                
                if (!isAttacking && maxDis && minDis)
                {
                    AttackPlayer();
                }
            }
        }

        private void FollowPlayer()
        {
            navMeshAgent.SetDestination(player.position);
        }

        protected abstract void AttackPlayer();
    }
}