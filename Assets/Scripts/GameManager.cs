using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] StatsSO _playerHealth;
    [SerializeField] TMP_Text _aleCountTxt;

    private void Start()
    {
        _aleCountTxt.text = _playerHealth.AleCount.ToString();
    }

    public void AlePicked()
    {
        _playerHealth.AleCount++;
        _aleCountTxt.text = _playerHealth.AleCount.ToString();
    }

    public void HealthLost()
    {
        if (_playerHealth.PlayerLife > 1)
            _playerHealth.PlayerLife--;
        else
        {
            _playerHealth.PlayerLife--;
            SceneManager.LoadScene("LoseMenu");
        }
    }

    public void MuttonAte()
    {
        if (_playerHealth.PlayerLife < 3)
            _playerHealth.PlayerLife++;
    }

    public void NextLevel()
    {
        Debug.Log("Door");
        _playerHealth.ArrayIndex++;
        if (_playerHealth.ArrayIndex < _playerHealth.SceneName.Length)
        {
            SceneManager.LoadScene(_playerHealth.SceneName[_playerHealth.ArrayIndex]);
        }
        else
            SceneManager.LoadScene("WinMenu");
    }
}
