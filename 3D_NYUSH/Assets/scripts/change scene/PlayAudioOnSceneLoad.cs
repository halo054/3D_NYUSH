using UnityEngine;

public class PlayAudioOnSceneLoad : MonoBehaviour
{
    public AudioClip audioClip; // 指定要播放的音频
    public float volume = 1f; // 指定音频的音量

    private bool hasPlayed = false;

    void Start()
    {
        if (!hasPlayed && audioClip != null)
        {
            // 创建一个 AudioSource 对象来播放音频
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = audioClip;

            // 设置音量
            audioSource.volume = volume;

            // 播放音频
            audioSource.Play();

            hasPlayed = true;
        }
    }
}