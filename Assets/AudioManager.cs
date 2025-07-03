using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // jika kamu ingin ganti icon mute/unmute

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Source")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clip")]
    public AudioClip bgmLevel;
    public AudioClip winSound;
    public AudioClip gameOverSound;
    public AudioClip clickSound;

    private bool isMuted = false;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // tetap hidup saat ganti scene
        }
        else
        {
            Destroy(gameObject); // hancurkan jika sudah ada Instance
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded; // pindah ke Awake agar tidak dobel-daftar
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name.ToLower(); // hindari case-sensitive

        if (sceneName.StartsWith("level"))
        {
            PlayMusic(bgmLevel, true);
        }
        else if (sceneName == "gameover")
        {
            PlayMusic(gameOverSound, false);
        }
        else if (sceneName == "winscene")
        {
            PlayMusic(winSound, false);
        }
        else
        {
            musicSource.Stop(); // scene lain: tidak ada musik
        }
    }

    public void PlayMusic(AudioClip clip, bool loop)
    {
        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.loop = loop;

            if (!isMuted)
            {
                musicSource.Play();
            }
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (!isMuted && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayClickSound()
    {
        PlaySFX(clickSound);
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;

        musicSource.mute = isMuted;
        sfxSource.mute = isMuted;

        // 🔁 Jika ingin ubah ikon mute/unmute, tinggal panggil fungsi UI di sini
        // contoh (opsional, nanti dibuat UI-nya):
        // MuteButton.Instance.UpdateIcon(isMuted);
    }

    public bool IsMuted()
    {
        return isMuted;
    }
}
