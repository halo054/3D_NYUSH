using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraDepthOfField : MonoBehaviour
{
    public Transform target; // 指定的物体
    public Shader depthOfFieldShader; // 深度模糊的Shader

    private Material depthOfFieldMaterial;
    private Camera myCamera;

    private void Start()
    {
        myCamera = GetComponent<Camera>();
        depthOfFieldMaterial = new Material(depthOfFieldShader);
    }

    private void Update()
    {
        // 计算摄像机和目标物体之间的距离
        float distance = Vector3.Distance(transform.position, target.position);
        
        // 将距离转换为模糊程度（0到1之间），并反转
        float blurAmount = Mathf.Clamp01(distance / 20.0f); // 10.0f是一个调整模糊程度的参数
        // 将模糊程度传递给Shader，并反转
        depthOfFieldMaterial.SetFloat("_BlurAmount", 1.0f - blurAmount);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (depthOfFieldMaterial != null)
        {
            // 将图像传递给Shader处理
            Graphics.Blit(source, destination, depthOfFieldMaterial);
        }
        else
        {
            // 如果Shader不存在，则直接传递图像
            Graphics.Blit(source, destination);
        }
    }
}