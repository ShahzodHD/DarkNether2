using System.Collections;
using UnityEngine;

public class ChangeGravityScale : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    private IEnumerator ChangeGravityScaleAttackInDownTop()
    {
        _rigidbody2D.gravityScale = 5000;
        yield return new WaitForSeconds(0.1f);
        _rigidbody2D.gravityScale = 3;
    }

    private IEnumerator ChangeGravityScaleAttackTopDown()
    {
        yield return new WaitForSeconds(0.2f);
        _rigidbody2D.gravityScale = 50;
        yield return new WaitForSeconds(0.1f);
        _rigidbody2D.gravityScale = 3;
    }
}
