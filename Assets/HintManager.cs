using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class HintManager : MonoBehaviour
{
    [System.Serializable]
    public class LevelHint
    {
        public string levelName; // Must match exact scene names
        [TextArea(3, 10)]
        public string hintText;
    }

    [Header("UI References")]
    public Button hintButton;
    public GameObject hintPopup;
    public TMP_Text hintTextUI;
    public LevelHint[] levelHints;

    private void Awake()
    {
        // Ensure only one instance exists
        if (FindObjectsOfType<HintManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        InitializeHintSystem();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Loaded scene: {scene.name}"); // Important for debugging
        
        // Reinitialize after 0.1 seconds to ensure UI is ready
        Invoke(nameof(InitializeHintSystem), 0.1f);
    }

    private void InitializeHintSystem()
    {
        // Double-check references
        if (hintButton == null)
            hintButton = GetComponentInChildren<Button>(true);
        
        if (hintPopup == null)
            hintPopup = transform.Find("HintPopup")?.gameObject;
        
        if (hintTextUI == null && hintPopup != null)
            hintTextUI = hintPopup.GetComponentInChildren<TMP_Text>(true);

        // Critical button setup
        if (hintButton != null)
        {
            hintButton.onClick.RemoveAllListeners();
            hintButton.onClick.AddListener(ShowHint);
            
            // Force enable all button components
            hintButton.interactable = true;
            var btnImage = hintButton.GetComponent<Image>();
            if (btnImage != null) btnImage.raycastTarget = true;
            
            // Visual debug - turns green when properly initialized
            hintButton.targetGraphic.color = Color.green;
        }

        Debug.Log($"Hint system initialized for {SceneManager.GetActiveScene().name}");
    }

    public void ShowHint()
    {
        string currentLevel = SceneManager.GetActiveScene().name;
        Debug.Log($"Trying to show hint for: {currentLevel}");

        foreach (var hint in levelHints)
        {
            if (hint.levelName == currentLevel)
            {
                hintTextUI.text = hint.hintText;
                hintPopup.SetActive(true);
                Time.timeScale = 0f;
                return;
            }
        }
        
        Debug.LogError($"No hint configured for: {currentLevel}");
    }

    public void CloseHint()
    {
        hintPopup.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Update()
    {
        // Fallback activation
        if (Input.GetKeyDown(KeyCode.H))
            ShowHint();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}