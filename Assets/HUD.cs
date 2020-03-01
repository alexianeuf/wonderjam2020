using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Slider m_healthSlider;
    [SerializeField] private Slider m_fuelSlider;
    
    private PlayerHealthController healthComponent;
    private PlayerFuelController fuelComponent;
    

    void Start()
    {
        healthComponent = Player.instance.GetComponent<PlayerHealthController>();
//        healthComponent = Player.instance.GetComponent<PlayerFuelController>();

        m_healthSlider.maxValue = healthComponent._maxHealth;
        m_healthSlider.value = healthComponent._maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        m_healthSlider.value = healthComponent._maxHealth;
    }
}