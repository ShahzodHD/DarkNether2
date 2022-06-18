using UnityEngine;
using System.Collections;

public class EnemyHealthSystem : MonoBehaviour
{
    [SerializeField] private EnemyAI enemyAI;

    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private AudioSource hurtSource;
    [SerializeField] private AudioClip hurtClip;
    [SerializeField] private AudioClip deathClip;

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

        if (currentHealth > 0)
        {
            hurtSource.clip = hurtClip;
            hurtSource.Play();
        }

        StartCoroutine("Hurt");
    }

    IEnumerator Hurt()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
        yield return null;
    }

    private void Die()
    {
        animator.SetTrigger("Death");
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EnemyAI>().enabled = false;

        enemyAI.runSourse.Stop();
        hurtSource.clip = deathClip;
        hurtSource.Play();

        Invoke("Faded", 6);
    }

    private void Faded()
    {
        Destroy(gameObject);
    }
}
