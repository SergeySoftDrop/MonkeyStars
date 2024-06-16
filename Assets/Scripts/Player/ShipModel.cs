using Assets.Scripts.Conf.Scripts;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Player
{
    public class ShipModel : MonoBehaviour
    {
        public GameConfig gameConfig;

        public GameObject ship;

        public Transform GunFirstTransform;
        public Transform GunSecondTransform;
        public Transform GunThirdTransform;
        public Transform GunFourthTransform;
        public Transform GunSuperTransform;

        public GameObject bulletPref;
        public GameObject bulletSuperPref;

        public float HP = 0;

        public bool boost = false;

        private void Awake()
        {
            this.HP = gameConfig.PlayerHP;
        }
        
        public void ToggleBoost()
        {
            boost = !boost;
        }
    }
}
