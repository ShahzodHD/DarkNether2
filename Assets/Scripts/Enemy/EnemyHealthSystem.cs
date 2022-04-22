using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private int currentHealth;
    private void Start()
    {
        currentHealth = maxHealth;
        Physics2D.IgnoreLayerCollision(6, 10);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Enemy died!");
    }
}
