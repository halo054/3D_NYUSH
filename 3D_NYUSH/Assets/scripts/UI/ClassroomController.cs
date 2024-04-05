using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomController : MonoBehaviour
{
    public GameObject progresscircle;

    public GameObject classroom_text;
    public Camera mainCamera; // 指向您的主摄像机
    private bool islooking = false;
    // Start is called before the first frame update
    void Start()
    {
        progresscircle.SetActive(false);
        classroom_text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckLookingAtObject(); // 检测是否正在看着物体
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
        if (islooking == true)
        {
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
}
