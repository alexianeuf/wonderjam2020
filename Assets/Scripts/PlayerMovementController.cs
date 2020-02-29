using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovementController : MonoBehaviour
{
    private List<Quaternion> lastRotations = new List<Quaternion>();

    [SerializeField] private float m_playerSpeed = 10f;
    [SerializeField] private float m_rotationStrength = 5f;

    private Rigidbody m_rigidbody;
    private Vector2 m_inputValue;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
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
        Debug.DrawLine(transform.position, transform.position + (transform.forward * 6000), Color.magenta);
        Debug.DrawRay(transform.position, transform.forward, Color.magenta);

        if (!Mathf.Approximately(m_inputValue.y, 0f))
        {
            Vector3 moveVector = transform.forward * m_playerSpeed * m_inputValue.y;

            m_rigidbody.MovePosition(m_rigidbody.position + moveVector);
        }
    }

    public void OnMove(InputValue value)
    {
        m_inputValue = value.Get<Vector2>();
    }
}