using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] float _lerp = 5f;


    Transform _transform;

    public Transform Target { get => _target; set => _target = value; }

    private void Start()
    {
        _transform = transform;
    }

    private void Update()
    {
        if (_target != null)
        {
            Vector3 camPos = _target.position;
            camPos.z = _transform.position.z;
            _transform.position = Vector3.Lerp(_transform.position, camPos, _lerp * Time.deltaTime);   
        }      
    }
}
