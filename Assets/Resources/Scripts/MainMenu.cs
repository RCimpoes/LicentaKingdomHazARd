using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class MainMenu : MonoBehaviour {

        public GameObject levelSelectPanel;

        private bool isPanelVisible = false;

        public void LoadLevel1()
        {
            SceneManager.LoadScene(1);

        }

        public void LoadLevel2()
        {
            SceneManager.LoadScene(2);

        }

        public void ShowHideLevelSelect()
        {
            isPanelVisible = !isPanelVisible;
            levelSelectPanel.SetActive(isPanelVisible);
            

        }

        public void QuitGame()
        {
            Debug.Log("PAaa!");
            Application.Quit();
        }

    }
}
