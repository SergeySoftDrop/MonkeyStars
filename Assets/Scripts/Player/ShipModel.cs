using Assets.Scripts.Conf.Scripts;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Player
{
    public class ShipModel : MonoBehaviour
    {
        public GameConfig gameConfig;

        public Transform GunFirstTransform;
        public Transform GunSecondTransform;
        public Transform GunThirdTransform;
        public Transform GunFourthTransform;
        public Transform GunSuperTransform;

        public GameObject bulletPref;
        public GameObject bulletSuperPref;

        public bool boost = false;

        public void ToggleBoost()
        {
            boost = !boost;
        }
    }
}
