using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject deadBody;
    [SerializeField] private GameObject deathMenuUI;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private PlayerCombat playerCombat;

    [SerializeField] private AudioSource attackSource;
    [SerializeField] private AudioSource deathSource;
    [SerializeField] private AudioClip[] hurtClip;
    [SerializeField] private AudioClip[] deathClip;

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

        if (currentHealth > 0)
        {
            attackSource.clip = hurtClip[Random.Range(0, hurtClip.Length)];
            attackSource.Play();
        }
    }
    private void Die()
    {
        animator.SetTrigger("Death");
        Instantiate(deadBody, this.transform.position,this.transform.rotation);
        Destroy(gameObject);
        deathMenuUI.SetActive(true);

        deathSource.clip = deathClip[Random.Range(0, deathClip.Length)];
        deathSource.Play();
    }
}
