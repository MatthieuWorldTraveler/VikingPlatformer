using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //Serialized var
    [SerializeField] protected int _movingSpeed = 5;
    [SerializeField] protected int _jumpForce = 15;
    [SerializeField] protected int _jumpMaxCount = 1;
    [SerializeField] protected Transform _groundPoint;
    [SerializeField] protected LayerMask _whatsIsGround;
    [SerializeField] protected  float _timeBtwAttack = .5f;
    [SerializeField] protected float _timeBtwDamage = .5f;

    // Private var
    protected float _damageCounter;
    protected float _fallTimer = 0.4f;
    protected int _jumpCount;
    protected float _attackSpeedCounter;
    protected bool _isGrounded;

    protected Rigidbody2D _rigidbody;
    protected Transform _transform;
    protected Animator _anim;

    protected virtual void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _transform = transform;
    }

    protected virtual void InputAndGrounded()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundPoint.position, .2f, _whatsIsGround);
        _anim.SetBool("isGrounded", _isGrounded);
        _anim.SetFloat("speed", Mathf.Abs(_rigidbody.velocity.x));
        if (_isGrounded)
            _jumpCount = 0;
    }

    protected virtual void Attack()
    {
        if (_isGrounded && Time.time > _attackSpeedCounter)
        {
            _anim.SetTrigger("Attack");
            _attackSpeedCounter = Time.time + _timeBtwAttack;
        }
    }

    protected virtual void MoveChar()
    {
        if (_rigidbody.velocity.x < 0)
        {
            _transform.right = Vector2.left;
        }
        else if (_rigidbody.velocity.x > 0)
        {
            _transform.right = Vector2.right;
        }
    }

    protected virtual void Jump()
    {
        if (_jumpCount < _jumpMaxCount)
        {
            Debug.Log("Jumping");
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
            _jumpCount++;
        }
    }


}
