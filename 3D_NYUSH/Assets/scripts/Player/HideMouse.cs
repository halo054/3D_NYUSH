using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMouse : MonoBehaviour
{
    public GameObject pausepanel;
    public GameObject smartPhone;
    public GameObject alarmclock;
    public GameObject musicroomscreen;
    void Start()
    {
        // 隐藏鼠标
        Cursor.visible = false;
    }

    void Update()
    {
        if (smartPhone.activeSelf || pausepanel.activeSelf || alarmclock.activeSelf)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }

        if (musicroomscreen != null)
        {
            if (musicroomscreen.activeSelf)
            {
                Cursor.visible = true;
            }
        }
    }
}