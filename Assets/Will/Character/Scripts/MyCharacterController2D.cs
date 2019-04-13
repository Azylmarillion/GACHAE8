using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class MyCharacterController2D : MonoBehaviour
{
    #region F/P    
    const float CEILINGRADIUS = .02f;
    //[Range(0, 1)] [SerializeField]
    //float crouchSpeed = 1;
    const float GROUNDEDRADIUS = .2f;
    [SerializeField,Range(0, 1000)]
    float jumpForce = 100f;
    [Range(0, .3f)] [SerializeField]
    float movementSmoothing = .05f;
    [SerializeField, Range(.1f, 50)]
    float moveSpeed = 7;
    [SerializeField]
    bool canAirControl = false;
    //public bool CanFlipCrouch = false;
    //public bool DisableColliderInJump = false;
    [SerializeField]
    bool isFacingRight = true;
    [SerializeField]
    bool isGrounded;
    public bool IsGrounded { get { return isGrounded; } }
    //[SerializeField]
    //bool isjumping = false;
    [SerializeField]
    LayerMask whatIsGround;
    [SerializeField]
    Rigidbody2D playerRigidbody2D;
    [SerializeField]
    Transform groundCheck;                           
    [SerializeField]
    Transform ceilingCheck;                          
    //[SerializeField]
    //Collider2D colliderToDisable;
    //[SerializeField]
    Vector3 velocity = Vector3.zero;
    [Header("Events")]
    [Space]
    public UnityEvent OnLandEvent;
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    //public BoolEvent OnCrouchEvent;
    //bool wasCrouching = false;
    #endregion

    #region Meths
    //void DisableCollider()
    //{
    //    colliderToDisable.enabled = false;
    //    //Debug.Log("collider" + colliderToDisable.enabled);
    //}
    //void EnableCollider()
    //{
    //    colliderToDisable.enabled = true;
    //    //Debug.Log("collider" + colliderToDisable.enabled);
    //}
    void FlipCharactere()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }

    public void Move(float _move, /*bool _isCrouch,*/ bool _isJump)
    {
        //if (!_isCrouch)
        //{
        //    if (Physics2D.OverlapCircle(ceilingCheck.position, CEILINGRADIUS, whatIsGround))
        //    {
        //        _isCrouch = true;
        //        //si ball bloquer en ball tant que _isCrouch true
        //    }
        //}

        if (isGrounded || canAirControl)
        {

            //if (_isCrouch)
            //{
            //    if (!wasCrouching)
            //    {
            //        wasCrouching = true;
            //        OnCrouchEvent.Invoke(true);
            //    }

            //    _move *= crouchSpeed;

            //    if (colliderToDisable != null)
            //        DisableCollider();
            //}
            //else
            //{
            //    if (colliderToDisable != null)
            //        EnableCollider();

            //    if (wasCrouching)
            //    {
            //        wasCrouching = false;
            //        OnCrouchEvent.Invoke(false);
            //    }
            //}
            if (_move > 0 && !isFacingRight)
            {
                FlipCharactere();
            }
            else if (_move < 0 && isFacingRight)
            {
                FlipCharactere();
            }
            //if (CanFlipCrouch && wasCrouching) return;            
               Vector3 targetVelocity = new Vector2(_move * 10f * moveSpeed * Time.fixedDeltaTime,playerRigidbody2D.velocity.y);
               playerRigidbody2D.velocity = Vector3.SmoothDamp(playerRigidbody2D.velocity, targetVelocity, ref velocity, movementSmoothing);            
        }
        if (isGrounded && _isJump)
        {
            isGrounded = false;            
            playerRigidbody2D.AddForce(new Vector2(0f, jumpForce));
            //isjumping = true;////
            //Debug.Log("jumping"+isjumping);
            //if (DisableColliderInJump)
            //{
            //    DisableCollider();
            //}
        }
    }
    #endregion

    #region UniMeths
    void Awake()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        //if (OnCrouchEvent == null)
        //    OnCrouchEvent = new BoolEvent();
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
                //isjumping = false;
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