using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomController : MonoBehaviour
{
    public GameObject progresscircle;

    public GameObject classroom_text;
    public Camera mainCamera; // 指向您的主摄像机
    private bool islooking = false;
    public bool beforeclass = false;
    private float Barvalue = 0f;
    private bool hasbarfill = false;
    public GameObject close_hint;
    private bool hasnotlooked = false;
    
    private bool musicPlayed = false; // 用于跟踪音乐是否已经播放过

    public AudioSource audioSource; // 声明音频源
    public AudioClip specifiedMusic; // 指定的音乐
    
    // Start is called before the first frame update
    void Start()
    {
        progresscircle.SetActive(false);
        classroom_text.SetActive(false);
        if (close_hint != null)
        {
            close_hint.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (beforeclass)
        {
            CheckLookingAtObject(); // 检测是否正在看着物体
        }

        if (islooking && progresscircle != null)
        {
            ProgressBarCircle progressBar = progresscircle.GetComponent<ProgressBarCircle>();
            Barvalue = progressBar.GetBarValue();
            
            if (Barvalue >= 99.1f)
            {
                hasbarfill = true;
            }
            if (Barvalue < 1f && hasnotlooked)
            {
                hasnotlooked = false;
                hasbarfill = false;
            }
        }

        if (hasbarfill)
        {
            HideGUI(); // 如果玩家没有看着物体，隐藏GUI提示
            if (close_hint != null)
            {
                if (!musicPlayed) // 检查音乐是否已经播放过
                {
                    audioSource.PlayOneShot(specifiedMusic); // 播放指定的音乐
                    musicPlayed = true; // 将音乐播放标志设置为true
                }
                close_hint.SetActive(true);
            }
        }
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
            hasnotlooked = true;
            if (close_hint != null)
            {
                close_hint.SetActive(false);
            }
            hasbarfill = false;
            islooking = false;
            HideGUI(); // 如果玩家没有看着物体，隐藏GUI提示
        }
    }

    private void ShowGUI()
    {

            classroom_text.SetActive(true);
            progresscircle.SetActive(true);
        
    }

    private void HideGUI()
    {
        classroom_text.SetActive(false);
        progresscircle.SetActive(false);
    }
    public bool GetIslooking()
    {
        return hasnotlooked;
    }
}
