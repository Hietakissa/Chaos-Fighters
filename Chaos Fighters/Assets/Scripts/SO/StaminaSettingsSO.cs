using UnityEngine;

[CreateAssetMenu(menuName = "Game/Stamina Settings", fileName = "New Stamina Settings")]
public class StaminaSettingsSO : ScriptableObject
{
    [Header("Passive")]
    [SerializeField] float maxStamina;
    [SerializeField] float staminaRegenerationRate;
    [SerializeField] float staminaRegerationDelay;
    [SerializeField] float staminaDepletionDelay;

    [Header("Active")]
    [SerializeField] float passiveBlockStaminaConsumption;
    [SerializeField] float blockHitStaminaConsumption;


    public float MaxStamina => maxStamina;
    public float StaminaRegenerationRate => staminaRegenerationRate;
    public float StaminaRegenerationDelay => staminaRegerationDelay;
    public float StaminaDepletionDelay => staminaDepletionDelay;

    public float PassiveBlockStaminaConsumption => passiveBlockStaminaConsumption;
    public float BlockHitStaminaConsumption => blockHitStaminaConsumption;
}