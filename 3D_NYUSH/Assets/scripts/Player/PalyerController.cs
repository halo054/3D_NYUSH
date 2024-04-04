using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // 移动速度
    public AudioClip[] footstepSounds; // 脚步声音效数组

    private CharacterController characterController;
    private Transform cameraTransform; // 摄像机的Transform组件
    private AudioSource audioSource;
    private int currentFootstepIndex = 0; // 当前脚步声音效的索引
    public GameObject smartPhone;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        // 获取摄像机的Transform组件
        cameraTransform = Camera.main.transform;
        // 获取音频源组件
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!smartPhone.activeSelf)
        {
            // 获取输入
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // 判断玩家是否在移动
            bool isMoving = (horizontalInput != 0 || verticalInput != 0);

            // 播放脚步声音效
            if (isMoving && !audioSource.isPlaying)
            {
                PlayFootstepSound();
            }

            // 获取摄像机的正前方向，并将其投影到XZ平面
            Vector3 cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;

            // 计算移动向量
            Vector3 movement = (horizontalInput * cameraTransform.right + verticalInput * cameraForward) * moveSpeed *
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
