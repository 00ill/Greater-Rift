using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    protected PlayerStatus _playerStatus;
    protected Rigidbody _projectileRigidbody;
    protected float _projectileSpeed;
    protected float _damage;
    protected float _damageCoefficient;
    protected float _criticalChance;
    private void Awake()
    {
        _playerStatus = FindObjectOfType<PlayerStatus>();
        InitializeProjectile();
    }

    protected virtual void OnEnable()
    {
    }

    protected virtual void InitializeProjectile()
    {
        TryGetComponent(out _projectileRigidbody);
    }

    /// <summary>
    /// 총알이 바라보는 방향으로 발사
    /// </summary>
    public virtual void ShootForward()
    {
        _projectileRigidbody.velocity = this.transform.forward * _projectileSpeed;
    }

    protected void DestroyProjectile(float time)
    {
        StartCoroutine(Destroy(time));
    }

    private IEnumerator Destroy(float time)
    {
        WaitForSeconds playTime = new WaitForSeconds(time);
        yield return playTime;
        Managers.Resource.Destroy(gameObject);

    }
}