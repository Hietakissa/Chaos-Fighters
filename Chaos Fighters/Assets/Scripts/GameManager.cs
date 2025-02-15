using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    [SerializeField] StaminaSettingsSO staminaSettings;
    [SerializeField] MovementSettingsSO movementSettings;


    public StaminaSettingsSO StaminaSettings => staminaSettings;
    public MovementSettingsSO MovementSettings => movementSettings;


    void Awake()
    {
        Instance = this;
    }
}