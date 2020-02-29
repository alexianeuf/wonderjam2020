using System;
using UnityEngine;

namespace Items
{
    public class Medikit : MonoBehaviour
    {
        [SerializeField] [Tooltip("Percent of health to give back")] [Range(20, 100)]
        private float _healingPercent;

        [SerializeField] [Tooltip("Sound launch when item is collect")]
        private AudioClip _pickUpItemSound;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameObject player = other.transform.parent.gameObject;
                PlayerHealthController playerHealthController = player.GetComponent<PlayerHealthController>();
                AudioSource.PlayClipAtPoint(_pickUpItemSound, transform.position, 200);

                if (playerHealthController != null)
                    playerHealthController.Heal(_healingPercent);    
                
                Destroy(gameObject);
            }
        }
    }
}