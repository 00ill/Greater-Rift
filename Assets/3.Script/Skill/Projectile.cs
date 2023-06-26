using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    protected PlayerStatus _playerStatus;
    protected Rigidbody _projectileRigidbody;
    //protected Entity _entity;
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
        //_damage = _playerStatus.Damage;
    }

    /// <summary>
    /// �ڽ� Ŭ�������� ����ü Ǯ �̸� �����������
    /// </summary>
    protected virtual void InitializeProjectile()
    {
        TryGetComponent(out _projectileRigidbody);
    }

    /// <summary>
    /// �Ѿ��� �ٶ󺸴� �������� �߻�
    /// </summary>
    public virtual void ShootForward()
    {
        _projectileRigidbody.velocity = this.transform.forward * _projectileSpeed;
    }
}