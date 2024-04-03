using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    // 在你的游戏中需要退出的地方调用这个方法
    public void QuitGame()
    {
        // 退出游戏
        Application.Quit();
    }
    // 切换到目标场景
    public void SwitchScene(string sceneName)
    {
        // 加载目标场景
        SceneManager.LoadScene(sceneName);
    }
}
