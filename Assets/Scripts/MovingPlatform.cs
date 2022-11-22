using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform[] _movePoints;
    [SerializeField] float _platformSpeed;
    [SerializeField] float _platformStopTimer;

    float _stopCounter;
    int i;
    Transform _transform;
    Rigidbody2D _rb;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _transform = transform;
        _transform.position = _movePoints[0].position;
    }
    private void Update()
    {
        if(Vector2.Distance(_transform.position, _movePoints[i].position) < .02f)
        {
            i++;
            _stopCounter = Time.time + _platformStopTimer;
            if (i == _movePoints.Length)
                i = 0;
        }
    }

    private void FixedUpdate()
    {
        if (Time.time > _stopCounter)
            _transform.position = Vector2.MoveTowards(_transform.position, _movePoints[i].position, _platformSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.transform.SetParent(_transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.transform.SetParent(null);
    }

}
