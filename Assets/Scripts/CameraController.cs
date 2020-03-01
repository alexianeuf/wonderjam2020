using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Camera m_camera;

    [SerializeField]
    private Vector3 m_offsetPosition = Vector3.zero;

    [SerializeField]
    private float damping = 1;
    
    private void Start()
    {
        if (m_camera == null)
        {
            m_camera = Camera.main;
        }
    }

    void LateUpdate() {
        float currentAngle = m_camera.transform.eulerAngles.y;
        float desiredAngle = transform.eulerAngles.y;
        float angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.deltaTime * damping);
         
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        m_camera.transform.position = transform.position - (rotation * -m_offsetPosition);
         
        m_camera.transform.LookAt(transform);
    }
    
}
