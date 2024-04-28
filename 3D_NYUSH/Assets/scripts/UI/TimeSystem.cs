using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class GameTimeManager : MonoBehaviour
{
    [SerializeField] private int initialHour = 16; // 游戏开始时的小时
    [SerializeField] private int initialMinute = 20; // 游戏开始时的分钟
    [SerializeField] private int initialSeconds = 0; // 游戏开始时的秒数
    public int timeScale = 5; // 每秒游戏时间增加的秒数

    private float timeSinceStart; // 游戏开始以来的时间（秒）
    private float lastSecond; // 上一秒钟的时间

    public TextMeshProUGUI textMesh; // 用于显示时间的TextMeshProUGUI组件
    public AudioClip targetMusic; // 指定的音乐
    public GameObject targetObject; // 要切换激活状态的物体
    private AudioSource audioSource; // 用于播放音乐的AudioSource组件
    private bool isObjectActive = false; // 物体是否激活
    public bool isbeforeclass = true;
    void Start()
    {
        if (!isbeforeclass)
        {
            initialHour = 17;
            initialMinute = 15;
        }
        // 初始化游戏时间
        timeSinceStart = initialHour * 3600 + initialMinute * 60 + initialSeconds; // 将小时、分钟和秒转换为秒
        // 记录当前时间作为上一秒钟的时间
        lastSecond = Time.time;
        // 获取AudioSource组件
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
// 确保targetObject不为空
        if (targetObject != null)
        {
            targetObject.SetActive(isObjectActive);
        }

        
    }

    void Update()
    {
       
        // 计算自上一秒钟以来经过的时间
        float timeSinceLastSecond = Time.time - lastSecond;
        // 如果经过的时间超过1秒，更新游戏时间和上一秒钟的时间
        if (timeSinceLastSecond >= 1.0f)
        {
            timeSinceStart += timeScale; // 增加游戏时间
            lastSecond += 1.0f; // 更新上一秒钟的时间
        }

        // 计算当前的游戏时间（小时、分钟和秒）
        int hours = (int)(timeSinceStart / 3600);
        int minutes = (int)((timeSinceStart % 3600) / 60);
        int seconds = (int)(timeSinceStart % 60);

        // 如果找到了TextMeshProUGUI组件，则更新UI显示
        if (textMesh != null)
        {
            textMesh.text = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
        }
        if (hours == 16 && minutes == 25 && seconds == 0 && isbeforeclass)
        {
            // 播放音乐并切换物体激活状态
            PlayMusicAndSwitchObjectActiveState();
        }
        // Check for mouse click
        if (Input.GetMouseButtonDown(0) && (seconds >= 25 || minutes >= 26)) // 0 represents the left mouse button
        {
            HandleMouseClick();
        }
    }
    private void PlayMusicAndSwitchObjectActiveState()
    {

            audioSource.clip = targetMusic;
            audioSource.Play();
        
        // 切换物体的激活状态
        if (targetObject != null)
        {
            isObjectActive = true;
            targetObject.SetActive(true);
        }
    }

    private void HandleMouseClick()
    {
            
        // Add your logic here, such as stopping music and switching object's active state

        StopMusicAndSwitchObjectActiveState();
    }


    private void StopMusicAndSwitchObjectActiveState()
    {
        // 停止音乐
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        // 切换物体的激活状态
        if (targetObject != null)
        {
            isObjectActive = false;
            targetObject.SetActive(false);
        }
    }
}