using UnityEngine;
using UnityEngine.UI;

public class ScrollOnWheel : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float scrollSensitivity = 5f; // 滚动灵敏度，可以根据需要调整

    void Update()
    {
        // 获取鼠标滚轮滚动值并转换为float类型
        float scrollValue = (float)Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;

        // 计算新的滚动位置
        float newNormalizedPosition = scrollRect.verticalNormalizedPosition + scrollValue;

        // 确保新的滚动位置不会小于0（顶部边界）
        if (newNormalizedPosition < -1)
        {
            newNormalizedPosition = -1;
        }
        // 确保新的滚动位置不会小于0（顶部边界）
        if (newNormalizedPosition > 2)
        {
            newNormalizedPosition = 2;
        }

        // 更新ScrollRect的垂直滚动位置
        scrollRect.verticalNormalizedPosition = newNormalizedPosition;
    }
}