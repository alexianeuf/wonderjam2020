using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD HUDInstance;

    [SerializeField] private Slider m_healthSlider;
    [SerializeField] private Slider m_fuelSlider;

    private PlayerHealthController healthComponent;
    private PlayerFuelController fuelComponent;

    private void Start()
    {
        HUDInstance = this;
    }

    public void InitHUD()
    {
        healthComponent = Player.instance.GetComponent<PlayerHealthController>();
        fuelComponent = Player.instance.GetComponent<PlayerFuelController>();

        m_healthSlider.maxValue = healthComponent._maxHealth;
        m_healthSlider.value = healthComponent.currentHealth;

        m_fuelSlider.maxValue = fuelComponent._maxFuelQuantity;
        m_fuelSlider.value = fuelComponent.currentFuelQuantity;
    }

    // Update is called once per frame
    void Update()
    {
        m_healthSlider.value = healthComponent.currentHealth;
        m_fuelSlider.value = fuelComponent.currentFuelQuantity;
    }
}