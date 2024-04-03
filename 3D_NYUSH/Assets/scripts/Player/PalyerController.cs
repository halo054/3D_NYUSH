using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // 移动速度

    private CharacterController characterController;
    private Transform cameraTransform; // 摄像机的Transform组件
    public GameObject smartPhone;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        // 获取摄像机的Transform组件
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (!smartPhone.activeSelf)
        {
            // 获取输入
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

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
}
