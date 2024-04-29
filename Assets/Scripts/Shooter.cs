using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Marsminerwa
{
    public class Shooter : MonoBehaviour
    {
        
        [Tooltip("Number of bullets to shoot per second")]
        public float ShootFrequency = 2f;
        
        [Tooltip("Number of bullets to shoot before reloading")]
        public int BulletsInMagazine = 4;
        public float ReloadTime = 1f;
        
        [SerializeField]
        private GlobalPool bulletPool;
        
        private int bulletsLeft;
        private bool canShoot;

        /// <summary> Retruns bullet GameObject if bullet was shot, null otherwise</summary>
        [CanBeNull]
        public GameObject Shoot(Vector3 position, Vector3 direction)
        {
            if (!canShoot) return null;
            
            canShoot = false;
            
            bulletsLeft -= 1;
            if (bulletsLeft <= 0)
            {
                this.CallAfter(ReloadTime, () => {
                    canShoot = true;
                    bulletsLeft = BulletsInMagazine;
                });
            }
            else
            {
                this.CallAfter(1f / ShootFrequency, () => canShoot = true);    
            }

            return bulletPool.Get((bullet) => {
                Transform bulletTransform = bullet.transform;
                bulletTransform.position = position;
                bulletTransform.up = direction;
            });
        }
        
        private void Start()
        {
            canShoot = true;
            bulletsLeft = BulletsInMagazine;
        }

        private void OnValidate()
        {
            BulletsInMagazine = Math.Max(1, BulletsInMagazine);
            ShootFrequency = Math.Max(0.0001f, ShootFrequency);
            ReloadTime = Math.Max(0.0001f, ReloadTime);
        }
    }
}