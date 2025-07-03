// ScoreManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    
    // Menggunakan skorSaatIni sebagai variabel utama (ganti currentScore)
    public int skorSaatIni = 0;
    public int skorTerakhir; // Untuk GameOver
    public int skorKemenangan; // Untuk WinScene

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            // Load highscore saat awal game
            PlayerPrefs.GetInt("HighScore", 0);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    public void TambahSkor(int jumlah)
    {
        skorSaatIni += jumlah;
        Debug.Log("Skor sekarang: " + skorSaatIni);
    }

    public int AmbilSkor()
    {
        return skorSaatIni;
    }

    public void SimpanSkorTerakhir()
    {
        skorTerakhir = skorSaatIni;
        PlayerPrefs.SetInt("LastScore", skorSaatIni);
        PlayerPrefs.Save();
        Debug.Log("Skor Terakhir Disimpan: " + skorSaatIni);
    }
    
    public void SimpanSkorKemenangan() {
    PlayerPrefs.SetInt("LastScore", skorSaatIni);
    PlayerPrefs.SetInt("HighScore", Mathf.Max(skorSaatIni, PlayerPrefs.GetInt("HighScore", 0)));
    PlayerPrefs.Save(); // <-- INI YANG PENTING
    Debug.Log("Skor Tersimpan: " + skorSaatIni);
}

    public void ResetSkor()
    {
        skorSaatIni = 0;
        Debug.Log("Skor direset ke 0");
    }

    // Untuk mengambil skor dari PlayerPrefs
    public int GetLastScore()
    {
        return PlayerPrefs.GetInt("LastScore", 0);
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }
}