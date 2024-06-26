using System;
using UnityEngine;

namespace Marsminerwa
{
    public class Win : MonoBehaviour
    {

        [SerializeField]
        private GameObject winScreen;

        private void Start()
        {
            GameManager.PlayPuzzleSolveSound();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Score.score += 1000;
                winScreen.SetActive(true);
                // PlayerMovement.PlayerObject.SetActive(false);
                Time.timeScale = 0.25f;
            }
        }
    }
}