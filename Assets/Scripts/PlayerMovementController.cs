﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float m_playerSpeed = 10f;
    [SerializeField] private float m_rotationStrength = 5f;

    private float m_realPlayerSpeed;
    private Rigidbody m_rigidbody;
    private Vector2 m_inputValue;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        m_realPlayerSpeed = m_playerSpeed * m_rigidbody.mass;

        Move();
        Rotate();
    }

    private void Rotate()
    {
        if (!Mathf.Approximately(m_inputValue.x, 0f))
        {
            Vector3 forwardPos = transform.position + (transform.forward * 60);
            Vector3 rightForwardPos = forwardPos + transform.right * m_rotationStrength;
            Vector3 leftForwardPos = forwardPos - transform.right * m_rotationStrength;

            Vector3 look = Vector3.Lerp(leftForwardPos, rightForwardPos, m_inputValue.x + 1);

            transform.LookAt(look);
        }
    }

    private void Move()
    {
        if (!Mathf.Approximately(m_inputValue.y, 0f))
        {
            Vector3 moveVector = transform.forward * m_realPlayerSpeed * m_inputValue.y;
            m_rigidbody.AddForce(moveVector);

//            m_rigidbody.velocity = new Vector3 (moveVector.x, m_rigidbody.velocity.y, moveVector.z);
//            m_rigidbody.MovePosition(m_rigidbody.position + moveVector);
        }

        ClampAngularVelocity();
    }

    private void ClampAngularVelocity()
    {
        float min = -0.4f;
        float max = 0.4f;
        Vector3 angularVelocity = m_rigidbody.angularVelocity;

        angularVelocity.x = Mathf.Clamp(angularVelocity.x, min, max);
        angularVelocity.z = Mathf.Clamp(angularVelocity.z, min, max);

        m_rigidbody.angularVelocity = angularVelocity;
    }

    public void OnMove(InputValue value)
    {
        m_inputValue = value.Get<Vector2>();
    }
}