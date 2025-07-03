using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyCanvas : MonoBehaviour 
{
    public string[] activeScenes; // Scene dimana canvas akan aktif

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Cek apakah canvas harus aktif di scene ini
        bool shouldBeActive = false;
        foreach(string sceneName in activeScenes)
        {
            if(scene.name == sceneName)
            {
                shouldBeActive = true;
                break;
            }
        }

        gameObject.SetActive(shouldBeActive);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}