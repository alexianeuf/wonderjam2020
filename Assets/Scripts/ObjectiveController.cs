using System.Collections.Generic;
using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    [SerializeField] private GameObject m_objectiveHolder;
    [SerializeField] private GameObject m_player;

    public GameObject m_currentObjective;
    private List<GameObject> m_objectivesDone;

    private void Start()
    {
        m_objectivesDone = new List<GameObject>();

        for (int i = 0; i < m_objectiveHolder.transform.childCount; i++)
        {
            GameObject current = m_objectiveHolder.transform.GetChild(i).gameObject;
        }

        SelectNextObjective();
    }

    public void SelectNextObjective()
    {
        if (m_objectiveHolder.transform.childCount == 0)
            return;

        // if all the objectives has been done
        if (m_objectiveHolder.transform.childCount == m_objectivesDone.Count)
            m_objectivesDone.Clear();

        
        // compute the farthest object 
        GameObject farthestObjective = m_objectiveHolder.transform.GetChild(0).gameObject;
        float farthestDistance = 0f;

        for (int i = 0; i < m_objectiveHolder.transform.childCount; i++)
        {
            GameObject current = m_objectiveHolder.transform.GetChild(i).gameObject;
            // reset
            current.GetComponent<Objective>().TargetObjective(false);
            
            // if the object has been already done continue to the next one
            if (m_objectivesDone.Contains(current))
                continue;

            float currentDistance = Vector3.Distance(m_player.transform.position, current.transform.position);

            if (currentDistance > farthestDistance)
            {
                farthestDistance = currentDistance;
                farthestObjective = current;
            }
        }

        m_currentObjective = farthestObjective;
        m_currentObjective.GetComponent<Objective>().TargetObjective(true);
        m_objectivesDone.Add(m_currentObjective);
    }

    public void OnObjectiveEnter(GameObject objective)
    {
        if (objective.Equals(m_currentObjective))
        {
            SelectNextObjective();
        }
    }
}