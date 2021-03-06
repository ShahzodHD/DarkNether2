using UnityEngine;
using System.Collections;

public class EnemyHealthSystem : MonoBehaviour
{
    [SerializeField] private EnemyAI enemyAI;

    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color hurtColor;
    [SerializeField] private GameObject hurtEffect;
    [SerializeField] private Transform hurtEffectPosition;

    [SerializeField] private AudioSource hurtSource;
    [SerializeField] private AudioClip[] hurtClip;
    [SerializeField] private AudioClip[] deathClip;

    [SerializeField] private int maxHealth = 100;

    private int currentHealth;
    private Color defaultSpriteColor;
    private void Start()
    {
        currentHealth = maxHealth;
        Physics2D.IgnoreLayerCollision(6, 10);
        defaultSpriteColor = spriteRenderer.color;
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
            hurtSource.clip = hurtClip[Random.Range(0, hurtClip.Length)];
            hurtSource.Play();
        }

        StartCoroutine("Hurt");
    }

    IEnumerator Hurt()
    {
        Instantiate(hurtEffect, hurtEffectPosition.position, Quaternion.identity);
        spriteRenderer.color = hurtColor;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = defaultSpriteColor;
        yield return null;
    }

    private void Die()
    {
        animator.SetTrigger("Death");
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EnemyAI>().enabled = false;

        enemyAI.runSourse.Stop();
        hurtSource.clip = deathClip[Random.Range(0, deathClip.Length)];
        hurtSource.Play();

        Invoke("Faded", 6);
    }

    private void Faded()
    {
        Destroy(gameObject);
    }
}
