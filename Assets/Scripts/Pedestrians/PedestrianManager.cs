using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianManager : MonoBehaviour
{
    [SerializeField] private GameObject m_wayPointHolder;
    [SerializeField] private GameObject m_pedestriansPrefab;
    [SerializeField] private int m_pedestrianMax = 10;

    public static int _activePedestriansCount = 0;
    public static GameObject PMinstance;
    void Start()
    {
        PMinstance = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (_activePedestriansCount < m_pedestrianMax)
        {
            InstantiatePedestrian();
        }        
    }

    void InstantiatePedestrian()
    {
        // get random spawn point
        int index = Random.Range(0, m_wayPointHolder.transform.childCount);
        GameObject spawnPoint = m_wayPointHolder.transform.GetChild(index).gameObject;

        // instantiate pedestrian
        GameObject newPedestrian = Instantiate(m_pedestriansPrefab, spawnPoint.transform.position, Quaternion.identity, transform);
        newPedestrian.GetComponent<Pedestrian>().m_wayPointHolder = m_wayPointHolder;

        // update count
        _activePedestriansCount++;
    }
}
