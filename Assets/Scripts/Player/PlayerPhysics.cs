using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [SerializeField] float laneWidth = 1.5f;

    private CharacterController _characterController;
    private Animator _animator;

    private Vector3 _moveDirection;
    [SerializeField] float forwardSpeed = 15f;
    [SerializeField] float maxForwardSpeed = 20f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float gravity = -20f;

    [SerializeField] AudioClip jumpClip;
    [SerializeField] float jumpClipVolume = 0.5f;


    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _moveDirection.z = forwardSpeed;
    }

    
    void Update()
    {
        if (!PlayerManager.gameStarted || PlayerManager.gameOver) return;

        IncreaseForwardSpeed();
        Gravity();
        LeftRightMove();
        Jump();
        StartCoroutine(Slide());

    }

    IEnumerator Slide()
    {
        if (InputController.swipeDown)
        {
            _animator.SetBool("IsSliding", true);
            _characterController.center = new Vector3(0, -0.5f, 0);
            _characterController.height = 0.1f;

            yield return new WaitForSeconds(0.45f); // length of the animator
            _characterController.center = new Vector3(0, 0, 0);
            _characterController.height = 2;
            _animator.SetBool("IsSliding", false) ;
        }
    }

    private void IncreaseForwardSpeed()
    {
        if (forwardSpeed < maxForwardSpeed)
        {
            forwardSpeed += 0.1f * Time.deltaTime;
            _moveDirection.z = forwardSpeed;
        }
    }

    private void Gravity()
    {
        _moveDirection.y += gravity * Time.deltaTime;
    }

    private void Jump()
    {
        if (_characterController.isGrounded)
        {
            _animator.SetBool("IsJumping", false);
            //if (Input.GetKeyDown(KeyCode.UpArrow))
            if (InputController.swipeUp)
            {
                _animator.SetBool("IsJumping", true);
                _moveDirection.y = jumpForce;
                AudioSource.PlayClipAtPoint(jumpClip, Camera.main.transform.position, jumpClipVolume);
            }
            
        }
    }

    private void LeftRightMove()
    {
        Vector3 targetPos = transform.position;

        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        if (InputController.swipeLeft)
        {
            targetPos.x = Mathf.Clamp(transform.position.x - laneWidth, -laneWidth, laneWidth);
        }
        //else if (Input.GetKeyDown(KeyCode.RightArrow))
        else if (InputController.swipeRight)
        {
            targetPos.x = Mathf.Clamp(transform.position.x + laneWidth, -laneWidth, laneWidth);
        }

        #region Notes for a bug
        /* Solution 1 - https://forum.unity.com/threads/character-controller-ignores-transform-position.617107/
         * The character controller has it's own internal definition of what position it has,
         * and will set the transform's position to that every frame, so you can't move it with transform.position.
         * /

        //_characterController.enabled = false;
        //_characterController.transform.position = targetPos;
        //_characterController.enabled = true;

        /* Solution 2 - https://issuetracker.unity3d.com/issues/charactercontroller-overrides-objects-position-when-teleporting-with-transform-dot-position?_ga=2.98612951.855228812.1593079713-495400138.1588009320
         * auto sync transforms is disabled in the physics settings,
         * so characterController.Move() won't necessarily be aware of the new pose as set by the transform
         */
        #endregion

        //transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 80);
        transform.position = targetPos; // TODO: animation to smooth the transition
    }

    private void FixedUpdate()
    {
        if (!PlayerManager.gameStarted || PlayerManager.gameOver) return;

        _animator.SetBool("IsRunning", true);
        _characterController.Move(_moveDirection * Time.fixedDeltaTime);
    }
}
