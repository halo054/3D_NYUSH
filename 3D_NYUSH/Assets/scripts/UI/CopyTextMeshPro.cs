using UnityEngine;
using TMPro;

public class CopyTextMeshPro : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sourceText; // 指定的TextMeshProUGUI组件，内容将被复制
    public TextMeshProUGUI targetText; // 用于显示复制内容的TextMeshProUGUI组件

    void Start()
    {
        // 初始复制内容
        CopyText();
    }

    void Update()
    {
        // 每帧检查源文本是否有变化
        if (targetText.text != sourceText.text)
        {
            // 如果源文本有更新，复制内容到目标文本
            CopyText();
        }
    }

    void CopyText()
    {
        // 将源TextMeshProUGUI的内容复制到目标TextMeshProUGUI
        targetText.text = sourceText.text;
    }
}