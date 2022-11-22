using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartBehaviour : MonoBehaviour
{
    [SerializeField] StatsSO _playerHelth;
    [SerializeField] Sprite _full;
    [SerializeField] Sprite _empty;
    [SerializeField] int _heartID = 0;

    Image _renderer;

    private void Start()
    {
        _renderer = GetComponent<Image>();
    }

    private void Update()
    {
        if (_playerHelth.PlayerLife < _heartID)
            _renderer.sprite = _empty;
        else if (_playerHelth.PlayerLife >= _heartID)
            _renderer.sprite = _full;


    }

}
