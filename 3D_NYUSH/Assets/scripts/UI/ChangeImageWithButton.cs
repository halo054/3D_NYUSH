using UnityEngine;
using UnityEngine.UI;

// 附加此脚本到每个按钮上
public class ButtonSpriteChanger : MonoBehaviour
{
    public Image targetImage; // 将要更换Sprite的Image组件
    public Sprite spriteToChange; // 按钮指定的Sprite

    public void ChangeSprite()
    {
        targetImage.gameObject.SetActive(true);
        // 确保目标Image组件不为空
        if (targetImage != null)
        {
            // 更换Image的Sprite为按钮指定的Sprite
            targetImage.sprite = spriteToChange;
        }
    }

    // 在Unity编辑器中，你可以将这个函数拖拽到按钮的OnClick()事件中
    public void OnButtonClick()
    {
        ChangeSprite();
    }
}