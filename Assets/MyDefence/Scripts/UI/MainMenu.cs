using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyDefence
{
    public class MainMenu : MonoBehaviour
    {
        #region Variables
        public SceneFader fader;
        [SerializeField] private string loadToScene = "LevelSelect";
        #endregion

        public void Play()
        {
            //Debug.Log("Goto Play Scene");
            fader.FadeTo(loadToScene);
        }

        public void Quit()
        {
            //Cheating - 나중에 제거
            PlayerPrefs.DeleteAll();

            Debug.Log("Game Quit");
            Application.Quit();

        }
    }
}
