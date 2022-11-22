using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] Sprite _damaged;
    [SerializeField] Sprite _broke;
    int _lifeCount = 2;

    Collider2D _collider;
    SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapons"))
        {
            switch (_lifeCount)
            {
                case 2:
                    _renderer.sprite = _damaged;
                    _lifeCount--;
                    break;
                case 1:
                    _renderer.sprite = _broke;
                    _collider.isTrigger = true;
                    break;
                default:
                    Debug.Log("Error404");
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Weapons"))
        {
            switch (_lifeCount)
            {
                case 2:
                    _renderer.sprite = _damaged;
                    _lifeCount--;
                    break;
                case 1:
                    _renderer.sprite = _broke;
                    _collider.isTrigger = true;
                    break;
                default:
                    Debug.Log("Error404");
                    break;
            }
        }
    }
}
