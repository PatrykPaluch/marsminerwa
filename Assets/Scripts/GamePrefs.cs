using UnityEngine;

namespace Marsminerwa
{
    public static class GamePrefs
    {
        public const string KeyLastScore = "LastScore";
        public const string KeyBestScore = "BestScore";

        public static int GetScore()
        {
            return PlayerPrefs.GetInt(KeyLastScore, 0);
        }

        public static void SetScore(int value)
        {
            PlayerPrefs.SetInt(KeyLastScore, value);
            SetBestScore(value);
        }

        public static int GetBestScore()
        {
            return PlayerPrefs.GetInt(KeyBestScore, 0);
        }
        
        
        private static void SetBestScore(int value)
        {
            int currentBest = PlayerPrefs.GetInt(KeyBestScore, 0);
            if (currentBest < value)
            {
                PlayerPrefs.SetInt(KeyBestScore, value);
            }
        }
    }
}