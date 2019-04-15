using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class MyCharacterController2D : MonoBehaviour
{
    #region F/P    
    const float CEILINGRADIUS = .2f;
    const float GROUNDEDRADIUS = .02f;
    [SerializeField, Range(0, 1000)]
    public float jumpForce = 100f;
    [Range(0, .3f)]
    [SerializeField]
    float movementSmoothing = .05f;
    [SerializeField, Range(.1f, 50)]
    public float moveSpeed = 7;
    [SerializeField]
    bool canAirControl = false;
    [SerializeField]
    bool isFacingRight = true;
    [SerializeField]
    bool isGrounded;
    public bool IsGrounded { get { return isGrounded; } }
    [SerializeField]
    LayerMask whatIsGround;
    [SerializeField]
    Rigidbody2D playerRigidbody2D;
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    Transform ceilingCheck;
    Vector3 velocity = Vector3.zero;
    [Header("Events")]
    [Space]
    public UnityEvent OnLandEvent;
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    #endregion

    #region Meths
    void FlipCharactere()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }

    public void Move(float _move, bool _isJump)
    {
        if (GameManager.m_AreInputsEnabled)
        {
            if (isGrounded || canAirControl)
            {
                if (_move > 0 && !isFacingRight)
                {
                    FlipCharactere();
                }
                else if (_move < 0 && isFacingRight)
                {
                    FlipCharactere();
                }
                Vector3 targetVelocity = new Vector2(_move * 10f * moveSpeed * Time.fixedDeltaTime, playerRigidbody2D.velocity.y);
                playerRigidbody2D.velocity = Vector3.SmoothDamp(playerRigidbody2D.velocity, targetVelocity, ref velocity, movementSmoothing);
            }
            if (isGrounded && _isJump)
            {
                isGrounded = false;
                playerRigidbody2D.AddForce(new Vector2(0f, jumpForce));
            }
        }
    }
    #endregion

    #region UniMeths
    void Awake()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    void FixedUpdate()
    {
        bool _wasGrounded = isGrounded;
        isGrounded = false;

        Collider2D[] _colliders = Physics2D.OverlapCircleAll(groundCheck.position, GROUNDEDRADIUS, whatIsGround);
        for (int i = 0; i < _colliders.Length; i++)
        {
            if (_colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                if (!_wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }
    void Start()
    {
        if (!playerRigidbody2D)
        {
            playerRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        }
    }
    #endregion
}