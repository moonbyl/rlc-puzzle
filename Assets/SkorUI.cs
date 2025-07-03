using UnityEngine;
using TMPro;

public class SkorUI : MonoBehaviour
{
    public TextMeshProUGUI teksSkor;

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // tetap hidup saat pindah scene
    }

    void Update()
    {
        if (ScoreManager.Instance != null)
        {
            teksSkor.text = "Skor: " + ScoreManager.Instance.skorSaatIni.ToString();
        }
    }
}
