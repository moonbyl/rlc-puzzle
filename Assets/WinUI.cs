using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinUI : MonoBehaviour
{
    public TMP_Text scoreText; // Reference ke TextMeshPro UI

    void Start()
    {
        // Ambil dan tampilkan skor kemenangan
        if (ScoreManager.Instance != null)
        {
            scoreText.text = "SKOR: " + ScoreManager.Instance.skorSaatIni.ToString();
        }
    }

    // Untuk tombol Kembali ke Menu Utama
    public void KembaliKeMenu()
    {
        // Reset time scale jika game pernah pause
        Time.timeScale = 1f;
        
        // Hapus semua PlayerPrefs jika diperlukan
        // PlayerPrefs.DeleteAll();
        
        // Kembali ke scene Home
        SceneManager.LoadScene("Home");
        
        // Reset nyawa dan skor saat kembali ke menu
        LifeManager.Instance?.ResetNyawa();
        ScoreManager.Instance?.ResetSkor();
    }

    // Untuk tombol Main Lagi (mulai dari Level1)
    public void MainLagi()
    {
        // Reset time scale
        Time.timeScale = 1f;
        
        // Reset semua progress
        ScoreManager.Instance?.ResetSkor();
        LifeManager.Instance?.ResetNyawa();
        
        // Cari index scene Level1
        int level1Index = -1;
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            
            if (sceneName == "level1")
            {
                level1Index = i;
                break;
            }
        }
        
        // Load Level1 jika ditemukan, jika tidak load scene pertama
        if (level1Index != -1)
        {
            SceneManager.LoadScene(level1Index);
        }
        else
        {
            Debug.LogWarning("Scene Level1 tidak ditemukan! Menggunakan scene pertama saja");
            SceneManager.LoadScene(0);
        }
    }
}