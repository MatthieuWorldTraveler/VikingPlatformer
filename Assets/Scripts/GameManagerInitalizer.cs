using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerInitalizer : MonoBehaviour
{
    [SerializeField] StatsSO _stats;

    private void Start()
    {
        _stats.PlayerLife = _stats.PlayerMaxLife;
        _stats.AleCount = 0;
        _stats.ArrayIndex = 0;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StartTheGame()
    {
        SceneManager.LoadScene(_stats.SceneName[0]);
    }
}
