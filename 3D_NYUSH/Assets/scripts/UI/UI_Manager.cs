using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    // 引用smart phone对象，你需要在Inspector面板中指定它
    public GameObject smartPhone;
    public GameObject eMail;
    public Image imageToHide; // 将要隐藏的Image组件
    public Button buttonToHide; // 将要隐藏的Button组件
    public GameObject new_email;
    private bool anynew_email = true;
    // 定义一个变量来控制游戏是否暂停
    private bool isPaused = false;

    // 定义一个UI面板，用于显示和隐藏暂停菜单
    public GameObject pausePanel;
    public CameraController cameraController; // 摄像机控制器引用
    // 灰色透明图片UI元素的引用
    public Image grayOverlayImage;
    private void Start()
    {
        smartPhone.SetActive(false);
        eMail.SetActive(true);
        new_email.SetActive(true);
        // 在Start时隐藏Image和Button
        if (imageToHide != null)
        {
            imageToHide.gameObject.SetActive(false);
        }
        if (buttonToHide != null)
        {
            buttonToHide.gameObject.SetActive(false);
        }
        // 订阅 BoldTextDetected 事件，当发现粗体字体时调用 OnBoldTextDetected 方法
        sendsignal.OnBoldTextDetected += OnBoldTextDetected;
        // 在开始时找到场景中的CameraController实例
        cameraController = FindObjectOfType<CameraController>();
        if (cameraController == null)
        {
            Debug.LogError("CameraController not found in the scene. Please add it to a game object.");
        }
    }

    void Update()
    {
        // 检查是否按下了Esc键
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 切换游戏暂停状态
            isPaused = !isPaused;
            // 切换暂停面板的显示和隐藏
            pausePanel.SetActive(isPaused);
            ToggleGrayOverlay(isPaused); // 切换灰色透明图片UI元素的显示状态
            // 如果游戏已经暂停，再次按下Esc键恢复游戏
            if (isPaused)
            {
                Time.timeScale = 0f; // 暂停游戏
                // 暂停游戏时，通知摄像机控制器停止处理输入
                cameraController.DisableInput();
                
            }
            else
            {
                // 恢复游戏时，通知摄像机控制器恢复处理输入
                cameraController.EnableInput();
                Time.timeScale = 1f; // 恢复游戏
            }
            
        }
        // 检查是否按下了Tab键
        if (Input.GetKeyDown(KeyCode.Tab) && !isPaused)
        {
            // 切换smart phone的可见性
            if (smartPhone.activeSelf)
            {
                smartPhone.SetActive(!smartPhone.activeSelf);
                buttonToHide.gameObject.SetActive(false);
                imageToHide.gameObject.SetActive(false);
            }
            else
            {
                smartPhone.SetActive(!smartPhone.activeSelf);
            }
            
            eMail.SetActive(!eMail.activeSelf);
            if (new_email.activeSelf == false)
            {
                if (anynew_email == true)
                {
                    new_email.SetActive(true);
                }
            }
            else if (new_email.activeSelf)
            {
                new_email.SetActive(false);
            }
        }
    }

    public void Continue()
    {
        // 切换游戏暂停状态
        isPaused = !isPaused;
        // 切换暂停面板的显示和隐藏
        pausePanel.SetActive(isPaused);
        ToggleGrayOverlay(isPaused); // 切换灰色透明图片UI元素的显示状态
        // 如果游戏已经暂停，再次按下Esc键恢复游戏
        if (isPaused)
        {
            Time.timeScale = 0f; // 暂停游戏
            // 暂停游戏时，通知摄像机控制器停止处理输入
            cameraController.DisableInput();
                
        }
        else
        {
            // 恢复游戏时，通知摄像机控制器恢复处理输入
            cameraController.EnableInput();
            Time.timeScale = 1f; // 恢复游戏
        }

    }
    // 切换到目标场景
    public void SwitchScene(string sceneName)
    {
        // 加载目标场景
        SceneManager.LoadScene(sceneName);
    }

    public void ToggleGrayOverlay(bool state)
    {
        grayOverlayImage.gameObject.SetActive(state);
    }
    public void Hide_smartphone()
    {
        smartPhone.SetActive(false);
        buttonToHide.gameObject.SetActive(false);
        imageToHide.gameObject.SetActive(false);
        eMail.SetActive(!eMail.activeSelf);
        if (new_email.activeSelf == false)
        {
            if (anynew_email == true)
            {
                new_email.SetActive(true);
            }
        }
        else if (new_email.activeSelf == true)
        {
            new_email.SetActive(false);
        }
    }

    public void ShowButton()
    {
        buttonToHide.gameObject.SetActive(true);
    }

    public void Hide_Button_Image()
    {
        buttonToHide.gameObject.SetActive(false);
        imageToHide.gameObject.SetActive(false);
    }
    // 当检测到粗体字体时执行的方法
    void OnBoldTextDetected()
    {
        anynew_email = false;
        // 取消订阅 BoldTextDetected 事件，以防止内存泄漏
        sendsignal.OnBoldTextDetected -= OnBoldTextDetected;
    }
}