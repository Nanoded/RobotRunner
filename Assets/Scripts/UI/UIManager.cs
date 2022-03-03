using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _loseSceen;
    [SerializeField] private GameObject _startScreen;
    private void Awake()
    {
        _loseSceen.SetActive(false);
        _startScreen.SetActive(true);
        Time.timeScale = 0;
    }
    public void StartButton()
    {
        Time.timeScale = 1;
        _startScreen.SetActive(false);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
