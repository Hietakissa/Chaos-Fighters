using UnityEngine;

[CreateAssetMenu(menuName = "Game/Combat Settings", fileName = "New Combat Settings")]
public class CombatSettingsSO : ScriptableObject
{
    [SerializeField] int maxHealth;
    [SerializeField] int basicDamage;
    [SerializeField] int specialDamage;

    public int MaxHealth;
    public int BasicDamage;
    public int SpecialDamage;
}