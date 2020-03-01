using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class ApcEnemy : Enemy
    {
        [SerializeField] [Tooltip("Rocket to fire")]
        private GameObject _rocketPrefab;

        protected override void AttackPlayer()
        {
            // Reduce the firerate
            ParticleSystem particleSystem = transform.GetComponentInChildren<ParticleSystem>();
            
            GameObject rocketObject = Instantiate(_rocketPrefab, transform.position + new Vector3(0, 3.152f, 0), new Quaternion(0, -90, 0,0));
            
            // Set the rocket values
            Rocket rocket = rocketObject.GetComponent<Rocket>();
            rocket.Target = Player.gameObject;
            rocket.Damage = _damage;
            
            AudioSource.PlayClipAtPoint(_attackClip, Camera.main.transform.position);
            if(particleSystem != null)
                particleSystem.Play();

            IsAttacking = true;
            StartCoroutine(AttackPause());
        }

        private IEnumerator AttackPause()
        {
            yield return new WaitForSeconds(5.0f);
            IsAttacking = false;
        }
    }
}
