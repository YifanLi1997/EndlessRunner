﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [SerializeField] float laneWidth = 1.5f;

    private CharacterController _characterController;

    private Vector3 _moveDirection;
    [SerializeField] float forwardSpeed = 5f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float gravity = -20f;


    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _moveDirection.z = forwardSpeed;
    }

    
    void Update()
    {
        Gravity();
        Jump();
        LeftRightMove();
    }

    private void Gravity()
    {
        _moveDirection.y += gravity * Time.deltaTime;
    }

    private void Jump()
    {
        // Todo: fix the bug that the player take obstacles as ground
        if (_characterController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _moveDirection.y = jumpForce;
            }
        }
        
    }

    private void LeftRightMove()
    {
        Vector3 targetPos = transform.position;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetPos.x = Mathf.Clamp(transform.position.x - laneWidth, -laneWidth, laneWidth);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetPos.x = Mathf.Clamp(transform.position.x + laneWidth, -laneWidth, laneWidth);
        }

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

        //transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 80);
        transform.position = targetPos; // TODO: animation to smooth the transition
    }

    private void FixedUpdate()
    {
        _characterController.Move(_moveDirection * Time.fixedDeltaTime);
    }
}