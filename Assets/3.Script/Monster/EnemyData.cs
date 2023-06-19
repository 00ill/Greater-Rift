using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObject/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    public string Name;
    public float MaxHealth;
    public float Health;
    public float Damage;
    public float DetectionRange;
    public float AttackRange;
    public float AttackCooldown;
    public float MoveSpeed;

}
