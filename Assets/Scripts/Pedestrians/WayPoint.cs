using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Pedestrian>().OnWayPointEnter(this.gameObject);
    }
}
