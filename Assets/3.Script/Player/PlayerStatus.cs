using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private int _level = 1;
    private float _maxHealth;
    private float _health;
    public float Health
    {
        get 
        { 
            return _health;
        }
        set 
        {
            if(_maxHealth <= value)
            {
                _health = _maxHealth;
            }
            else
            {
                _health = value;
            }
        }
    }

    public float MoveSpeed { get; set; } = 15f;
}
