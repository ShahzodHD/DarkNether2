using UnityEngine;

public class EnemyAI : MonoBehaviour
{   
    [SerializeField] private float speed;
    [SerializeField] private protected float distanceBeforePlayer;
    [SerializeField] private float globalDistance;
    [SerializeField] private float coolDown;
    [SerializeField] private int damage;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask playerLayers;
    [SerializeField] private protected Transform target;
    [SerializeField] private Vector3 flipRight;
    [SerializeField] private Vector3 flipLeft;

    [SerializeField] private protected Animator animator;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Vector2 sizeRange;
    [SerializeField] private float angleRange;

    private protected bool canAttacked = true;
    private protected float _speed;
    private protected void Start()    
    {
        Physics2D.IgnoreLayerCollision(10, 10, true);

        rb = GetComponent<Rigidbody2D>();

        target = GameObject.Find("Player").transform;

        _speed = speed;
    }
    private protected void Update()
    {
        if (Vector2.Distance(transform.position,target.position) < globalDistance){ Follow(); }

        Flip();
        AttacKCount();
    }
    private protected void Follow()
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
    private protected void Flip()
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
    public virtual void AttacKCount()
    {
        if (Vector2.Distance(transform.position, target.position) < distanceBeforePlayer && canAttacked == true) 
        {
            animator.SetTrigger("Attack");
            animator.ResetTrigger("TakeDamage");

            canAttacked = false;
            _speed = 0;
        }
    }
    private protected void EndAttack()
    {
        Invoke("Overcharge", coolDown);
        _speed = speed;

        animator.ResetTrigger("TakeDamage");
    }
    private protected void Overcharge()
    {
        canAttacked = true;
    }

    private protected void Attack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapBoxAll(attackPoint.position, sizeRange, angleRange, playerLayers);

        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerHealthSystem>().TakeDamage(damage);
        }
    }
    private protected void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireCube(attackPoint.position, sizeRange);
    }
}
