using UnityEngine;
using TMPro;

public class ModifyTextStyle : MonoBehaviour
{
    // 公共方法，用于修改所有子物体中的TextMeshProUGUI的字体样式
    public void SetTextStyleToRegular()
    {
        // 遍历当前GameObject的所有子物体
        foreach (Transform child in transform)
        {
            // 获取子物体上的TextMeshProUGUI组件
            TextMeshProUGUI textMeshPro = child.GetComponent<TextMeshProUGUI>();
            if (textMeshPro != null)
            {
                // 设置TextMeshProUGUI组件的字体样式为非粗体
                textMeshPro.fontStyle = FontStyles.Normal;
            }
        }
    }
}