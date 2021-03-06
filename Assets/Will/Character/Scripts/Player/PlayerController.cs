﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region F/P
    #region Player
    [SerializeField, Header("Player settings")]
    Animator animatorPlayer;
    [SerializeField]
    MyCharacterController2D playerToControl;
    [SerializeField]
    Rigidbody2D playerRigidbody2D;
    [SerializeField] private AudioClip m_jump;
    [SerializeField] private AudioClip m_death;
    [SerializeField] private AudioSource m_SourceEffect;

    float horizontalMove = 0f;
    bool canJump = false;
    bool canCrouch = false;
    #endregion
    #endregion

    #region Meths

    #region PlayerBehaviour     
    public void MakeMeDie(bool _dieAlive)
    {
        m_SourceEffect.clip = m_death;
        m_SourceEffect.Play();
        animatorPlayer.SetBool("MakeMeDie", _dieAlive);
    }
    void MakeMeJump(bool _doIt)
    {
        if (!_doIt) return;
        canJump = true;
        m_SourceEffect.clip = m_jump;
        m_SourceEffect.Play();
        animatorPlayer.SetBool("MakeMeJump", true);

    }
    void MakeMeMove(float _horizontalMove)
    {        
        horizontalMove = _horizontalMove;
        animatorPlayer.SetFloat("MakeMeWalk", Mathf.Abs(_horizontalMove));

        if (!playerToControl.IsGrounded)
        {
            animatorPlayer.SetBool("MakeMeJump", true); 
            }
    }    
    private void OnDeath()
    {
        CameraBehaviour.Instance.ScreenShake();
    }

    #endregion
    #region Events
    public void OnLanding()
    {
        animatorPlayer.SetBool("MakeMeJump", false);
    }
    #endregion
    #endregion

    #region UniMeths
    void Awake()
    {        
        XboxControllerInputManagerWindows.OnADownInputPress += MakeMeJump;
        XboxControllerInputManagerWindows.OnHorizontalAxisInput += MakeMeMove;
        KeyboardInputsManager.OnSpaceClickDownInputPress += MakeMeJump;
        
    }

    void FixedUpdate()
    {        
        playerToControl.Move(horizontalMove, canJump);
        canJump = false;
        if (GameManager.m_JumpSpeedPower)
        {
            playerToControl.jumpForce = 300f;
        }
    }
    void Start()
    {
        if (!playerRigidbody2D)
        {
            playerRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        }
        if (!animatorPlayer)
        {
            animatorPlayer = gameObject.GetComponent<Animator>();
        }
        GameManager.E_Death.AddListener(OnDeath);
        GameManager.m_RespawnPoint = transform.position;
    }
    private void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        if (Mathf.Abs(horizontalMove) > .01f)
        {
            MakeMeMove(horizontalMove);
        }
    }
    #endregion
}