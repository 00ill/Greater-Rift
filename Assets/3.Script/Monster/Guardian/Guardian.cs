using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardian : EnemyStatus
{


    protected override void OnEnable()
    {
        base.OnEnable();
        OnDeath -= OnGuardianDeath;
        OnDeath += OnGuardianDeath;
    }
    public override void TakeDamage(int damage, PlayerStatus playerStatus)
    {
        base.TakeDamage(damage, playerStatus);
        Managers.Event.PostNotification(Define.EVENT_TYPE.GuardianHpChange, this);
    }

    private void OnGuardianDeath()
    {
        Managers.Game.isGuardianSpawn = false;
    }
}
