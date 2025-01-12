using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance { get; private set; }
    public bool canLoadNextLevel = false;
    public bool shortCutsEnabled = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadLevelByName(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void LoadLevelByIndex(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadPreviousLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    private void Update()
    {
        if (!shortCutsEnabled)
            return;
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
            return;
        }

        if (canLoadNextLevel)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                canLoadNextLevel = false;
                LoadNextLevel();
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadLevelByIndex(0);
            return;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            LoadPreviousLevel();
            return;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            LoadNextLevel();
            return;
        }
    }
}