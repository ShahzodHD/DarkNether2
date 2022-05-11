using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    [SerializeField] private Animator animator;

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

        animator.SetTrigger("TakeDamage");
    }
    private void Die()
    {
        animator.SetTrigger("Death");
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EnemyAI>().enabled = false;
        Invoke("Faded", 6);
    }

    private void Faded()
    {
        Destroy(gameObject);
    }
}
