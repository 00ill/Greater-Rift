using Enemy;
using System.Collections;
using UnityEngine;

public class DarkFlare : Projectile
{
    private int _maxTargetNum = 10;
    int numColliders;
    private GameObject[] _lines;
    private Collider[] _hitColliders;
    private LineRenderer[] _lineRenderers;
    private EnemyStatus[] _enemyStatuses;
    private WaitForSeconds _playTime = new WaitForSeconds(5);
    private WaitForSeconds _damageTick = new WaitForSeconds(0.3f);
    protected override void OnEnable()
    {
        base.OnEnable();
        _lines = new GameObject[_maxTargetNum];
        _hitColliders = new Collider[_maxTargetNum];
        _lineRenderers = new LineRenderer[_maxTargetNum];
        _enemyStatuses = new EnemyStatus[_maxTargetNum];

        for (int i = 0; i < _maxTargetNum; i++)
        {
            _lines[i] = transform.GetChild(0).GetChild(i).gameObject;
            _lineRenderers[i] = _lines[i].GetComponent<LineRenderer>();
            _lineRenderers[i].positionCount = 2;
            _lineRenderers[i].enabled = false;
        }
        StartCoroutine(Destroy());
        StartCoroutine(AttackMonster());
    }
    private void Update()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Monster");
        numColliders = Physics.OverlapSphereNonAlloc(transform.position, 10f, _hitColliders, layerMask);
        for (int i = 0; i < numColliders; i++)
        {
            if (_hitColliders[i].TryGetComponent(out EnemyStatus enemyStatus))
            {
                _enemyStatuses[i] = enemyStatus;
                if (!enemyStatus.IsDead)
                {
                    _lineRenderers[i].enabled = true;
                    _lineRenderers[i].SetPosition(0, transform.position);
                    _lineRenderers[i].SetPosition(1, _hitColliders[i].transform.position + Vector3.up * 0.5f);
                    _lineRenderers[i].transform.GetChild(0).position = _hitColliders[i].transform.position + Vector3.up * 0.5f;
                    _lineRenderers[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    _lineRenderers[i].transform.GetChild(0).gameObject.SetActive(false);
                    _lineRenderers[i].enabled = false;
                }
            }
            else
            {
                _lineRenderers[i].transform.GetChild(0).gameObject.SetActive(false);
                _enemyStatuses[i] = null;
            }
        }
        for (int i = numColliders; i < _maxTargetNum; i++)
        {
            _lineRenderers[i].enabled = false;
            _lineRenderers[i].transform.GetChild(0).gameObject.SetActive(false);
            _enemyStatuses[i] = null;
        }
    }
    protected override void InitializeProjectile()
    {
        _projectileSpeed = 3f;
        base.InitializeProjectile();
    }

    private IEnumerator Destroy()
    {
        yield return _playTime;
        Managers.Resource.Destroy(gameObject);
    }
    private IEnumerator AttackMonster()
    {
        while (true)
        {
            for (int i = 0; i < _enemyStatuses.Length; i++)
            {
                if (_enemyStatuses[i] != null)
                {
                    _enemyStatuses[i].TakeDamage((int)(_playerStatus.GetStats(Statistic.Damage).IntetgerValue * Managers.Skill.GetSkillData(SkillName.DarkFlare).DamageCoefficient), _playerStatus);
                }
            }
            yield return _damageTick;
        }
    }
    private void OnDestroy()
    {
        for (int i = 0; i < _lineRenderers.Length; i++)
        {
            _lineRenderers[(int)i].transform.GetChild(0).gameObject.SetActive(false);
        }
        StopAllCoroutines();
    }
}