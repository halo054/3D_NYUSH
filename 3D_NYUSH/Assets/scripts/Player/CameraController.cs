using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 5.0f; // 鼠标灵敏度
    public float minYRotation = -30.0f; // 最小Y轴旋转角度
    public float maxYRotation = 30.0f; // 最大Y轴旋转角度
    
    private float mouseX; // 鼠标X轴移动的偏移量
    private float mouseY; // 鼠标Y轴移动的偏移量

    public GameObject smartPhone;
    private bool inputEnabled = true; // 控制摄像机是否接收输入

    public void EnableInput()
    {
        inputEnabled = true;
    }

    public void DisableInput()
    {
        inputEnabled = false;
    }

    void Start()
    {
        // 初始化鼠标输入的偏移量
        mouseX = transform.eulerAngles.y;
        mouseY = transform.eulerAngles.x;
    }

    void Update()
    {
        // 如果输入被禁用，则不处理鼠标输入
        if (!inputEnabled) return;
        if (!smartPhone.activeSelf)
        {
            // 获取鼠标输入
            mouseX += Input.GetAxis("Mouse X") * sensitivity;
            mouseY -= Input.GetAxis("Mouse Y") * sensitivity;

            // 限制Y轴旋转角度在minYRotation到maxYRotation之间，避免摄像机翻转
            mouseY = Mathf.Clamp(mouseY, minYRotation, maxYRotation);

            // 应用旋转到摄像机
            transform.eulerAngles = new Vector3(mouseY, mouseX, 0);
        }
    }
}