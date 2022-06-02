using UnityEngine;

public class BossAI : EnemyAI
{
    public override void AttacKCount()
    {
        if (Vector2.Distance(transform.position, target.position) < distanceBeforePlayer && canAttacked == true)
        {
            animator.SetFloat("AttackInt", Random.Range(1, 5));
            animator.SetTrigger("Attack");
            animator.ResetTrigger("TakeDamage");

            canAttacked = false;
            _speed = 0;
        }
    }
}
