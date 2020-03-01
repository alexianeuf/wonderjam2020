using System;
using Managers;
using UnityEngine;

namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] [Tooltip("EnemyPrefab to spawn")]
        private GameObject _enemyPrefab;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (FrenzyManager.isFrenzy)
                {
                    Instantiate(_enemyPrefab, transform.position, Quaternion.identity, transform);
                }       
            }
        }
    }
}