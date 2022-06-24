using UnityEngine;
using System.Collections;

public class VillagerAI : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private float speed;
    [SerializeField] private float delayRight;
    [SerializeField] private float delayLeft;

    [Header("Patrol between two position")]
    [SerializeField] private Vector3 pointA;
    [SerializeField] private Vector3 pointB;

    private bool RunLeft;
    private bool RunRight;

    private bool facingRight = true;
    private bool facingLeft = true;
    private void Start()
    {
        RunLeft = true;
    }
    private void Update()
    {
        StartCoroutine("Patrol");
    }
    IEnumerator Patrol()
    {
        if (RunLeft == true && RunRight == false) { StartCoroutine("PatrolLeft"); }
        if (RunRight == true && RunLeft == false) { StartCoroutine("PatrolRight"); }
        if (transform.position == pointA)
        {
            animator.SetBool("IsRunning", false);
            yield return new WaitForSeconds(delayLeft);
            animator.SetBool("IsRunning", true);
            FlipRight();
            RunLeft = false;
            RunRight = true;
        }
        if (transform.position == pointB)
        {
            animator.SetBool("IsRunning", false);
            yield return new WaitForSeconds(delayRight);
            animator.SetBool("IsRunning", true);
            FlipLeft();
            RunRight = false;
            RunLeft = true;
        }
        yield return null;
    }
    IEnumerator PatrolLeft()
    {
        transform.position = Vector3.MoveTowards(transform.position, pointA, speed * Time.deltaTime);
        yield return null;
    }
    IEnumerator PatrolRight()
    {
        transform.position = Vector3.MoveTowards(transform.position, pointB, speed * Time.deltaTime);
        yield return null;
    }
    private void FlipRight()
    {
        if (facingRight == true)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;

            facingRight = false;
            facingLeft = true;
        }
    }
    private void FlipLeft()
    {
        if (facingLeft == true)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;

            facingLeft = false;
            facingRight = true;
        }
    }
}
