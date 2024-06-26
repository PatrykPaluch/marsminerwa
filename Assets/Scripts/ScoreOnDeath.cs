using UnityEngine;

namespace Marsminerwa
{
    public class ScoreOnDeath : MonoBehaviour
    {
        [SerializeField]
        private int score;
        
        private Health health;

        private void Awake()
        {
            health = GetComponent<Health>();
        }
        
        private void OnEnable()
        {
            health.OnDeath += OnDeath;
        }

        private void OnDisable()
        {
            health.OnDeath -= OnDeath;
        }

        private void OnDeath(Health _, int __)
        {
            Score.score += score;
        }
    }
}