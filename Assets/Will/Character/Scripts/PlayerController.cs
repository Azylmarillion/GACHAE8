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
    float horizontalMove = 0f;
    bool canJump = false;
    bool canCrouch = false;
    #endregion
    #endregion

    #region Meths
    #region PlayerBehaviour  
    //void MakeMeCrouch(int _verticalAxisValue)
    //{
    //    if (_verticalAxisValue < -.2f && playerToControl.IsGrounded && !canCrouch && Mathf.Abs(horizontalMove) < .9f)
    //    {
    //        canCrouch = true;
    //        playerRigidbody2D.velocity = Vector2.zero;
    //        animatorPlayer.SetBool("MakeMeCrouch", true);
    //        return;
    //    }
    //    else if (_verticalAxisValue < -.2f && playerToControl.IsGrounded && canCrouch)
    //    {
    //        canCrouch = true;
    //        animatorPlayer.SetBool("MakeMeCrouch", true);
    //    }
    //    else if ((_verticalAxisValue > .2f || !playerToControl.IsGrounded || Mathf.Abs(horizontalMove) > .9f))
    //    {
    //        canCrouch = false;
    //        animatorPlayer.SetBool("MakeMeCrouch", false);
    //        return;
    //    }
    //}
    void MakeMeJump(bool _doIt)
    {
        if (!_doIt) return;
        canJump = true;
        canCrouch = false;
        //animatorPlayer.SetBool("MakeMeJump", true);
    }
    void MakeMeMove(float _horizontalMove)
    {        
        horizontalMove = _horizontalMove;
        //animatorPlayer.SetFloat("MakeMeWalk", Mathf.Abs(_horizontalMove));
        //if (!playerToControl.IsGrounded) animatorPlayer.SetBool("MakeMeJump", true);        
    }
    #endregion
    #region Events
    public void OnLanding()
    {
       // animatorPlayer.SetBool("MakeMeJump", false);
    }
    public void OnCrouch(bool _canStand)
    {

    }
    #endregion
    #endregion

    #region UniMeths
    void Awake()
    {        
        XboxControllerInputManagerWindows.OnADownInputPress += MakeMeJump;
        XboxControllerInputManagerWindows.OnHorizontalAxisInput += MakeMeMove;
    }
    void FixedUpdate()
    {
        playerToControl.Move(horizontalMove, /*canCrouch,*/ canJump);
        canJump = false;
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
    } 
    #endregion
}