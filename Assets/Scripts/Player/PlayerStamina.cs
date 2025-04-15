using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public int maxStamina = 100;
    public float stamina;

    public float regenRate = 10f;
    public float regenDelay = 1.5f;
    private float regenTimer;

    public StaminaBar staminaBar;

    PlayerRespawn pr;
    bool staminaOut;

    void Start()
    {
        staminaOut = false;
        stamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
        pr = GameObject.Find("Player").GetComponent<PlayerRespawn>();
    }

    private void Update()
    {
        if (stamina <= 0)
        {
            staminaOut = true;
            stamina = 0;
        }

        regenTimer += Time.deltaTime;

        if (regenTimer >= regenDelay && !staminaOut)
        {
            RegenerateStamina();
        }
    }

    void RegenerateStamina()
    {
        if (stamina < maxStamina)
        {
            stamina += regenRate * Time.deltaTime;

            stamina = Mathf.Clamp(stamina, 0, maxStamina);

            staminaBar.SetStamina((int)stamina);

            if (stamina >= maxStamina)
                staminaOut = false;
        }
    }

    public void TakeStamina(int value)
    {
        if (staminaOut)
            return;

        stamina -= value;
        stamina = Mathf.Clamp(stamina, 0, maxStamina);
        staminaBar.SetStamina((int)stamina);

        regenTimer = 0f;
    }
}