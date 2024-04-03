using TMPro;
using UnityEngine;

public class sendsignal : MonoBehaviour
{
    // 全局事件委托，用于向外部发送信号
    public delegate void BoldTextDetected();
    public static event BoldTextDetected OnBoldTextDetected;

    bool hasSentSignal = false;

    void OnEnable()
    {
        // 每当物体激活时，检查是否有粗体字体并发送信号
        hasSentSignal = false;
    }

    void Update()
    {
        // 在 Update 方法中检查是否需要发送信号
        if (!hasSentSignal && transform.childCount > 0 && !CheckForBoldTextRecursive(gameObject))
        {
            // 如果没有发现粗体字体，向全局发送信号
            if (OnBoldTextDetected != null)
            {
                OnBoldTextDetected();
            }
            hasSentSignal = true; // 标记已发送信号
        }
    }

    // 公共方法，用于检查指定GameObject的所有子物体及其子物体中的TextMeshProUGUI组件
    // 如果发现粗体字体，返回true；否则返回false
    bool CheckForBoldTextRecursive(GameObject parentObject)
    {
        bool hasBoldText = false;

        // 遍历指定GameObject的所有子物体
        foreach (Transform child in parentObject.transform)
        {
            // 获取子物体上的TextMeshProUGUI组件
            TextMeshProUGUI textMeshProUGUI = child.GetComponent<TextMeshProUGUI>();
            if (textMeshProUGUI != null)
            {
                // 获取TextMeshProUGUI组件的字体样式
                FontStyles fontStyles = textMeshProUGUI.fontStyle;

                // 检查字体样式中是否包含粗体
                if ((fontStyles & FontStyles.Bold) != 0)
                {
                    // 如果发现粗体字体，设置hasBoldText为true
                    hasBoldText = true;
                    break; // 找到粗体字体就停止循环，不再继续检查
                }
            }

            // 递归检查子物体
            if (CheckForBoldTextRecursive(child.gameObject))
            {
                // 如果递归调用发现粗体字体，设置hasBoldText为true
                hasBoldText = true;
                break; // 找到粗体字体就停止循环，不再继续检查
            }
        }

        // 返回是否发现粗体字体
        return hasBoldText;
    }
}