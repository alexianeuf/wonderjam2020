using System;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Rocket : MonoBehaviour
    {
        public Vector3 Destination;

        [SerializeField] [Tooltip("Rocket speed")]
        private float _speed = 100f;

        public float Damage;

        
        // Start is called before the first frame update
        void Start()
        {
            // TODO :Set the movement to the destination
            GetComponent<Rigidbody>().MovePosition(Destination);
            Destroy(gameObject, 5.0f);
        }

        // Update is called once per frame
        void Update()
        {
            // FOR THE TRIGGER
            // GetComp ParticleSystem ().Play();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponentInParent<PlayerHealthController>().Damage(Damage);
                ParticleSystem particleSystem = GetComponentInChildren<ParticleSystem>();

                float duration = 0;
                if (particleSystem != null)
                {
                    particleSystem.Play();
                    duration = particleSystem.main.duration;
                }

                Destroy(gameObject, duration);
            }
        }
    }
}
