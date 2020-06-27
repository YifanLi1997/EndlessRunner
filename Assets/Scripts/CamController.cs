using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] Transform targetFollowed;
    private Vector3 _offset;

    void Start()
    {
        _offset = transform.position - targetFollowed.position;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, targetFollowed.position.z + _offset.z);
    }
}
