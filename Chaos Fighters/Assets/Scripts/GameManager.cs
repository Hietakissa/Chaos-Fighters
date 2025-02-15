using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    [SerializeField] StaminaSettingsSO staminaSettings;
    [SerializeField] MovementSettingsSO movementSettings;
    [SerializeField] CombatSettingsSO combatSettings;


    public StaminaSettingsSO StaminaSettings => staminaSettings;
    public MovementSettingsSO MovementSettings => movementSettings;
    public CombatSettingsSO CombatSettings => combatSettings;


    void Awake()
    {
        Instance = this;
    }
}