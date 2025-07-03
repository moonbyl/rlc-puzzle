using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public TMP_Text scoreText; // Reference ke TextMeshPro UI

    void Start()
    {
        // Ambil skor terakhir dari ScoreManager
        if (ScoreManager.Instance != null)
        {
            scoreText.text = "Skor Akhir: " + ScoreManager.Instance.skorTerakhir;
        }
        
        // Optional: Reset skor untuk permainan baru
        ScoreManager.Instance?.ResetSkor();
    }

    public void KembaliKeMenu()
    {
        SceneManager.LoadScene("Home");
    }
}