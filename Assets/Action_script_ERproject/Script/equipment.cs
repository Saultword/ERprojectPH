using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipment : MonoBehaviour
{
    public int turretModules; // ������ģ������
    public int bullets; // �ӵ�����

    // Start is called before the first frame update
    void Start()
    {
        turretModules = 1; // ��ʼ������ģ������Ϊ1
        bullets = 20; // ��ʼ�ӵ�����Ϊ20
    }

    // Update is called once per frame
    void Update()
    {

    }
    public int GetTurretModuleCount()
    {
        return turretModules;
    }
    public void DecreaseTurretModuleCount()
    {
        if (turretModules >= 1)
        {
            turretModules = turretModules-1;
        }
    }
}
