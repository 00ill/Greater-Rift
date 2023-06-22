using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObject/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    public string Name;
    public int Life;
    public int Damage;
    public int Armor;
    public int DetectionRange;
    public int AttackRange;
    public int AttackCooldown;
    public int MoveSpeed;
}
