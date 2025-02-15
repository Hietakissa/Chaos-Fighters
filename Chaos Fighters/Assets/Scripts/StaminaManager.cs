using UnityEngine;

public class StaminaManager
{
    bool regenerating = true;
    float currentRegenDelay = 0f;
    float stamina;

    StaminaSettingsSO settings;


    public float Stamina => stamina;

    public void Init()
    {
        settings = GameManager.Instance.StaminaSettings;
        stamina = settings.MaxStamina;
    }

    public void Update()
    {
        if (regenerating)
        {
            // Make up for extra time waited for stamina consumption due to low framerates
            if (currentRegenDelay < 0f)
            {
                stamina += Mathf.Abs(currentRegenDelay) * settings.StaminaRegenerationRate;
                currentRegenDelay = 0f;
            }

            stamina += settings.StaminaRegenerationRate * Time.deltaTime;
        }
        else
        {
            currentRegenDelay -= Time.deltaTime;
            if (currentRegenDelay <= 0f) regenerating = true;
        }

        stamina = Mathf.Clamp(stamina, 0f, settings.MaxStamina);
    }

    public void TakeHit()
    {
        regenerating = false;

        stamina -= settings.BlockHitStaminaConsumption;

        if (stamina <= 0f) currentRegenDelay = settings.StaminaDepletionDelay;
        else currentRegenDelay = settings.StaminaRegenerationDelay;
    }
}