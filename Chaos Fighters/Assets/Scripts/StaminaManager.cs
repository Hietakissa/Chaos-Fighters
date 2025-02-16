using UnityEngine;

public class StaminaManager
{
    bool regenerating = true;
    float currentRegenDelay = 0f;
    float stamina;

    StaminaSettingsSO settings;

    PlayerController player;

    public float Stamina => stamina;

    public void Init(PlayerController player)
    {
        this.player = player;
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
                ReplenishStamina(Mathf.Abs(currentRegenDelay) * settings.StaminaRegenerationRate);
                currentRegenDelay = 0f;
            }

            ReplenishStamina(settings.StaminaRegenerationRate * Time.deltaTime);
        }
        else
        {
            currentRegenDelay -= Time.deltaTime;
            if (currentRegenDelay <= 0f) regenerating = true;
        }
    }

    public void DrainStamina(float amount)
    {
        regenerating = false;

        stamina -= amount;
        if (stamina < 0f) stamina = 0f;

        if (stamina <= 0f) currentRegenDelay = settings.StaminaDepletionDelay;
        else currentRegenDelay = settings.StaminaRegenerationDelay;


        player.DebugStaminaBar.fillAmount = stamina / settings.MaxStamina;
    }

    public void ReplenishStamina(float amount)
    {
        stamina += amount;
        if (stamina > settings.MaxStamina) stamina = settings.MaxStamina;


        player.DebugStaminaBar.fillAmount = stamina / settings.MaxStamina;
    }

    public void TakeHit()
    {
        DrainStamina(settings.BlockHitStaminaConsumption);
    }
}