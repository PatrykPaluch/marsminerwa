using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Marsminerwa
{
    public class Menu : MonoBehaviour
    {

        [SerializeField]
        private TMP_Text scoreText;
        [SerializeField]
        private TMP_Text scoreBestText;
        
        private void Start()
        {
            Time.timeScale = 1;
            scoreText.text = GamePrefs.GetScore().ToString();
            scoreBestText.text = GamePrefs.GetBestScore().ToString();
        }

        public void StartButton()
        {
            SceneManager.LoadScene("Scenes/SampleScene");
        }

        public void ExitButton()
        {
            Application.Quit();
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }

    }
}