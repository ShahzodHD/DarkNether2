using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 400f;

    [Range(0f, 1f)][SerializeField] private float crouchSpeed = .36f;
    [Range(0, .3f)][SerializeField] private float movementSmoothing = .05f;

    [SerializeField] private bool airControl = false;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private Collider2D crouchDisableCollider;
    [SerializeField] private Animator animator;

    const float groundedRadios = .2f;
    const float ceilingRadius = .2f;
    private bool grounded;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private Vector3 velocity = Vector3.zero;

    [Header("Events")]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent onCrouchEvent;
    private bool wasCrouching = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }
        if (onCrouchEvent == null)
        {
            onCrouchEvent = new BoolEvent();
        }

        Physics.IgnoreLayerCollision(10, 6, true);
    }

    private void FixedUpdate()
    {
        bool wasGrounded = grounded;
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadios, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                }
            }
        }
    }

    public void Move(float move, bool crouch, bool jump)
    {
        if (!crouch)
        {
            if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround))
            {
                crouch = true;
            }
        }

        if (grounded || airControl)
        {
            if (crouch)
            {
                if (!wasCrouching)
                {
                    wasCrouching = true;
                    onCrouchEvent.Invoke(true);
                }

                move *= crouchSpeed;

                if (crouchDisableCollider != null)
                {
                    crouchDisableCollider.enabled = false;
                }
            }
            else
            {
                if (crouchDisableCollider != null)
                {
                    crouchDisableCollider.enabled = true;
                }
                if (wasCrouching)
                {
                    wasCrouching = false;
                    onCrouchEvent.Invoke(false);
                }
            }

            Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

            if (move > 0 && !facingRight)
            {
                Flip();
            }
            else if (move < 0 && facingRight)
            {
                Flip();
            }
        }

        if (grounded && jump)
        {
            grounded = false;
            rb.AddForce(new Vector2(0f, jumpForce));
        }
        if (move > 0 || move < 0) 
        {
            animator.SetBool("IsWalking", true);
        }
        if (move == 0) 
        {
            animator.SetBool("IsWalking", false);
        }
    }
    
    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
