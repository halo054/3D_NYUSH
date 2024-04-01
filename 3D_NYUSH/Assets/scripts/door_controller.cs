using System.Collections;
using System.Collections.Generic;
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

    void Update()
    {
        if (hasRotated == true)
        {
            rotationAmount = 90f;
        }
        else
        {
            rotationAmount = -90f;
        }
        // 检测是否按下了E键且没有正在旋转
        if (Input.GetKeyDown(KeyCode.E) && !isRotating)
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
}
