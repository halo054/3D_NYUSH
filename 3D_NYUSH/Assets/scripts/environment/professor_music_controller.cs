using UnityEngine;

public class MusicController : MonoBehaviour
{
    public GameObject targetObject; // 指定的物体
    public AudioSource musicSource; // AudioSource 组件
    private float maxVolumeDistance = 60f; // 最大音量距离

    private void Update()
    {
        if (targetObject != null && musicSource != null)
        {
            // 计算目标物体和音乐控制物体的距离
            float distance = Vector3.Distance(targetObject.transform.position, transform.position);

            // 根据距离调整音乐音量
            float volume = 0.4f - Mathf.Clamp01(distance / maxVolumeDistance);
            musicSource.volume = volume;
        }
    }
}