using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public Button menuButton;
    public Button resumeButton;
    public Button exitButton;
    public GameObject menuPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScoreText();
        UpdateLivesText();
        UpdateLevelText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + GameManager.Instance.userScore;
    }

    public void UpdateLivesText()
    {
        livesText.text = "Lives: " + GameManager.Instance.userLives;
    }

    public void UpdateLevelText()
    {
        levelText.text = "Level: " + GameManager.Instance.gameLevel;
    }

    public void OpenMenu()
    {
        menuPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseMenu()
    {
        menuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
