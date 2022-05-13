using UnityEngine;

public class ParallaxBehaviour : MonoBehaviour
{
    [SerializeField] private Transform followingTarget;
    [SerializeField, Range(0f, 1f)] float parallaxStrength = 0.1f;
    [SerializeField] bool disableVecticalParallax;
    private Vector3 targetPreviousPosition;
    private void Start()
    {
        if (!followingTarget)
        {
            followingTarget = Camera.main.transform;
        }
        targetPreviousPosition = followingTarget.position;
    }
    private void Update()
    {
        var delta = followingTarget.position - targetPreviousPosition;
        if (disableVecticalParallax == true) 
        {
            delta.y = 0f;
        }
        targetPreviousPosition = followingTarget.position;
        transform.position += delta * parallaxStrength;
    }
}
