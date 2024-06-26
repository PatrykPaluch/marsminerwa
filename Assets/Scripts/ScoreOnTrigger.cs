using System;
using UnityEngine;

namespace Marsminerwa
{
    public class ScoreOnTrigger : MonoBehaviour
    {
        [SerializeField]
        private int score = 100;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Score.score += score;
                GameManager.PlayPuzzleSolveSound();
                Destroy(this);
            }
        }
    }
}