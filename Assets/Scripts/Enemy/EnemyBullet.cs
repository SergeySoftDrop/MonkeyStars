using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    public override float Damage()
    {
        return gameConfig.EnemyBulletDamage;
    }

    protected override float GetSpeed()
    {
        return gameConfig.EnemyBulletSpeed;
    }
}
