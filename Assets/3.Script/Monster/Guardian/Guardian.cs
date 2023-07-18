using Enemy;
using UnityEngine;

public class Guardian : EnemyStatus
{


    protected override void OnEnable()
    {
        base.OnEnable();
        OnDeath -= OnGuardianDeath;
        OnDeath += OnGuardianDeath;
        OnDeath -= CheckItemSpawn;
        OnDeath -= GuardianCheckItemSpawn;
        OnDeath += GuardianCheckItemSpawn;
        Managers.Sound.SelectVolume(Define.Sound.Bgm, 1f);
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

    private void GuardianCheckItemSpawn()
    {
        _enemyCollider.enabled = false;
        int itemDropProb = 40;
        if (Util.Probability(itemDropProb))
        {
            Managers.Item.GenerateItem(Managers.Game.PlayerLevel, transform.position);
        }
        if (Util.Probability(itemDropProb))
        {
            Managers.Item.GenerateItem(Managers.Game.PlayerLevel, transform.position + Vector3.forward);
        }
        if (Util.Probability(itemDropProb))
        {
            Managers.Item.GenerateItem(Managers.Game.PlayerLevel, transform.position + Vector3.back);
        }
        if (Util.Probability(itemDropProb))
        {
            Managers.Item.GenerateItem(Managers.Game.PlayerLevel, transform.position + Vector3.left);
        }
        if (Util.Probability(itemDropProb))
        {
            Managers.Item.GenerateItem(Managers.Game.PlayerLevel, transform.position + Vector3.right);
        }
    }
}
