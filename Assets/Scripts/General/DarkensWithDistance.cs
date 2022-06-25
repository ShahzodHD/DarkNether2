using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class DarkensWithDistance : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;
    [SerializeField] private Light2D globalLight;
    private void Update()
    {
        float distance = Vector3.Distance(playerPosition.position, transform.position);
        globalLight.intensity = distance / 200;
    }
}
