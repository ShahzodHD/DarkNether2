using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemControll : MonoBehaviour
{
    [SerializeField] static bool GameIsPaused = false;
    [SerializeField] GameObject pauseMenuUI;
    private void Start()
    {
        Time.timeScale = 1f;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused == true)
            {
                Remuse();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Remuse()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void EndGame()
    {
        print("game end");
    }
}
