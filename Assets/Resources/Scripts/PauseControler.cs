using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseControler : MonoBehaviour {

    public GameObject PauseMenu;
    public GameObject PauseButton;

    public Scene menuScene;
    public Scene Level1Scene;
    public Scene Level2Scene;
   

    public static bool pause;

    private void Start()
    {
        pause = true;
    }

    public void HideShowPauseMenu()
    {
      
        if (pause)
        {
            Time.timeScale = 0;
            
        }
        else if (!pause)
        {
            Time.timeScale = 1;

        }

        PauseMenu.SetActive(pause);
        PauseButton.SetActive(!pause);
        pause = !pause;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
        int  currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.UnloadSceneAsync(currentSceneIndex);
    }
}
