using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD HUDInstance;

    [SerializeField] private Slider m_healthSlider;
    [SerializeField] private Slider m_fuelSlider;
    [SerializeField] private Text m_timerText;

    private PlayerHealthController healthComponent;
    private PlayerFuelController fuelComponent;
    private TimerController timerComponent;
    private PlayerSco timerComponent;
    
    
    private void Awake()
    {
        HUDInstance = this;
    }

    public void InitHUD()
    {
        healthComponent = Player.instance.GetComponent<PlayerHealthController>();
        fuelComponent = Player.instance.GetComponent<PlayerFuelController>();
        timerComponent = GameManager.instance.GetComponent<TimerController>();

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
        m_timerText.text = timerComponent.currentTime.ToString();
    }
}