using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBarCircle : MonoBehaviour
{
    [Header("Bar Setting")]
    public Color BarColor;
    public Color BarBackGroundColor;
    public Color MaskColor;
    public Sprite BarBackGroundSprite;

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
}