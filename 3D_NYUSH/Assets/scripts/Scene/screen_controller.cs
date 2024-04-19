using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class screen_controller : MonoBehaviour
{
    public TextMeshProUGUI key_hint;
    public TextMeshProUGUI text_to_hide;
    public Camera mainCamera; // 指向您的主摄像机
    private bool islooking = false;
    public GameObject screen;
    public GameObject new_email;
    
    public CameraController cameraController; // 摄像机控制器引用
    private bool hasE = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
        HideGUI();
    }

    // Update is called once per frame
    void Update()
    {
        CheckLookingAtObject(); // 检测是否正在看着物体
        if (!hasE)
        {
            key_hint.text = "Press E to check";
            HideScreen();
            // 恢复游戏时，通知摄像机控制器恢复处理输入
            cameraController.EnableInput();
            Time.timeScale = 1f; // 恢复游戏
        }
        else
        {
            // 暂停游戏时，通知摄像机控制器停止处理输入
            cameraController.DisableInput();
            Time.timeScale = 0f; // 暂停游戏
            key_hint.text = "Press E to return";
            ShowScreen();
        }

        if (islooking)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                hasE = !hasE;
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            hasE = false;
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

        if (hasE)
        {
            ShowGUI();
            return;
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
            key_hint.gameObject.SetActive(false);
           
    }
    private void ShowScreen()
    {
        new_email.SetActive(false);
        
        text_to_hide.gameObject.SetActive(false);
            screen.SetActive(true);
        
    }
    private void HideScreen()
    {
        
        text_to_hide.gameObject.SetActive(true);
        screen.SetActive(false);
        new_email.SetActive(true);
    }
}
