using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectiveIndicator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var objective = GameManager.instance.GetComponent<ObjectiveController>().m_currentObjective;
        if (objective == null)
            return;

        transform.LookAt(objective.transform);
    }
}
