using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Objective : MonoBehaviour
{
    [SerializeField]
    private GameObject m_indicator;
    public void TargetObjective(bool active)
    {
        m_indicator.SetActive(active);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.GetComponent<ObjectiveController>().OnObjectiveEnter(this.gameObject);
    }
}