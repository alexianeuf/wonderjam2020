using System;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(AudioSource))]
    public class Rocket : MonoBehaviour
    {
        public GameObject Target;

        [SerializeField] [Tooltip("Rocket speed")]
        private float _speed = 10f;

        [SerializeField] [Tooltip("Sound when exploded")]
        private AudioClip _explosionClip;

        public float Damage;

        private Rigidbody rig;
        private AudioSource audioSource;
        
        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            rig = GetComponent<Rigidbody>();
            Destroy(gameObject, 5.0f);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector3 heading = Target.transform.position - transform.position;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance;
            Vector3 tempVect = direction.normalized * (_speed * Time.fixedDeltaTime);
            
            rig.MovePosition(transform.position + tempVect);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponentInParent<PlayerHealthController>().Damage(Damage);
                ParticleSystem particleSystem = GetComponentInChildren<ParticleSystem>();

                float duration = 0;
                audioSource.Stop();
                audioSource.volume = 1.0f;
                audioSource.clip = _explosionClip;
                audioSource.Play();
                
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
