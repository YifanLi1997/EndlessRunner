using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: fix the bug that should be related to this script
// 1. Player can get stuck in front of an obstacle
//    the new collison is not captured. Config by script PlayerHealth, variable _hit

public class InputController : MonoBehaviour
{
    public static bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;

    private bool _isDragging = false;
    private Vector2 _touchStartPos = Vector2.zero;
    private Vector2 _swipeDelta = Vector2.zero;

    [Header("Only for Config")]
    [SerializeField] bool c_tap;
    [SerializeField] bool c_swipeLeft;
    [SerializeField] bool c_swipeRight;
    [SerializeField] bool c_swipeUp;
    [SerializeField] bool c_swipeDown;
    [SerializeField] Vector2 c_touchStartPos;
    [SerializeField] Vector2 c_swipeDelta;


    void Update()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        UseMouse();
        UseTouch();

        if (_swipeDelta.magnitude > 100) // TODO: test the threshold
        {
            float x = _swipeDelta.x;
            float y = _swipeDelta.y;
            PickMainDirection(x, y);

            AssignConfigVariables();
            ResetTouch();
        }
    }

    private void AssignConfigVariables()
    {
        c_tap = tap;
        c_swipeLeft = swipeLeft;
        c_swipeRight = swipeRight;
        c_swipeUp = swipeUp;
        c_swipeDown = swipeDown;
        c_touchStartPos = _touchStartPos;
        c_swipeDelta = _swipeDelta;
    }

    private void PickMainDirection(float x, float y)
    {
        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            if (x > 0) swipeRight = true;
            else swipeLeft = true;
        }
        else
        {
            if (y > 0) swipeUp = true;
            else swipeDown = true;
        }
    }

    private void UseMouse()
    {
        GetMouseInput();
        if (_isDragging) CalculateMouseSwipeDelta();
    }

    private void CalculateMouseSwipeDelta()
    {
        if (Input.GetMouseButton(0))
        {
            _swipeDelta = (Vector2)Input.mousePosition - _touchStartPos;
        }
    }

    private void GetMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CatchTouch();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ResetTouch();
        }
    }

    private void UseTouch()
    {
        GetTouchInput();
        if (_isDragging) CalculateTouchSwipeDelta();

    }

    private void CalculateTouchSwipeDelta()
    {
        if (Input.touches.Length > 0) // TODO: Confused about why both < and > work here
        {
            _swipeDelta = Input.touches[0].position - _touchStartPos;
        }
}

    private void GetTouchInput()
    {
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                CatchTouch();
            }
            else if (Input.touches[0].phase == TouchPhase.Ended
                || Input.touches[0].phase == TouchPhase.Canceled)
            {
                ResetTouch();
            }
        }
    }

    private void CatchTouch()
    {
        tap = true;
        _isDragging = true;
        _touchStartPos = Input.mousePosition;
    }

    private void ResetTouch()
    {
        _touchStartPos = Vector2.zero;
        _swipeDelta = Vector2.zero;
        _isDragging = false;
    }
}
