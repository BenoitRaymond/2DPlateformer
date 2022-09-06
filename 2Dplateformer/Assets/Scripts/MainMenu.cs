using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene_Level1");
    }

    public void SelectLevel(int level)
    {
        string selectedLevel = "GameScene_Level" + level.ToString();
        SceneManager.LoadScene(selectedLevel);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
