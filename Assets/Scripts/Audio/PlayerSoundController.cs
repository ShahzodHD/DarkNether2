using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private AudioSource runningSourse;
    [SerializeField] private AudioSource jumpSourse;
    [SerializeField] private AudioSource landingSourse;

    [SerializeField] private AudioClip[] jumpClip;

    private bool _runControll = true;
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        if (rb.velocity.x > 0 || rb.velocity.x < 0)
        {
            if (_runControll == true)
            {
                runningSourse.Play();
                _runControll = false;
            }
        }
        else
        {
            runningSourse.Stop();
            _runControll = true;
        }
    }
    public void LandingSound()
    {
        landingSourse.Play();
    }
    public void JumpSound()
    {
        jumpSourse.clip = jumpClip[Random.Range(0, jumpClip.Length)];
        jumpSourse.Play();
    }
}
