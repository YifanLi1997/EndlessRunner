using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    //private void OnDisable()
    //{
    //    _animator.SetTrigger("IsDisappearing");

    //    StartCoroutine(DelayDisable());
    //}

    //IEnumerator DelayDisable()
    //{
    //    Debug.Log("wait");
    //    yield return new WaitForSeconds(1f);
    //    Debug.Log("disappear");
    //}
}
