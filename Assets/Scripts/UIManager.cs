using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public Button menuButton;
    public Button resumeButton;
    public Button exitButton;
    public Toggle muteToggle;
    public GameObject menuPanel;
    public GameObject mainCamera;
    public bool isMuted = false;
    public Image buttonImage;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScoreText();
        UpdateLivesText();
        UpdateLevelText();
        // Add listener for toggle changes
        // Set the correct sprite based on saved settings
        bool isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
        muteToggle.isOn = isMuted;
        UpdateButtonImage(isMuted);
        muteToggle.onValueChanged.AddListener(delegate { ToggleSound(muteToggle.isOn); });
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
        Debug.Log("Quit!");
    }

    public void ToggleSound(bool isMuted)
    {
        AudioListener.pause = isMuted; // Mute/unmute audio globally
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0); // Save preference
        UpdateButtonImage(isMuted);
    }

    public void UpdateButtonImage(bool isMuted)
    {
        buttonImage.sprite = isMuted ? soundOffSprite : soundOnSprite;
    }
}
    

