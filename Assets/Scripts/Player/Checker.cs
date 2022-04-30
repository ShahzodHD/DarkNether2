using UnityEngine;

public class Checker : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Animator _animator;

    private void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("AttackInDownTop"))
        {
            _rigidbody2D.gravityScale = 5000;
            Invoke("ReturnMass", 0.1f);
        }
    }

    private void ReturnMass()
    {
        _rigidbody2D.gravityScale = 3;
    }
}
