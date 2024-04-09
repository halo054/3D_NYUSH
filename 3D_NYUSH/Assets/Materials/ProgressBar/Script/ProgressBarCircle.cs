using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBarCircle : MonoBehaviour
{
    [Header("Bar Setting")]
    public Color BarColor;
    public Color BarBackGroundColor;
    public Color MaskColor;
    public Sprite BarBackGroundSprite;
    public string targetSceneName; // 新添加的字段，用于存储目标场景的名称

    private Image bar, barBackground, Mask;
    private float barValue;
    private bool isIncreasing;

    public float BarValue
    {
        get { return barValue; }

        set
        {
            value = Mathf.Clamp(value, 0, 100);
            barValue = value;
            UpdateValue(barValue);

            // 当进度条充满时触发场景切换
            if (barValue >= 100f)
            {
                SwitchToTargetScene();
            }
        }
    }

    private void Awake()
    {
        barBackground = transform.Find("BarBackgroundCircle").GetComponent<Image>();
        bar = transform.Find("BarCircle").GetComponent<Image>();
        Mask = transform.Find("Mask").GetComponent<Image>();
    }

    private void Start()
    {
        bar.color = BarColor;
        Mask.color = MaskColor;
        barBackground.color = BarBackGroundColor;
        barBackground.sprite = BarBackGroundSprite;

        UpdateValue(barValue);
    }

    void UpdateValue(float val)
    {
        bar.fillAmount = -(val / 100) + 1f;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            isIncreasing = true;
        }
        else
        {
            isIncreasing = false;
            // Reset the progress if "E" key is released
            BarValue = 0f;
        }

        if (isIncreasing)
        {
            BarValue += Time.deltaTime * 100f; // Adjust this multiplier for desired speed
        }
    }

    // 新添加的方法，用于切换到目标场景
    void SwitchToTargetScene()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogWarning("Target scene name is not specified!");
        }
    }
}
