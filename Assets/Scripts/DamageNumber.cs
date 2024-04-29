using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Marsminerwa
{
    public class DamageNumber : MonoBehaviour
    {
        private Vector2 velocity;
        private TMP_Text text;
        private float lifetime;
        private Vector2 gravity;

        private void Awake()
        {
            text = GetComponent<TMP_Text>();
        }

        public void Init(int hpChange)
        {
            Vector2 min;
            Vector2 max; 
            if (hpChange > 0){
                min = DamageNumbersManager.NumberHealStartVelocityMin;
                max = DamageNumbersManager.NumberHealStartVelocityMax;
                gravity = DamageNumbersManager.NumberHealGravity;
                
                text.text = "+" + hpChange;
                text.color = DamageNumbersManager.HealColor;
            }
            else
            {
                min = DamageNumbersManager.NumberDamageStartVelocityMin;
                max = DamageNumbersManager.NumberDamageStartVelocityMax;
                gravity = DamageNumbersManager.NumberDamageGravity;
                
                text.text = "" + (-hpChange);
                text.color = DamageNumbersManager.DamageColor;
            }

            lifetime = 0;
            velocity = new Vector2(
                Random.Range(min.x, max.x),
                Random.Range(min.y, max.y));
        }
        
        private void Update()
        {
            transform.position += (Vector3)velocity * Time.deltaTime;
            velocity += gravity * Time.deltaTime;
            lifetime += Time.deltaTime;
            float colorProgress = lifetime / DamageNumbersManager.NumberLifetime;
            float colorAlpha = DamageNumbersManager.NumberColorAlphaOverLifetime.Evaluate(colorProgress);
            Color newColor = text.color;
            newColor.a = colorAlpha;
            text.color = newColor;
            
            if (lifetime > DamageNumbersManager.NumberLifetime)
                gameObject.SafeDestroy();
        }
    }
}