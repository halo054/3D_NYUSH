using UnityEngine;

public class InsectFlight : MonoBehaviour
{
    private Transform plantTransform; // 植物的Transform
    private float flightSpeed = 2f; // 飞行速度
    public float maxRadius = 3f; // 最大绕行半径
    private float minRadius = 0.1f; // 最小绕行半径
    private Vector3 targetPosition; // 目标位置

    public GameObject plant;

    // 设置植物的Transform
    public void SetPlantTransform(Transform plant)
    {
        plantTransform = plant;
    }

    void Start()
    {
        SetPlantTransform(plant.transform);

        // 设置初始位置在植物周围的球形范围内
        float randomRadius = Random.Range(minRadius, maxRadius);
        Vector2 randomCircle = Random.insideUnitCircle.normalized * randomRadius;
        Vector3 initialPosition = plantTransform.position + new Vector3(randomCircle.x, 0f, randomCircle.y);

        // 设置初始位置
        transform.position = initialPosition;

        // 计算初始目标位置
        SetRandomTarget();
    }

    void Update()
    {
        if (plantTransform == null)
        {
            Debug.LogError("Plant transform not set!");
            return;
        }

        // 到达目标位置后重新设置目标位置
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTarget();
        }

        // 移动虫子向目标位置
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, flightSpeed * Time.deltaTime);
    }

    // 设置随机目标位置
    // 设置随机目标位置
    // 设置随机目标位置和朝向
    void SetRandomTarget()
    {
        float randomRadiusXY = Random.Range(minRadius, maxRadius);
        float randomRadiusZ = Random.Range(-maxRadius, maxRadius); // 修改为在-z到+z范围内生成随机Z坐标
        float randomAngle = Random.Range(0f, Mathf.PI * 2f); // 随机角度

        // 计算随机的三维坐标
        float x = plantTransform.position.x + randomRadiusXY * Mathf.Cos(randomAngle);
        float z = plantTransform.position.z + randomRadiusXY * Mathf.Sin(randomAngle);
        float y = plantTransform.position.y + randomRadiusZ; // 使用随机生成的z坐标作为y坐标

        targetPosition = new Vector3(x, y, z);

        // 计算虫子应该面向的方向
        Vector3 directionToTarget = (targetPosition - transform.position).normalized;

        // 计算面向目标的旋转角度（只在 Y 轴上旋转）
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);

        // 应用旋转
        transform.rotation = targetRotation;
    }


}