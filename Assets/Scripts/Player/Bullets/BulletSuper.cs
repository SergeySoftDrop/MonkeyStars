using Assets.Scripts.Conf.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSuper : Bullet
{
    protected override float GetSpeed()
    {
        return gameConfig.BulletSuperSpeed;
    }

    public override float Damage()
    {
        return gameConfig.BulletSuperDamage;
    }
}
