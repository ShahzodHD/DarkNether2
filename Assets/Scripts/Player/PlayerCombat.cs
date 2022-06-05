using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private float attackRange;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackRate;
    [SerializeField] private float nextAttackTime;

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerController playerController;

    public bool attackCtrl;
    private void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                AttackAnim();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
    private void AttackAnim()
    {
        anim.SetTrigger("Attack");
        playerMovement.runSpeed = 0;
    }
    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealthSystem>().TakeDamage(attackDamage);
        }

        Invoke("AfterAttack", 0.3f);
    }
    public void AfterAttack() 
    { 
        playerMovement.runSpeed = 25;
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
