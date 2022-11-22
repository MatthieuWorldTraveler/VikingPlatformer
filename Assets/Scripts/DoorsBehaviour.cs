using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsBehaviour : MonoBehaviour
{
    [SerializeField] Sprite _closed;
    [SerializeField] Sprite _open;

    [SerializeField] bool _canUse;
    SpriteRenderer _renderer;

    public bool CanUse { get => _canUse; }

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_canUse && collision.gameObject.CompareTag("Player"))
        {
            _renderer.sprite = _open;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_canUse && collision.gameObject.CompareTag("Player"))
        {
            _renderer.sprite = _closed;
        }
    }
}
