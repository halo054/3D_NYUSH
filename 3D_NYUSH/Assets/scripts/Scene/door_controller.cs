using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; // 引入UI命名空间以便使用TextMeshPro
using TMPro;
using UnityEngine;

public class door_controller : MonoBehaviour
{
    private float rotationAmount = -90f; // 旋转角度
    public float rotationDuration = 1f; // 旋转持续时间
    private bool isRotating = false; // 是否正在旋转
    private Quaternion initialRotation; // 初始旋转
    private Quaternion targetRotation; // 目标旋转
    private float rotationTimer = 0f; // 旋转计时器
    private bool hasRotated = false;
    public bool islocked = true;
    public TextMeshProUGUI key_hint;
    public Camera mainCamera; // 指向您的主摄像机
    private bool islooking = false;
    

    private void Start()
    {
        HideGUI();
    }

    void Update()
    {
        CheckLookingAtObject(); // 检测是否正在看着物体
        if (islocked == true)
        {
            key_hint.text = "locked";
        }
        else if (hasRotated == true)
        {
            key_hint.text = "press E to close";
        }
        else
        {
            key_hint.text = "press E to open";
        }
        if (hasRotated == true)
        {
            rotationAmount = 90f;
        }
        else
        {
            rotationAmount = -90f;
        }
        // 检测是否按下了E键且没有正在旋转
        if (Input.GetKeyDown(KeyCode.E) && !isRotating && !islocked && islooking)
        {
            // 计算目标旋转角度
            Vector3 targetEulerAngles = transform.eulerAngles + new Vector3(0f, rotationAmount, 0f);
            targetRotation = Quaternion.Euler(targetEulerAngles);

            initialRotation = transform.rotation;
            rotationTimer = 0f;
            isRotating = true;
            if (hasRotated == true)
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

    // 自定义的平滑插值方法
    float SmoothStep(float t)
    {
        return t * t * (3f - 2f * t);
    }
    private void CheckLookingAtObject()
    {
        float maxDistance = 10f; // 设置射线的最大长度
        Camera mainCamera = Camera.main; // 获取主摄像机

        // 创建从摄像机位置发射的射线
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        // 可视化射线
        Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * maxDistance, Color.green);

        // 如果射线与物体相交
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            // 检查相交的物体是否是当前物体
            if (hit.collider.gameObject == gameObject)
            {
                Debug.Log("111");
                islooking = true;
                ShowGUI(); // 如果玩家正在看着物体，显示GUI提示
                return;
            }
        }

        // 如果没有射线与物体相交
        islooking = false;
        HideGUI(); // 如果玩家没有看着物体，隐藏GUI提示
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