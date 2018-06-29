using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class BackButonAction : MonoBehaviour
    {
        public void BackToMainMenu()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                if (Input.GetKey(KeyCode.Escape))
                {
                    SceneManager.LoadScene(0);

                    return;
                }
            }
        }
    }
}