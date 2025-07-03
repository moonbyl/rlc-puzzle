using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void QuitGame()
    {
        // Untuk memastikan fungsi ini berjalan saat build
        Debug.Log("Keluar dari game...");

        // Jika sedang dijalankan di Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
