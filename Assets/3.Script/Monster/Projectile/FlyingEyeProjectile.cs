using Enemy;
using UnityEngine;

public class FlyingEyeProjectile : Projectile
{
    private EnemyStatus _enemyStatus;

    protected override void InitializeProjectile()
    {
        _projectileSpeed = 5f;
        GameObject.Find("FlyingEye").TryGetComponent(out _enemyStatus);
        base.InitializeProjectile();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerStatus.TakeDamage(_enemyStatus.GetStats(Enemy.Statistic.Damage).IntegerValue);
            Managers.Resource.Destroy(gameObject);
        }
    }
}
