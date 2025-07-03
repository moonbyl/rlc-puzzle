using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TMP_Text))]
public class LevelDisplay : MonoBehaviour 
{
    private TMP_Text levelText;

    void Awake()
    {
        levelText = GetComponent<TMP_Text>();
        SceneManager.activeSceneChanged += OnSceneChanged;
        UpdateLevelText();
    }

    void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        UpdateLevelText();
    }

    void UpdateLevelText()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if(sceneName.StartsWith("level"))
        {
            string levelNum = sceneName.Replace("level", "").Trim();
            levelText.text = $"LEVEL {levelNum}";
        }
    }
}