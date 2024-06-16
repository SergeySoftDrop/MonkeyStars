using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameCounter : MonoBehaviour
    { 
        public int EnemyCount { get; set; }
        public int BaseHP { get; set; }
        public int PlayerHP { get; set; }
        public GameCounter(int enemy, int baseHP, int playerHP)
        {
            EnemyCount = enemy;
            BaseHP = baseHP;
            PlayerHP = playerHP;
        }
    }
}