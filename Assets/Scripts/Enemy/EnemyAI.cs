using UnityEngine;

public class EnemyAI : MonoBehaviour
{   
    /// <summary>
    /// 15.05.22 »справить 2 бага с догом
    /// 1) скорость увеличиваетс€ после 1 удара
    /// 2) правильно настроить коллайдеры и чекбоксы собаки
    /// </summary>
    [SerializeField] private float speed;
    [SerializeField] private float distanceBeforePlayer;
    [SerializeField] private float globalDistance;
    [SerializeField] private float coolDown;
    [SerializeField] private int damage;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask playerLayers;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 flipRight;
    [SerializeField] private Vector3 flipLeft;

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Vector2 sizeRange;
    [SerializeField] private float angleRange;

    private bool canAttacked = true;
    private float _speed;
    private void Start()    
    {
        Physics2D.IgnoreLayerCollision(10, 10, true);

        _speed = speed;
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position,target.position) < globalDistance){ Follow(); }

        Flip();
        AttacKCount();
    }
    private void Follow()
    {
        if (rb.velocity.magnitude > 0)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
        if (Vector2.Distance(transform.position, target.position) > distanceBeforePlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
        }
    }
    private void Flip()
    {
        if (transform.position.x < target.position.x)
        {
            transform.localScale = new Vector3(flipRight.x, flipRight.y, flipRight.z);
        }
        else
        {
            transform.localScale = new Vector3(flipLeft.x, flipLeft.y, flipLeft.z);
        }
    }
    private void AttacKCount()
    {
        if (Vector2.Distance(transform.position, target.position) < distanceBeforePlayer && canAttacked == true) 
        {
            animator.SetTrigger("Attack");
            animator.ResetTrigger("TakeDamage");

            canAttacked = false;
            _speed = 0;
        }
    }
    private void EndAttack()
    {
        Invoke("Overcharge", coolDown);
        _speed = speed;

        animator.ResetTrigger("TakeDamage");
    }
    private void Overcharge()
    {
        canAttacked = true;
    }

    private void Attack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapBoxAll(attackPoint.position, sizeRange, angleRange, playerLayers);

        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerHealthSystem>().TakeDamage(damage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireCube(attackPoint.position, sizeRange);
    }
}
