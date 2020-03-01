using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float m_playerSpeed = 10f;
    [SerializeField] private float m_playerBackwardSpeed = 10f;
    [SerializeField] private float m_rotationStrength = 5f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private int m_turtleDelay = 4;

    [SerializeField] private float m_velocityDamp = 1f;

    public bool IsMoving { get; private set; }
    public bool CanMove = true;

    [Header("Fenzy mode")] [SerializeField]
    private float m_fenzyAgroDistance = 15f;

    [SerializeField] [Range(0f, 1f)] [Tooltip("1 = more on the pedestrian, 0 = more on teh player")]
    private float m_fenzyAgro = 0.5f;

    [SerializeField] private int m_fenzyModeFrameNumber = 30;
    [SerializeField] private AudioSource m_fenzyAudioSource;

    private int m_fenzyModeCounter = 0;
    private Rigidbody m_rigidbody;
    private Vector2 m_inputValue;
    private Vector3 m_moveVector = Vector3.zero;
    private Vector3 m_nextLookPosition;

    private Vector3 m_velocity = Vector3.zero;
    private IEnumerator m_turtleCoroutine;
    private bool m_turtleCoroutineStarted;
    private bool m_ismFenzyAudioClipNotNull;

    private void Awake()
    {
        m_ismFenzyAudioClipNotNull = m_fenzyAudioSource != null;
        m_rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        m_turtleCoroutine = TurtleCouroutine();
    }

    public void FixedUpdate()
    {
        CheckIfMoving();

        
            if (!IsGrounded())
            {
                StartTurtleRoutine();
                return;
            }
            else
            {
                StopTurtleRoutine();
            }

            Move();
            Rotate();

        if (Mathf.Abs(m_moveVector.magnitude) < 0.1f && !CanMove)
        {
            GameManager.instance.GetComponent<MenuController>().LaunchGameOverMenu(DeathCause.LowFuel);
        }
    }

    #region Movement

    private void Rotate()
    {
        if (!Mathf.Approximately(m_inputValue.x, 0f) && CanMove)
        {
            Vector3 forwardPos = transform.position + (transform.forward * 60);
            Vector3 rightForwardPos = forwardPos + transform.right * m_rotationStrength;
            Vector3 leftForwardPos = forwardPos - transform.right * m_rotationStrength;

            m_nextLookPosition = Vector3.Lerp(leftForwardPos, rightForwardPos, m_inputValue.x + 1);

            if (FrenzyManager.isFrenzy)
            {
                ApplyFrenzyMode();
            }

            transform.LookAt(m_nextLookPosition);
        }
    }

    private void Move()
    {
        if (!Mathf.Approximately(m_inputValue.y, 0f) && CanMove)
        {
            var speed = m_playerSpeed;
            if (m_inputValue.y < 0)
                speed = m_playerBackwardSpeed;
               m_moveVector = transform.forward * speed * m_inputValue.y * Time.fixedDeltaTime;
        }
        else
        {
            // apply the remaining velocity
            m_moveVector = Vector3.SmoothDamp(m_moveVector, Vector3.zero, ref m_velocity, m_velocityDamp);
        }

        m_rigidbody.MovePosition(m_rigidbody.position + m_moveVector);

        ClampAngularVelocity();
    }

    private void ApplyFrenzyMode()
    {
        if (PedestrianManager.m_pedestrianInstances.Count == 0)
        {
            return;
        }

        m_fenzyModeCounter++;
        if (m_fenzyModeCounter % m_fenzyModeFrameNumber != 0)
        {
            return;
        }

        m_fenzyModeCounter = 0;

        Pedestrian nearestPed = PedestrianManager.m_pedestrianInstances[0];
        float nearestDistance = Mathf.Infinity;

        foreach (var pedestrian in PedestrianManager.m_pedestrianInstances)
        {
            if (!pedestrian.isAlive)
                continue;

            float distance = Vector3.Distance(m_rigidbody.position, pedestrian.transform.position);

            if (distance < nearestDistance)
            {
                var target = pedestrian.transform.position - transform.position;
                float angle = Vector3.SignedAngle(transform.forward, target, Vector3.up);

                if (angle < 30f && angle > -30f)
                {
                    nearestDistance = distance;
                    nearestPed = pedestrian;
                }
            }
        }

        if (nearestDistance > m_fenzyAgroDistance)
        {
            return;
        }

        if (m_ismFenzyAudioClipNotNull)
            m_fenzyAudioSource.Play();
        m_nextLookPosition = Vector3.Lerp(m_nextLookPosition, nearestPed.transform.position, m_fenzyAgro);
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
        if (CanMove)
            IsMoving = true;
    }

    private void CheckIfMoving()
    {
        if (Mathf.Approximately(m_inputValue.y, 0f) && Mathf.Approximately(m_inputValue.x, 0f))
            IsMoving = false;
    }

    public bool IsGrounded()
    {
        RaycastHit hit;
        int layerMask = 1 << LayerMask.NameToLayer("Player");
        layerMask = ~layerMask;

        var origin = transform.position + transform.TransformDirection(Vector3.up);
        var distance = 2;

        Debug.DrawRay(origin, transform.TransformDirection(Vector3.down) * distance, Color.yellow, 0, false);
        isGrounded = Physics.Raycast(origin, transform.TransformDirection(Vector3.down), out hit, distance, layerMask);

        return isGrounded;
    }

    #endregion

    #region turtle

    private IEnumerator TurtleCouroutine()
    {
        yield return new WaitForSeconds(m_turtleDelay);

        QuitTurtleMode();
        m_turtleCoroutineStarted = false;
    }

    private void StopTurtleRoutine()
    {
        if (!m_turtleCoroutineStarted)
            return;

        StopCoroutine(m_turtleCoroutine);
        m_turtleCoroutineStarted = false;
    }

    private void StartTurtleRoutine()
    {
        if (!m_turtleCoroutineStarted)
        {
            m_turtleCoroutineStarted = true;
            m_turtleCoroutine = TurtleCouroutine();
            StartCoroutine(m_turtleCoroutine);
        }
    }

    private void QuitTurtleMode()
    {
        Debug.Log("Deturtleing player");
        m_rigidbody.position = m_rigidbody.position + Vector3.up * 2;
        m_rigidbody.rotation = Quaternion.identity;
    }

    #endregion
}