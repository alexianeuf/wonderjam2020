using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] [Tooltip("Player max health")]
    private float _maxHealth = 100.0f;

    [SerializeField] // TODO : remove this Serialize
    private float currentHealth;

    private void Awake()
    {
        currentHealth = _maxHealth;
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            // TODO : launch GameOver Screen
            Destroy(gameObject);
        }
    }
}