using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinManager : MonoBehaviour
{
    public static WinManager Instance;
    
    [Header("UI References")]
    public GameObject winPanel;
    public TMP_Text winScoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        winPanel.SetActive(false);
    }

    public void ShowWinScreen(int finalScore)
    {
        winScoreText.text = "SKOR AKHIR: " + finalScore;
        winPanel.SetActive(true);
        Time.timeScale = 0f; // Jeda game
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Home");
    }
}