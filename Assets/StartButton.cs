using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void StartGame()
    {
        // Reset skor melalui ScoreManager
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetSkor();
            Debug.Log("Skor telah direset");
        }
        else
        {
            Debug.LogWarning("ScoreManager.Instance tidak ditemukan");
        }

        // Load scene level1
        SceneManager.LoadScene("level1");
        
        Debug.Log("Memulai level1...");
    }
}