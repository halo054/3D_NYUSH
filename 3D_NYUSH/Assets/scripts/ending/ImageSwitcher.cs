using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageSwitcher : MonoBehaviour
{
    public Sprite[] images; // 存储要切换的图片数组
    public Sprite specifiedImage; // 指定的图片
    public float switchInterval = 0.5f; // 图片切换间隔
    public string nextSceneName; // 要加载的下一个场景的名称
    public GameObject objectToActivate; // 要激活/停用的 GameObject

    private Image imageComponent;
    private int currentIndex = 0;
    private float timer = 0f;
    private bool shouldSwitch = true;
    private bool activateObject = false;
    private float objectTimer = 0f;
    private bool firstClick = true;

    void Start()
    {
        Cursor.visible = true;
        // 将时间流速设置为1
        Time.timeScale = 1f;
        imageComponent = GetComponent<Image>();
        if (images.Length > 0)
        {
            imageComponent.sprite = images[0]; // 初始显示第一张图片
        }
        objectToActivate.SetActive(false);
    }

    void Update()
    {
        if (shouldSwitch)
        {
            timer += Time.deltaTime;
            if (timer >= switchInterval)
            {
                timer = 0f;
                SwitchImage();
            }
        }

        // 检查鼠标点击事件
        if (Input.GetMouseButtonDown(0))
        {
            if (firstClick && currentIndex >= images.Length)
            {
                firstClick = false;
                if (specifiedImage != null)
                {
                    imageComponent.sprite = specifiedImage; // 切换到指定的图片
                    return;
                }
            }

            if (!shouldSwitch && !string.IsNullOrEmpty(nextSceneName))
            {
                LoadNextScene();
            }
        }

        // 检查是否需要激活另一个 GameObject
        if (activateObject)
        {
            objectTimer += Time.deltaTime;
            if (objectTimer >= 1f)
            {
                objectTimer = 0f;
                ActivateObject();
            }
        }
    }

    void SwitchImage()
    {
        currentIndex++; // 切换到下一张图片
        if (currentIndex < images.Length)
        {
            imageComponent.sprite = images[currentIndex];
        }
        else
        {
            shouldSwitch = false; // 到达最后一张图片，停止切换
            activateObject = true; // 开始激活另一个 GameObject
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName); // 加载下一个场景
    }

    void ActivateObject()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(!objectToActivate.activeSelf); // 切换 GameObject 的激活状态
        }
    }
}
