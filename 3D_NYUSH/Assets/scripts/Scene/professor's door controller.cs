using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; // 引入UI命名空间以便使用TextMeshPro
using TMPro;
using UnityEngine;
using Object = System.Object;

public class professor_door_controller : MonoBehaviour
{
    private float rotationAmount = -90f; // 旋转角度
    public float rotationDuration = 1f; // 旋转持续时间
    private bool isRotating = false; // 是否正在旋转
    private Quaternion initialRotation; // 初始旋转
    private Quaternion targetRotation; // 目标旋转
    private float rotationTimer = 0f; // 旋转计时器
    private bool hasRotated = false;
    public bool is_locked = true;
    public TextMeshProUGUI key_hint;
    public Camera mainCamera; // 指向您的主摄像机
    private bool islooking = false;
    public AudioClip openSound;
    public AudioClip closeSound;
    public AudioClip keypickupsound;
    public AudioClip lockSound;
    private AudioSource audioSource;
    private bool isKeyPickedUpValue;
    public GameObject keyObject; // 这里放入您想要指定的 GameObject
    private bool haskeypicksound = false;
    public bool isprofessorin = false;
    public bool isweird = false;
    private bool wait = false;
    private bool hasplay = false;
    public bool isday5 = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        HideGUI();
    }
    IEnumerator PlayAudioCoroutine(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
        yield return new WaitForSeconds(audioClip.length);
        haskeypicksound = true; // 设置为true，确保只播放一次
    }

    void Update()
    {           
        if (keyObject == null)
        {
            keyObject = GameObject.FindGameObjectWithTag("RealKey");
            // 获取关联的物体上的KeyInteraction脚本
            KeyInteraction keyInteractionScript = keyObject.GetComponent<KeyInteraction>();

            // 检查是否成功获取了KeyInteraction脚本
            if (keyInteractionScript != null)
            {
                // 获取isKeyPickedUp的值
                isKeyPickedUpValue = keyInteractionScript.IsKeyPickedUp();
            }

            if (isKeyPickedUpValue)
            {
                is_locked = false;
                haskeypicksound = true;
            }
        }

        if (!haskeypicksound && isKeyPickedUpValue)
        {
            is_locked = false;
            if (!audioSource.isPlaying)
            {
                audioSource.clip = keypickupsound;
                StartCoroutine(PlayAudioCoroutine(audioSource.clip));
            }
        }


        // 检查是否提供了有效的 GameObject 引用
        if (keyObject != null)
        {
            // 获取关联的物体上的KeyInteraction脚本
            KeyInteraction keyInteractionScript = keyObject.GetComponent<KeyInteraction>();

            // 检查是否成功获取了KeyInteraction脚本
            if (keyInteractionScript != null)
            {
                // 获取isKeyPickedUp的值
                isKeyPickedUpValue = keyInteractionScript.IsKeyPickedUp();
            }
        }
        CheckLookingAtObject(); // 检测是否正在看着物体
        if (!isprofessorin)
        {
            if (islooking && !isRotating)
            {
                if (hasRotated)
                {
                    key_hint.text = "Press E to close";
                }
                else
                {
                    key_hint.text = "Press E to open";
                }
            }

            if (!is_locked)
            {
                if (hasRotated)
                {
                    if (audioSource.clip != closeSound)
                    {
                        audioSource.clip = closeSound;
                    }

                    rotationAmount = 90f;
                }
                else
                {
                    if (audioSource.clip != openSound)
                    {
                        audioSource.clip = openSound;
                    }

                    rotationAmount = -90f;
                }
            }

            if (!isKeyPickedUpValue)
            {
                if (Input.GetKeyDown(KeyCode.E) && islooking)
                {
                    wait = true;
                    hasplay = false;
                }
                if (!islooking)
                {
                    wait = false;
                }
            }
            
            if (wait)
            {
                if (audioSource.clip != lockSound)
                {
                    audioSource.clip = lockSound;
                }

                key_hint.text = "Locked";
                
                if (audioSource.isPlaying == false && !hasplay)
                {
                    audioSource.Play();
                    hasplay = true;
                }

                if (isday5 && key_hint.text == "Locked")
                {
                    key_hint.text = "";
                }
            }

            // 检测是否按下了E键且没有正在旋转
            if (Input.GetKeyDown(KeyCode.E) && !isRotating && !is_locked && islooking)
            {

                // 计算目标旋转角度
                Vector3 targetEulerAngles = transform.eulerAngles + new Vector3(0f, rotationAmount, 0f);
                targetRotation = Quaternion.Euler(targetEulerAngles);

                initialRotation = transform.rotation;
                rotationTimer = 0f;
                isRotating = true;
                if (hasRotated)
                {
                    hasRotated = false;
                }
                else
                {
                    hasRotated = true;
                }

            }

            // 如果正在旋转，执行旋转动画
            if (isRotating)
            {
                if (audioSource.isPlaying == false)
                {
                    audioSource.Play();
                }

                rotationTimer += Time.deltaTime;
                float t = Mathf.Clamp01(rotationTimer / rotationDuration);

                // 插值旋转
                transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, SmoothStep(t));

                // 如果旋转完成，将isRotating设置为false
                if (t >= 1f)
                {
                    isRotating = false;
                }
            }
        }

        else
        {
            if (islooking)
            {
                if (isweird)
                {
                    key_hint.text = "The music sounds weird...";
                }
                else
                {
                    key_hint.text = "Professor is inside. I should not disturb him...";
                }
            }
        }
    }

    // 自定义的平滑插值方法
        float SmoothStep(float t)
        {
            return t * t * (3f - 2f * t);
        }

        private void CheckLookingAtObject()
        {
            float maxDistance = 2.5f; // 设置射线的最大长度
            Camera mainCamera = Camera.main; // 获取主摄像机

            // 创建从摄像机位置发射的射线
            Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
            RaycastHit hit;


            // 如果射线与物体相交
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                // 检查相交的物体是否是当前物体
                if (hit.collider.gameObject == gameObject)
                {
                    islooking = true;
                    ShowGUI(); // 如果玩家正在看着物体，显示GUI提示
                    return;
                }
            }

            // 如果没有射线与物体相交
            if (islooking)
            {
                islooking = false;
                HideGUI(); // 如果玩家没有看着物体，隐藏GUI提示
            }
        }

        private void ShowGUI()
        {
            // 显示TextMeshPro GUI
            if (key_hint != null)
            {
                key_hint.gameObject.SetActive(true);
            }
        }

        private void HideGUI()
        {
            // 隐藏TextMeshPro GUI
            if (key_hint != null)
            {
                key_hint.gameObject.SetActive(false);
            }
        }
    
}
