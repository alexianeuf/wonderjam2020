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

    public bool IsMoving { get; private set; }
    public bool CanMove = true;

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        m_realPlayerSpeed = m_playerSpeed * m_rigidbody.mass;

        CheckIfMoving();

        if (CanMove)
        {
            if (!IsGrounded())
                return;

            Move();
            Rotate();
        }
        else if (m_rigidbody.velocity.magnitude <= 0)
        {
            // TODO : Launch GAME OVER Screen - see how prevent a pop if player hit a barrel at the same frame
            playerInput.defaultActionMap = "UI";
        }
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
        if (CanMove)
            IsMoving = true;
    }

    private void CheckIfMoving()
    {
        if (Mathf.Approximately(m_inputValue.y, 0f) && Mathf.Approximately(m_inputValue.x, 0f))
            IsMoving = false;
    }

    [SerializeField] private bool isGrounded;

    public bool IsGrounded()
    {
        RaycastHit hit;
        int layerMask = 1 << LayerMask.NameToLayer("Player");
        layerMask = ~layerMask;

        var origin = transform.position + transform.TransformDirection(Vector3.up);
        var distance = 2;

        Debug.DrawRay(origin, transform.TransformDirection(Vector3.down) * distance, Color.yellow, 0, false);
        isGrounded = Physics.Raycast(origin, transform.TransformDirection(Vector3.down), out hit, distance, layerMask);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
        }

        return isGrounded;
    }
}