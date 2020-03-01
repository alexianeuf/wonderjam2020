using System.Collections.Generic;
using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    [SerializeField] private GameObject m_objectiveHolder;
    [SerializeField] private GameObject m_player;

    public GameObject m_currentObjective;
    private List<GameObject> m_objectivesDone;
    private List<GameObject> m_selectableObjectives;
    private List<GameObject> m_allObjectives;

    private void Start()
    {
        m_objectivesDone = new List<GameObject>();
        m_selectableObjectives = new List<GameObject>();
        m_allObjectives = new List<GameObject>();

        for (int i = 0; i < m_objectiveHolder.transform.childCount; i++)
        {
            GameObject current = m_objectiveHolder.transform.GetChild(i).gameObject;
            current.GetComponent<Objective>().TargetObjective(false);
            
            m_selectableObjectives.Add(current);
            m_allObjectives.Add(current);
        }

        SelectNextObjective();
    }

    public void SelectNextObjective()
    {
        // if there is no more objectives
        if (m_selectableObjectives.Count == 0)
        {
            // reset the done objectives
            m_objectivesDone.Clear();
            
            // reset the selectable objectives
            m_selectableObjectives = m_allObjectives.GetRange(0, m_allObjectives.Count);
        }

        // choose the new objective
        int childIndex = Random.Range(0, m_selectableObjectives.Count);
        var nextOObjective = m_selectableObjectives[childIndex];
        
        
        // change the current objective
        if (m_currentObjective != null)
        {
            m_currentObjective.GetComponent<Objective>().TargetObjective(false);
        }

        m_currentObjective = nextOObjective;
        m_currentObjective.GetComponent<Objective>().TargetObjective(true);
        
        // update the lists
        m_objectivesDone.Add(m_currentObjective);
        m_selectableObjectives.Remove(m_currentObjective);

        GetComponent<TimerController>().ResetTimer();
    }

    public void OnObjectiveEnter(GameObject objective)
    {
        if (objective.Equals(m_currentObjective))
        {
            // TODO replace 100f with timer value
            m_player.GetComponent<PlayerScoreController>().IncreaseScore(100f);

            SelectNextObjective();
        }
    }
}