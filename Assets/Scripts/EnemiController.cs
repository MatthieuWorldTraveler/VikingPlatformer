using UnityEngine;

public class EnemiController : CharacterController
{

    bool _charInRange;
    bool _charInRangeAttack;

    [Header("Index 0 = Left // Index 1 = Right !")]
    [SerializeField] Transform[] _patrolPoint;
    [SerializeField] Transform[] _limitPoint;

    [SerializeField] int _lifePoints;

    [SerializeField] float _stopTimer = 1f;
    float _stopCounter; // Stop counter when player out of range
    int _velocity = 0;
    int i; // Array for points
    bool _outLimit;

    Transform _player;



    public bool CharInRangeAttack { get => _charInRangeAttack; set => _charInRangeAttack = value; }
    public bool CharInRange
    {
        get => _charInRange;
        set
        {
            if (!_outLimit)
            {
                _charInRange = value;
            }
        }
    }
    public Transform Player { get => _player; set => _player = value; }
    public int LifePoints { get => _lifePoints;}

    protected override void Start()
    {
        _velocity = i == 0 ? -1 : 1;
        base.Start();
    }

    private void Update()
    {
        InputAndGrounded();

        Attack();

        _outLimit = _transform.position.x < _limitPoint[0].position.x || _transform.position.x > _limitPoint[1].position.x;

    }

    private void FixedUpdate()
    {
        Target();
        MoveChar();
        Jump();
    }

    protected override void Attack()
    {
        if (_charInRangeAttack) //Bool for Char in range
        {
            base.Attack();
        }
    }

    protected override void MoveChar()
    {

        if (Time.time > _stopCounter && !_charInRangeAttack)
            _rigidbody.velocity = new Vector2(_velocity * _movingSpeed, _rigidbody.velocity.y);
        else
            _rigidbody.velocity = new Vector2(0f, _rigidbody.velocity.y);
        base.MoveChar();

        if (_charInRange && !_outLimit)
        {
            if (Time.time > _stopCounter)
            {
                _velocity = _player.position.x < _transform.position.x ? -1 : 1;
                //if ((_transform.right == Vector3.right && _player.position.x < _transform.position.x) || (_transform.right == Vector3.left && _player.position.x > _transform.position.x))
                //{
                //    _stopCounter = Time.time + _stopTimer;
                //}
            }

        }
        else if (_outLimit)
        {
            _charInRange = false;
            _velocity = _transform.position.x < _patrolPoint[i].position.x || _transform.position.x > _patrolPoint[i].position.x ? 1 : -1;
        }

    }

    private void Target()
    {
        if (!_charInRange)
        {
            if (Vector2.Distance(new Vector2(_transform.position.x, 0f), new Vector2(_patrolPoint[i].position.x, 0f)) < .1f)
            {
                i++;
                _stopCounter = Time.time + _stopTimer;
                if (i == _patrolPoint.Length)
                    i = 0;
                _velocity = i == 0 ? -1 : 1;
                if (_outLimit)
                    _outLimit = false;

            }
        }
    }

    protected override void Jump()
    {
        if (_charInRange)
            Debug.Log(_player.position.y + " // " + _transform.position.y + " // " + (_player.position.y > _transform.position.y));
        if (Time.time > _stopCounter && !_charInRangeAttack && _charInRange && _player.position.y > _transform.position.y)
            base.Jump();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapons"))
        {
            LifeChanged();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Weapons"))
        {
            LifeChanged();
        }
    }

    private void LifeChanged()
    {
        if (Time.time > _damageCounter)
        {
            Debug.Log("LifeChanged");
            _damageCounter = Time.time + _timeBtwDamage;
            _lifePoints--;
            if (_lifePoints <= 0)
            {
                _anim.SetTrigger("Fall");
                Destroy(gameObject.transform.parent.gameObject, _fallTimer);
            }
        }
    }
}
