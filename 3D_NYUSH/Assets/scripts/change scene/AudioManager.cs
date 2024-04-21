using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    // 音频列表和对应的场景名称列表
    public AudioClip[] audioClips;
    public string[] sceneNames;
    private int currentIndex = 0;
    public bool haskey;
    // 音量变量
    [Range(0f, 1f)]
    public float volume = 1f;

    // 存储当前正在播放的音频源
    private AudioSource currentAudioSource;
    public GameObject circle;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        // 只有在当前场景为 "transition scene" 时才播放音频
        if (SceneManager.GetActiveScene().name == "transition scene")
        {
            PlayAudio();
        }
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (circle == null)
        {
            // 在场景中查找带有特定标签的物体，并将其赋值给变量
            circle = GameObject.FindGameObjectWithTag("circle");
            if (circle != null)
            {
                circle.SetActive(true);
            }
        }
        // 当场景加载完成时，检查是否需要播放音频
        if (scene.name == "transition scene")
        {
            PlayAudio();
        }
        
    }

    void PlayAudio()
    {
        if (currentIndex < audioClips.Length && currentIndex < sceneNames.Length)
        {
            StartCoroutine(PlayAudioCoroutine(audioClips[currentIndex], sceneNames[currentIndex]));
            currentIndex++;
        }
    }

    IEnumerator PlayAudioCoroutine(AudioClip audioClip, string sceneName)
    {
        // 添加一个新的音频源并设置音量
        currentAudioSource = gameObject.AddComponent<AudioSource>();
        currentAudioSource.clip = audioClip;
        currentAudioSource.volume = volume;
        currentAudioSource.Play();
        yield return new WaitForSeconds(audioClip.length);
        // 销毁音频源
        Destroy(currentAudioSource);
        SceneManager.LoadScene(sceneName);
    }

    // 公共方法用于设置音频音量
    public void SetVolume(float vol)
    {
        volume = vol;
        // 如果有正在播放的音频源，更新它们的音量
        if (currentAudioSource != null)
        {
            currentAudioSource.volume = volume;
        }
    }
}
