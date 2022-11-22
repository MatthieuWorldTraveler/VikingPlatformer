using UnityEngine;

public class HammerBehaviour : MonoBehaviour
{

    Rigidbody2D _rb;

    [SerializeField] bool _isCallingBackAnim;
    bool _isCallingBack;
    bool _Collided = false;

    public bool IsCallingBack { get => _isCallingBack; set => _isCallingBack = value; }
    public bool Collided { get => _Collided; }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isCallingBackAnim)
            _isCallingBack = _isCallingBackAnim;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Chandelier" && collision.gameObject.tag != "OutOfMap" && !(collision.gameObject.CompareTag("Ennemi") && collision.gameObject.GetComponent<EnemiController>().LifePoints != 0))
        {
            _Collided = true;
            _rb.bodyType = RigidbodyType2D.Kinematic;
            _rb.velocity = Vector2.zero;
            _rb.transform.parent = collision.gameObject.transform;
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            GetComponentInChildren<Collider2D>().enabled = false;
        }
    }
}
