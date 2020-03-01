using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var pedestrian = other.GetComponent<Pedestrian>();
        if(pedestrian != null)
            pedestrian.OnWayPointEnter(this.gameObject);
    }
}
