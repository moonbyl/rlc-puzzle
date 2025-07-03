// LightController.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class LightController : MonoBehaviour
{
    public Tempat_Drop[] semuaTempatDrop;
    public Sprite lampuNyala;
    private Sprite lampuMati;
    private SpriteRenderer spriteRenderer;
    private bool sudahMenang = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer tidak ditemukan di objek: " + gameObject.name);
            return;
        }
        lampuMati = spriteRenderer.sprite;
    }

    void Update()
    {
        UpdateLampuStatus();
    }

    public void CekDanKurangiNyawaJikaPerlu()
    {
        bool semuaBenar = CekKebenaranPemasangan();
        spriteRenderer.sprite = semuaBenar ? lampuNyala : lampuMati;

        if (semuaBenar)
        {
            HandleKemenangan();
        }
        else
        {
            LifeManager.Instance?.KurangiNyawa();
        }
    }

    public void UpdateLampuStatus()
    {
        bool semuaBenar = CekKebenaranPemasangan();
        spriteRenderer.sprite = semuaBenar ? lampuNyala : lampuMati;

        if (semuaBenar && !sudahMenang)
        {
            HandleKemenangan();
        }
    }

    private bool CekKebenaranPemasangan()
    {
        foreach (Tempat_Drop tempat in semuaTempatDrop)
        {
            if (tempat == null || !tempat.isCorrectlyPlaced)
            {
                return false;
            }
        }
        return true;
    }

    private void HandleKemenangan()
    {
        if (!sudahMenang && ScoreManager.Instance != null)
        {
            sudahMenang = true;

            // JANGAN biarkan LifeManager mengurangi nyawa saat menang
            LifeManager.Instance?.StopAllCoroutines();

            // Hitung bonus
            int nyawa = LifeManager.Instance?.GetNyawa() ?? 0;
            int bonus = nyawa switch
            {
                3 => 100,
                2 => 90,
                _ => 80
            };

            // Simpan skor SEBELUM pindah scene
            ScoreManager.Instance.TambahSkor(bonus);
            ScoreManager.Instance.SimpanSkorKemenangan();
            PlayerPrefs.Save(); // PASTIKAN tersimpan

            Debug.Log($"Skor Disimpan: {ScoreManager.Instance.AmbilSkor()}");

            if (IsLastLevel())
            {
                Invoke("PindahKeWinScene", 1.5f);
            }
            else
            {
                Invoke("PindahKeLevelBerikutnya", 1.5f);
            }
        }
    }

    private void PindahKeWinScene()
    {
        string winScene = "WinScene";
        if (Application.CanStreamedLevelBeLoaded(winScene))
        {
            SceneManager.LoadScene(winScene);
        }
        else
        {
            Debug.LogError($"Scene {winScene} tidak ditemukan!");
            // Fallback ke WinManager jika WinScene tidak ada
            WinManager.Instance?.ShowWinScreen(ScoreManager.Instance.AmbilSkor());
        }
    }

    private void ShowLevelCompleteScore(int score)
    {
        Debug.Log($"Level Complete! Skor: {score}");
        // Tambahkan efek visual sementara di sini jika perlu
    }

    void PindahKeLevelBerikutnya()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Reset nyawa sebelum pindah ke level berikutnya
            LifeManager.Instance?.ResetNyawa();
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // Jika ternyata ini adalah level terakhir (tapi IsLastLevel() false)
            PindahKeWinScene();
        }
    }

    bool IsLastLevel() {
    int currentIndex = SceneManager.GetActiveScene().buildIndex;
    return currentIndex == SceneManager.sceneCountInBuildSettings - 1;
}
}