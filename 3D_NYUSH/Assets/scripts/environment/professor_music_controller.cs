using UnityEngine;

public class MusicController : MonoBehaviour
{
    public Transform targetObject; // 指定的物体
    public AudioSource musicSource; // AudioSource 组件
    public float maxVolumeDistance = 150f; // 最大音量距离
    public float maxVolume = 0.2f;

    private void Update()
    {
        if (targetObject != null && musicSource != null)
        {
            // 将目标物体的位置转换到音乐控制物体的局部坐标系中
            Vector3 targetPositionInLocalSpace = transform.InverseTransformPoint(targetObject.position);
            
            // 计算目标物体和音乐控制物体的距离
            float distance = targetPositionInLocalSpace.magnitude;

            // 根据距离调整音乐音量
            float volume = maxVolume - Mathf.Clamp01(distance / maxVolumeDistance);
            musicSource.volume = volume;
        }
    }
}