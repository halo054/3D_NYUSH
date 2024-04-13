using UnityEngine;

public class RandomSetActive : MonoBehaviour
{
    float nextSwitchTime; // 下一次切换的时间
    float minSwitchInterval = 0.5f; // 最小切换间隔
    float maxSwitchInterval = 2f; // 最大切换间隔

    void Start()
    {
        // 初始化下一次切换的时间
        nextSwitchTime = Time.time + Random.Range(minSwitchInterval, maxSwitchInterval);
    }

    void Update()
    {
        // 如果当前时间超过了下一次切换的时间
        if (Time.time >= nextSwitchTime)
        {
            // 遍历所有子对象
            foreach (Transform child in transform)
            {
                // 生成一个0到3之间的随机数
                int randomNumber = Random.Range(0, 4);

                // 如果随机数为0，则激活子对象，否则禁用
                child.gameObject.SetActive(randomNumber == 0);
            }

            // 生成下一次切换的时间
            nextSwitchTime = Time.time + Random.Range(minSwitchInterval, maxSwitchInterval);
        }
    }
}