using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Marsminerwa
{
    public class DamageNumbersManager : MonoBehaviour
    {
        private static DamageNumbersManager instance;
        public static Vector2 NumberDamageStartVelocityMin => instance.numberDamageStartVelocityMin;  
        public static Vector2 NumberDamageStartVelocityMax => instance.numberDamageStartVelocityMax;
        public static Vector2 NumberHealStartVelocityMin => instance.numberHealStartVelocityMin;  
        public static Vector2 NumberHealStartVelocityMax => instance.numberHealStartVelocityMax;
        public static Vector2 NumberDamageGravity => instance.numberDamageGravity;
        public static Vector2 NumberHealGravity => instance.numberHealGravity;
        public static float NumberLifetime => instance.numberLifetime;
        public static AnimationCurve NumberColorAlphaOverLifetime => instance.numberColorAlphaOverLifetime;
        public static Color HealColor => instance.healColor;
        public static Color DamageColor => instance.damageColor;
        
        [SerializeField]
        private Vector2 numberDamageStartVelocityMin = new Vector2(-1, 1);
        [SerializeField]
        private Vector2 numberDamageStartVelocityMax = new Vector2(1, 1);
        [SerializeField]
        private Vector2 numberHealStartVelocityMin = new Vector2(-0.1f, 0.2f);
        [SerializeField]
        private Vector2 numberHealStartVelocityMax = new Vector2(0.1f, 0.2f);
        [SerializeField]
        private Vector2 numberDamageGravity = new Vector2(0, -2);
        [SerializeField]
        private Vector2 numberHealGravity = new Vector2(0, 0.3f);
        [SerializeField]
        private float numberLifetime = 0.5f;
        [SerializeField]
        private AnimationCurve numberColorAlphaOverLifetime;
        [SerializeField]
        private Color healColor = new Color(0.2f, 0.8f, 0.2f);
        [SerializeField]
        private Color damageColor = new Color(0.8f, 0.2f, 0.2f);
        
        [SerializeField]
        private GlobalPool damageNumbersPool;

        public static void ShowDamageNumber(Vector2 position, int hpChange)
        {
            instance.InstanceShowDamageNumber(position, hpChange);
        }
        
        private void InstanceShowDamageNumber(Vector2 position, int hpChange)
        {
            GameObject damageNumber = damageNumbersPool.Get();
            damageNumber.transform.SetPosition2D(position);
            damageNumber.GetComponent<DamageNumber>().Init(hpChange);
        }
        
        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("Multiple instances of DamageNumbersManager found", this);
                gameObject.name += "(error)";
                gameObject.SetActive(false);
                return;
            }

            instance = this;
        }
        
        private void OnDestroy()
        {
            if (instance == this)
                instance = null;
        }
    }
}