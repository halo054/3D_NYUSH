using UnityEngine;

public class InsectSwarm : MonoBehaviour
{
    public GameObject insectPrefab; // 虫子的预制体
    public int numberOfInsects = 200; // 虫子的数量
    public float swarmRadius = 3f; // 虫子飞舞的半径
    public float flightSpeed = 2f; // 虫子的飞行速度
    public float noiseScale = 1f; // 噪声的缩放
    public Transform plantTransform; // 植物的Transform

    void Start()
    {
        // 创建虫子并随机飞舞
        for (int i = 0; i < numberOfInsects; i++)
        {
            // 在植物周围随机生成位置
            Vector3 randomPos = Random.insideUnitSphere * swarmRadius;

            // 实例化虫子，并设置位置和旋转
            GameObject insect = Instantiate(insectPrefab, plantTransform.position + randomPos, Quaternion.identity);
        }
    }

    
}