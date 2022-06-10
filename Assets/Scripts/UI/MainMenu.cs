using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int indexLevel;
    public void PlayGame()
    {
        SceneManager.LoadScene(indexLevel);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
