using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance { get; private set; }
    public bool canLoadNextLevel = false;

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

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadLevelByIndex(1);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            LoadLevelByIndex(2);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            LoadLevelByIndex(3);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            LoadLevelByIndex(4);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            LoadLevelByIndex(5);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            LoadLevelByIndex(6);
            return;
        }
    }
}