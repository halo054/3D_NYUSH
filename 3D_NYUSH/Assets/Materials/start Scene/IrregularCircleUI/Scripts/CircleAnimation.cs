using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CircleAnimation : MonoBehaviour
{
    public List<GameObject> animObjects;
    private bool shouldAnimate = true;
    private float rotationSpeed = 5f; // 旋转速度

    void Awake()
    {
        // 初始化animObjects列表，并获取所有需要旋转的游戏对象
        animObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("RotateTag")); // 替换"RotateTag"为实际使用的标签
        // 注册为场景加载的观察者
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // 注销观察者
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        if (shouldAnimate && animObjects != null)
        {
            foreach (GameObject go in animObjects)
            {
                Vector3 angle = go.transform.eulerAngles;
                angle.z += rotationSpeed * Time.deltaTime; // 使用固定的旋转速度
                go.transform.eulerAngles = angle;
            }
        }
    }

    // 当场景加载完成后调用此方法
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 重新获取旋转对象的引用
        RefreshReferences();
    }

    // 刷新旋转对象的引用
    private void RefreshReferences()
    {
        animObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("RotateTag")); // 替换"RotateTag"为实际使用的标签
    }

    // 其他方法保持不变...
}