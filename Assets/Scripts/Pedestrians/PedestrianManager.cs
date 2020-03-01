using System.Collections.Generic;
using UnityEngine;

public class PedestrianManager : MonoBehaviour
{
    [SerializeField] private GameObject m_wayPointHolder;
    [SerializeField] private GameObject m_pedestriansPrefab;
    [SerializeField] private int m_pedestrianMax = 10;

    public static int _activePedestriansCount = 0;
    public static GameObject PMinstance;
    public static List<Pedestrian> m_pedestrianInstances;

    void Awake()
    {
        PMinstance = gameObject;
        _activePedestriansCount = 0;
        m_pedestrianInstances = new List<Pedestrian>();
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
        GameObject newPedestrian = Instantiate(m_pedestriansPrefab, spawnPoint.transform.position, Quaternion.identity,
            transform);
        var pedestrianComponent = newPedestrian.GetComponent<Pedestrian>();
        pedestrianComponent.m_wayPointHolder = m_wayPointHolder;
        m_pedestrianInstances.Add(pedestrianComponent);

        // update count
        _activePedestriansCount++;
    }

    public static void DestroyPedestrian(GameObject pedestrian)
    {
        m_pedestrianInstances.Remove(pedestrian.GetComponent<Pedestrian>());
        Destroy(pedestrian);
    }
}