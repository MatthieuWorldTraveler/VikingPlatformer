using UnityEngine;

public class PlayerController : CharacterController
{

    //Serialized var
    [SerializeField] LayerMask _whatsIsOnlyGround;
    [SerializeField] GameManager _statsManager;

    // Private var
    float _fallCounter;
    bool _fell = false;
    Vector2 _lastGroundPos;
    GameObject _otherObj = null;
    float _horizontalAxisRaw;


    // Thor Attack part
    [SerializeField] GameObject _hammer;
    [SerializeField] Rigidbody2D _hammerPrefab;
    [SerializeField] Camera _camera2;
    [SerializeField] GameObject _canvasCamera2;
    [SerializeField] int _hammerSpeed;
    Rigidbody2D _hammerInstantiate;
    bool _specialAttack;
    bool _landSpecialAttack;
    bool _haveHammer = true;
    bool _hammerComingBack;
    bool _hammerComingBackAnimBool;
    [SerializeField] int _backSpeed = 10;
    float _specialAttackCounter = 0;

    private void Update()
    {
        InputAndGrounded();

        if (!_fell)
        {
            Jump();
            Attack();
            SpecialAttack();
            _fallCounter = Time.time + _fallTimer;
        }

        if (_fell && Time.time > _fallCounter)
        {
            _fell = false;
            _transform.position = _lastGroundPos;
        }

        if (_specialAttack)
            _specialAttackCounter += Time.deltaTime;

        SpecialAttackLanding();
        CallBackHammer();

        if (_otherObj != null && Input.GetButtonDown("DoorInteract"))
            _statsManager.NextLevel();
    }

    protected override void InputAndGrounded()
    {

        _horizontalAxisRaw = Input.GetAxisRaw("Horizontal");
        base.InputAndGrounded();
        bool okToSaveLastPos = Physics2D.OverlapCircle(_groundPoint.position, .2f, _whatsIsOnlyGround);
        if (okToSaveLastPos)
            _lastGroundPos = _transform.position - _transform.right;
    }

    private void SpecialAttackLanding()
    {
        if (_landSpecialAttack)
        {
            _haveHammer = false;
            GiveStrengh();
        }
    }

    private void CallBackHammer()
    {
        if (_hammerInstantiate != null)
        {
            _hammerComingBackAnimBool = _hammerInstantiate.GetComponent<HammerBehaviour>().IsCallingBack;
            if (_hammerComingBack || _hammerComingBackAnimBool)
            {
                _hammerInstantiate.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = false;
                _hammerInstantiate.gravityScale = 0;
                _hammerInstantiate.transform.position = Vector2.MoveTowards(_hammerInstantiate.transform.position, _transform.position, _backSpeed * Time.deltaTime);

                if (Vector2.Distance(_transform.position, _hammerInstantiate.transform.position) < .5f)
                {
                    _camera2.GetComponent<CameraFollow>().Target = null;
                    _canvasCamera2.SetActive(false);
                    _rigidbody.bodyType = RigidbodyType2D.Dynamic;
                    _hammerInstantiate.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = true;
                    Destroy(_hammerInstantiate.gameObject);
                    _hammer.SetActive(true);
                    _anim.SetTrigger("CatchBack");
                    _haveHammer = true;
                    _hammerComingBack = false;
                    _hammerInstantiate.GetComponent<HammerBehaviour>().IsCallingBack = false;
                }
            }
        }
    }

    private void GiveStrengh()
    {
        _hammerInstantiate = Instantiate(_hammerPrefab, _hammer.transform.position, Quaternion.identity);
        _hammerInstantiate.transform.right = _transform.right;
        _camera2.GetComponent<CameraFollow>().Target = _hammerInstantiate.transform;
        _canvasCamera2.SetActive(true);
        _hammer.SetActive(false);
        Vector2 strengh;
        // SetUp the strengh to put to the hammer
        switch (_specialAttackCounter)
        {

            case > 5:
                strengh = (_hammerInstantiate.transform.right * (_hammerSpeed * 16)) + (Vector3.up * 5);
                break;
            case > 4:
                strengh = (_hammerInstantiate.transform.right * (_hammerSpeed * 12)) + (Vector3.up * 5);
                break;
            case > 3:
                strengh = (_hammerInstantiate.transform.right * (_hammerSpeed * 8)) + (Vector3.up * 5);
                break;
            case > 2:
                strengh = (_hammerInstantiate.transform.right * (_hammerSpeed * 4)) + (Vector3.up * 5);
                break;
            case > 1:
                strengh = (_hammerInstantiate.transform.right * (_hammerSpeed * 2)) + (Vector3.up * 5);
                break;
            case > 0:
                strengh = (_hammerInstantiate.transform.right * (_hammerSpeed * 1)) + (Vector3.up * 5);
                break;
            default:
                strengh = Vector2.zero;
                break;
        }
        _hammerInstantiate.AddForce(strengh, ForceMode2D.Impulse);
        _landSpecialAttack = false;
    }



    private void SpecialAttack()
    {
        if (_haveHammer && _otherObj == null)
        {
            if (_isGrounded && Input.GetButtonDown("Interact"))
            {
                _anim.SetBool("SpecialAttack", true);
                _specialAttack = true;
                _specialAttackCounter = 0;
            }
            if (_specialAttack && Input.GetButtonUp("Interact"))
            {
                _specialAttack = false;
                _anim.SetBool("SpecialAttack", false);
                if (_specialAttackCounter < 20f)
                {
                    _landSpecialAttack = true;
                }
            }
        }
        // Hammer Callback
        if (!_haveHammer && !_landSpecialAttack && (!_hammerComingBack || !_hammerComingBackAnimBool) && Input.GetButtonDown("Recup"))
        {
            _hammerInstantiate.constraints = RigidbodyConstraints2D.None;
            if (_hammerInstantiate.GetComponent<HammerBehaviour>().Collided)
                _hammerInstantiate.GetComponent<Animator>().SetBool("isCallingBack", true);
            else
                _hammerComingBack = true;
        }
    }

    private void FixedUpdate()
    {
        if (!_fell && !_specialAttack)
        {
            MoveChar();
        }
    }

    protected override void Attack()
    {
        if (_haveHammer && !_specialAttack && Input.GetButtonDown("Fire1"))
        {
            base.Attack();
        }
    }

    protected override void MoveChar()
    {
        _rigidbody.velocity = new Vector2(_horizontalAxisRaw * _movingSpeed, _rigidbody.velocity.y);
        base.MoveChar();
    }

    protected override void Jump()
    {
        if (!_specialAttack && Input.GetButtonDown("Jump"))
            base.Jump();

        // Stop jump mid time
        if (_rigidbody.velocity.y > 0f && Input.GetButtonUp("Jump"))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OutOfMap"))
        {
            _statsManager.HealthLost();
            _fell = true;
            _anim.SetTrigger("Fall");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Doors"))
        {
            _otherObj = collision.gameObject;
        }

        else if (collision.gameObject.CompareTag("Ale"))
        {
            Destroy(collision.gameObject);
            _statsManager.AlePicked();
        }

        else if (collision.gameObject.CompareTag("Mutton"))
        {
            Destroy(collision.gameObject);
            _statsManager.MuttonAte();
        }

        else if (collision.gameObject.CompareTag("EnnemiWeapon"))
        {
            if (Time.time > _damageCounter)
            {
                _damageCounter = Time.time + _timeBtwDamage;
                _statsManager.HealthLost();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Doors"))
        {
            _otherObj = null;
        }
    }
}
