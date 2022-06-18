using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScene : MonoBehaviour
{
    [SerializeField] private int transitionScene;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private VectorValue playerStorage;

    [SerializeField] private bool firstStart = true;

    private void Start()
    {
        if (firstStart == true)
        {
            playerStorage.initialValue = new Vector3(-7, 1, -1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        firstStart = false;
        playerStorage.initialValue = spawnPosition;
        SceneManager.LoadScene(transitionScene);
    }
}
