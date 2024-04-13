using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller_weird : MonoBehaviour
{
    public float moveSpeedNear = 5f; // 接近物体时的移动速度
    public float moveSpeedFar = 1f; // 远离物体时的移动速度
    public GameObject targetObject; // 指定的物体
    public AudioClip[] footstepSounds; // 脚步声音效数组

    private CharacterController characterController;
    private Transform cameraTransform; // 摄像机的Transform组件
    private AudioSource audioSource;
    private int currentFootstepIndex = 0; // 当前脚步声音效的索引
    private float lastDistanceToTarget; // 上一帧玩家与目标物体的距离
    public GameObject smartPhone;
    public float minVolumeDistance = 1f; // 脚步声音效的最小音量距离
    public float maxVolumeDistance = 10f; // 脚步声音效的最大音量距离

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        // 获取摄像机的Transform组件
        cameraTransform = Camera.main.transform;
        // 获取音频源组件
        audioSource = GetComponent<AudioSource>();
        // 初始化上一帧距离为当前距离
        lastDistanceToTarget = Vector3.Distance(transform.position, targetObject.transform.position);
    }

    void Update()
    {
        if (!smartPhone.activeSelf)
        {
            // 获取玩家位置与目标物体位置的距离
            float distanceToTarget = Vector3.Distance(transform.position, targetObject.transform.position);

            // 获取脚步声音效的音量
            float volume = Mathf.Lerp(1f, 0f, Mathf.InverseLerp(minVolumeDistance, maxVolumeDistance, distanceToTarget));

            // 设置音频源的音量
            audioSource.volume = volume;

            float speed = distanceToTarget < lastDistanceToTarget ? moveSpeedNear : moveSpeedFar;
if (distanceToTarget > 15.0f)
                {
                    speed = moveSpeedNear;
                }
else
{
    speed = moveSpeedFar;
}
            


            // 更新上一帧距离
            lastDistanceToTarget = distanceToTarget;

            // 获取输入
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // 判断玩家是否在移动
            bool isMoving = (horizontalInput != 0 || verticalInput != 0);

            // 播放脚步声音效
            if (isMoving && !audioSource.isPlaying && characterController.velocity != Vector3.zero)
            {
                PlayFootstepSound();
            }

            // 获取摄像机的正前方向，并将其投影到XZ平面
            Vector3 cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;

            // 计算移动向量
            Vector3 movement = (horizontalInput * cameraTransform.right + verticalInput * cameraForward) * speed *
                               Time.deltaTime;

            // 应用移动
            characterController.Move(movement);

            // 这里不再需要进行碰撞检测和调整移动方向的操作，CharacterController会自动处理碰撞
        }
    }

    // 播放脚步声音效
    void PlayFootstepSound()
    {
        if (footstepSounds.Length == 0)
            return;

        // 获取当前要播放的脚步声音效
        AudioClip footstepSound = footstepSounds[currentFootstepIndex];

        // 播放脚步声音效
        audioSource.clip = footstepSound;
        audioSource.Play();

        // 更新索引以循环播放脚步声音效
        currentFootstepIndex = (currentFootstepIndex + 1) % footstepSounds.Length;
    }
}
