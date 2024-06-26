using UnityEngine;

namespace Marsminerwa
{
    public class ScoreOnConnectionChange : MonoBehaviour
    {
        [SerializeField]
        private int scoreValue = 100;

        private bool done = false;
        
        private void OnEnable()
        {   
            GetComponent<Connection>().OnChange += OnChange;
        }
        
        private void OnDisable()
        {
            GetComponent<Connection>().OnChange -= OnChange;
        }
        
        private void OnChange(Connection source)
        {
            if (done) return;
            done = true;
            Score.score += scoreValue;
        }
    }
}