using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Pedestrian : MonoBehaviour
{
    private GameObject m_currentWayPoint = null;
    [SerializeField] public GameObject m_wayPointHolder;
    [SerializeField] public List<GameObject> m_prefabList;

    protected NavMeshAgent NavMeshAgent;
    private GameObject m_meshChild;

    public bool isAlive = true;

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        
        int index = UnityEngine.Random.Range(0, m_prefabList.Count);
        m_meshChild = Instantiate(m_prefabList[index], transform.position, transform.rotation, transform);
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
            int index = UnityEngine.Random.Range(0, m_wayPointHolder.transform.childCount);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // update score
            other.transform.parent.gameObject.GetComponent<PlayerScoreController>().IncreaseScore(-1f);

            // update furry mode
            PlayerFrenzyController frenzyController = other.transform.parent.gameObject.GetComponent<PlayerFrenzyController>();
            frenzyController.IncreaseFrenzyLevel();

            OnDeath();
        }
    }

    private void OnDeath()
    {
        PerlinCameraShake.instance.Trauma = 0.25f;
        isAlive = false;
        StartCoroutine("DeathRoutine");
    }

    IEnumerator DeathRoutine()
    {
        // hide pedestrian body
        m_meshChild.SetActive(false);

        // blood splashes
        GetComponentInChildren<ParticleSystem>().Play();

        // ouch + car collision sounds
        foreach (AudioSource audio in GetComponents<AudioSource>())
        {
            audio.Play();
        }
        PedestrianManager._activePedestriansCount--;

        // let it all finish
        yield return new WaitForSeconds(1f);

        PedestrianManager.DestroyPedestrian(gameObject);
    }
}
