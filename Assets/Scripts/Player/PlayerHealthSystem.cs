using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject deadBody;

    [SerializeField] private int maxHealth = 100;

    private int currentHealth;
    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        print(damage);
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        animator.SetTrigger("Death");
        Instantiate(deadBody, this.transform.position,this.transform.rotation);
        Destroy(gameObject);
    }
}
