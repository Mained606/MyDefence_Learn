using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace MyDefence
{
    public class LevelClearUI : MonoBehaviour
    {
        public SceneFader fader;

        [SerializeField] private string nextLevel = "Level02";
        [SerializeField] private string loadToScene = "MainMenu";
        public void Continue()
        {
            fader.FadeTo(nextLevel);
        }

        public void Menu()
        {
            fader.FadeTo(loadToScene);
        }
    }

}
