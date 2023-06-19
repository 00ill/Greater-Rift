using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private int _level = 1;
    public float MaxHealth { get; private set; } = 200f;
    private float _health = 100f;
    public float Health
    {
        get => _health;
        set { _health = Mathf.Clamp(value, 0, MaxHealth); }
    }
    public float MaxMana { get; private set; } = 200f;
    private float _mana = 100f;
    public float Mana
    {
        get => _mana;
        set { _mana = Mathf.Clamp(value, 0, MaxMana); }
    }
    public float MoveSpeed { get; set; } = 15f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            OnDamage(10f);
        }
    }
    private void OnDamage(float damage)
    {
        Health -= damage;
        Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerHpChange, this);
    }
}
