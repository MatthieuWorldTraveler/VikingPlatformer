using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.MoreAttributes;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "SO", fileName = "PlayerStats")]
public class StatsSO : ScriptableObject
{
    [SerializeField][ReadOnly] int _playerMaxLife = 3;
    [SerializeField][ReadOnly] int _playerLife;
    [Space(10)]
    [SerializeField][ReadOnly] int _aleCount;

    [Space(10)]
    [SerializeField][ReadOnly] int _arrayIndex;

    [SerializeField] string[] _sceneName;
   

    public int PlayerLife { get => _playerLife; set => _playerLife = value; }
    public int AleCount { get => _aleCount; set => _aleCount = value; }
    public int PlayerMaxLife { get => _playerMaxLife; }
    public int ArrayIndex { get => _arrayIndex; set => _arrayIndex = value; }
    public string[] SceneName { get => _sceneName; }
}
