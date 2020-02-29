using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Pedestrian : MonoBehaviour
{
    [SerializeField] public GameObject m_wayPointHolder;
    public GameObject m_currentWayPoint = null;

    [SerializeField] public List<GameObject> m_prefabList;

    protected NavMeshAgent NavMeshAgent;

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        
        int index = Random.Range(0, m_prefabList.Count);
        Instantiate(m_prefabList[index], transform.position, transform.rotation, transform);
    }

    // Start is called before the first frame update
    void Start()
    {
        SelectNextWayPoint();
        NavMeshAgent.SetDestination(m_currentWayPoint.transform.position);
    }

    void SelectNextWayPoint()
    {
        if (m_wayPointHolder.transform.childCount == 0)
            return;

        // get a random waypoint
        GameObject nextWayPoint = null;

        while (nextWayPoint == null || nextWayPoint.Equals(m_currentWayPoint))
        {
            int index = Random.Range(0, m_wayPointHolder.transform.childCount);
            nextWayPoint = m_wayPointHolder.transform.GetChild(index).gameObject;
        }

        m_currentWayPoint = nextWayPoint;
    }

    public void OnWayPointEnter(GameObject objective)
    {
        if (objective.Equals(m_currentWayPoint))
        {
            SelectNextWayPoint();
            NavMeshAgent.SetDestination(m_currentWayPoint.transform.position);
        }
    }
}
