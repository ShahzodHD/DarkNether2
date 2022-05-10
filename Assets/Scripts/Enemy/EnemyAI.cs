using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float distanceBeforePlayer;
    [SerializeField] private float globalDistance;
    [SerializeField] private float coolDown;
    [SerializeField] private int damage;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask playerLayers;
    [SerializeField] private Transform target;

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Vector2 sizeRange;
    [SerializeField] private float angleRange;

    private bool canAttacked = true;
    private float _speed;
    private void Start()
    {
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
            transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
        }
    }
    private void Flip()
    {
        if (transform.position.x < target.position.x)
        {
            transform.localScale = new Vector3(2, 2, 2);
        }
        else
        {
            transform.localScale = new Vector3(-2, 2, 2);
        }
    }
    private void AttacKCount()
    {
        if (Vector2.Distance(transform.position, target.position) < distanceBeforePlayer && canAttacked == true) 
        {
            animator.SetTrigger("Attack");
            canAttacked = false;
            _speed = 0;
        }
    }
    private void EndAttack()
    {
        Invoke("Overcharge", coolDown);
        _speed = speed;
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
