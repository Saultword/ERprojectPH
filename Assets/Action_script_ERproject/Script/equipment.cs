using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipment : MonoBehaviour
{
    public int turretModules; // 防御塔模块数量
    public int bullets; // 子弹数量

    // Start is called before the first frame update
    void Start()
    {
        turretModules = 1; // 初始防御塔模块数量为1
        bullets = 20; // 初始子弹数量为20
    }

    // Update is called once per frame
    void Update()
    {

    }
}
