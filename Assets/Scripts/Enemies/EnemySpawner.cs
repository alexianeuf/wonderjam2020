using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] [Tooltip("EnemyPrefab to spawn")]
        private List<GameObject> _enemiesPrefab;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (FrenzyManager.isFrenzy)
                {
                    int index = Random.Range(0, _enemiesPrefab.Count - 1);
                        
                    Instantiate(_enemiesPrefab[index], transform.position, Quaternion.identity, transform);
                }       
            }
        }
    }
}