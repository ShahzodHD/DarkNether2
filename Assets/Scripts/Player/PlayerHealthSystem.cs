using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject deadBody;
    [SerializeField] private GameObject deathMenuUI;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private PlayerCombat playerCombat;

    [SerializeField] private int maxHealth = 100;

    private int currentHealth;
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }

        animator.SetTrigger("TakeDamage");
        playerCombat.AfterAttack();
    }
    private void Die()
    {
        animator.SetTrigger("Death");
        Instantiate(deadBody, this.transform.position,this.transform.rotation);
        Destroy(gameObject);
        deathMenuUI.SetActive(true);
    }
}
