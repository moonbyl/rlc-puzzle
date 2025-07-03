using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance;

    public int nyawa = 3;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private bool isLevel1 = true;
    private bool sudahMenang = false; // Added missing variable declaration

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isLevel1 = scene.name == "Level1";
        
        // JANGAN reset nyawa kalau di WinScene/GameOver
        if (scene.name == "WinScene" || scene.name == "GameOver") 
        {
            return;
        }

        // Reset hanya di level biasa jika belum menang
        if (!sudahMenang) 
        {
            ResetNyawa();
        }
        UpdateHeartReferences();
    }

    public void SetMenangState(bool menang)
    {
        sudahMenang = menang;
    }

    private void UpdateHeartReferences()
    {
        GameObject heartsContainer = GameObject.Find("HeartsContainer");
        if (heartsContainer != null)
        {
            hearts = heartsContainer.GetComponentsInChildren<Image>();
            UpdateHeartsUI();
        }
    }

    public void WrongComponentInstalled()
    {
        KurangiNyawa();
    }

    public void KurangiNyawa()
    {
        if (nyawa <= 0 || sudahMenang) return; // Jangan kurangi nyawa jika sudah menang

        nyawa--;
        UpdateHeartsUI();

        if (nyawa <= 0)
        {
            HandleGameOver();
        }
    }

    public void HandleGameOver()
    {
        if (sudahMenang) return; // Jangan proses game over jika sudah menang

        // PASTIKAN skor tersimpan sebelum pindah
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.SimpanSkorTerakhir();
            PlayerPrefs.Save();
            Debug.Log($"Skor GameOver: {ScoreManager.Instance.AmbilSkor()}");
        }

        SceneManager.LoadScene("GameOver");
    }

    private void UpdateHeartsUI()
    {
        if (hearts == null || hearts.Length == 0) return;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] != null)
            {
                hearts[i].sprite = (i < nyawa) ? fullHeart : emptyHeart;
            }
        }
    }

    public int GetNyawa()
    {
        return nyawa;
    }

    public void ResetNyawa()
    {
        nyawa = 3;
        sudahMenang = false; // Reset status menang juga
        UpdateHeartsUI();
    }
}