using UnityEngine;

namespace Assets.Scripts.Conf.Scripts
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public float PlayerHP = 10;
        public float PlayerSpeed = 10.0f;
        public float PlayerBoost = 20.0f;
        public float PlayerBoostRateInSecond = 0.1f;
        public float PlayerBoostMaxDurationInSecond = 5f;

        public float TransformRotationSpeed = 10f;
        public float PlayerSpwnRadius = 1f;

        public float BaseHP = 20;

        public float Enemy_1HP = 2;
        public float Enemy_1SpeedMax = 10f;
        public float Enemy_1SpeedMin = 5f;
        public float Enemy_1GenerateDistanceMin = 150f;
        public float Enemy_1GenerateDistanceMax = 150f;
        public int Enemy_1Count = 20;
        public int Enemy_1SpawnRate = 20;
        public int Enemy_1SpawnCountMax = 10;
        public int Enemy_1SpawnCountMin = 1;

        public int Enemy_2HP = 2;
        public float Enemy_2SpeedMax = 10f;
        public float Enemy_2SpeedMin = 5f;
        public float Enemy_2RotateSens = 5f;
        public float Enemy_2GenerateDistanceMin = 150f;
        public float Enemy_2GenerateDistanceMax = 150f;
        public int Enemy_2Count = 20;
        public int Enemy_2SpawnRate = 20;
        public int Enemy_2SpawnCountMax = 10;
        public int Enemy_2SpawnCountMin = 1;
        public int Enemy_2Shootcount = 5;
        public float Enemy_2ShootRateInSecond = 1f;
        public float Enemy_2ShootReloadInSecond = 3f;
        public float Enemy_2ShootDistance = 150f;

        public float PlayerShootRateInSecond = 5.0f;
        public float PlayerSuperShootRateInSecond = 1.0f;

        public float BulletSpeed = 125f;
        public float BulletSuperSpeed = 175f;
        public float BulletLifeTimeInSecond = 5f;
        public float BulletDamage = 1f;
        public float BulletSuperDamage = 2f;

        public float EnemyBulletSpeed = 125f;
        public float EnemyBulletDamage = 1f;

        public float MeteorRotateSpeed = 10f;
        public float MeteorCount = 20f;
        public float MeteorGenerateDistanceMax = 400f;
        public float MeteorGenerateDistanceMin = 800f;

        public float DamageEffectTimeInSeconds = 2f;

        public float PlayerCrashDamage = 1f;
    }
}
