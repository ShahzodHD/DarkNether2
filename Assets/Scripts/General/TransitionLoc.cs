using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionLoc : MonoBehaviour
{
    [SerializeField] private GameObject buttonE;
    [SerializeField] private int location;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            buttonE.SetActive(true);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(location);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            buttonE.SetActive(false);
        }
    }
}
