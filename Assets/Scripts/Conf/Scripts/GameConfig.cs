using UnityEngine;

namespace Assets.Scripts.Conf.Scripts
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public int PlayerHP = 10;
        public int BaseHP = 20;
        public int Enemy_1HP = 2;
        public int Enemy_2HP = 2;
        public int EnemyCount = 20;

        public float PlayerShootRateInSecond = 1.0f;
        public float PlayerSuperShootRateInSecond = 1.0f;
    }
}
