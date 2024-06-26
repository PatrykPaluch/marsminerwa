using System;
using TMPro;
using UnityEngine;

namespace Marsminerwa
{
    [DefaultExecutionOrder(ExecutionOrder.First)]
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        
        private AudioSource audioSource;
        
        [SerializeField]
        private AudioClip puzzleSolveSound;
        [SerializeField]
        private AudioClip enemyHitSound;
        [SerializeField]
        private AudioClip enemyDeathSound;
        [SerializeField]
        private AudioClip playerHitSound;
        [SerializeField]
        private AudioClip playerDeathSound;
        [SerializeField]
        private AudioClip moveBoxSound;
        
        public static AudioClip PuzzleSolveSound => instance.puzzleSolveSound;
        public static AudioClip EnemyHitSound => instance.enemyHitSound;
        public static AudioClip EnemyDeathSound => instance.enemyDeathSound;
        public static AudioClip PlayerHitSound => instance.playerHitSound;
        public static AudioClip PlayerDeathSound => instance.playerDeathSound;
        public static AudioClip MoveBoxSound => instance.moveBoxSound;

        public static void PlayPuzzleSolveSound() { instance.Play(PuzzleSolveSound); }
        public static void PlayEnemyHitSound() { instance.Play(EnemyHitSound); }
        public static void PlayEnemyDeathSound() { instance.Play(EnemyDeathSound); }
        public static void PlayPlayerHitSound() { instance.Play(PlayerHitSound); }
        public static void PlayPlayerDeathSound() { instance.Play(PlayerDeathSound); }
        public static void PlayMoveBoxSound() { instance.Play(MoveBoxSound); }

        private void Play(AudioClip audioClip)
        {
            audioSource.PlayOneShot(audioClip);
        }

        private void Awake()
        {
            Time.timeScale = 1;
            Score.score = 0;
            instance = this;
            audioSource = GetComponent<AudioSource>();
        }

        [SerializeField]
        private TMP_Text scoreText;

        private int prevScore = -1;
        private void Update()
        {
            if (prevScore != Score.score)
            {
                scoreText.text = $"{Score.score}";
                prevScore = Score.score;
            }
        }
    }
}