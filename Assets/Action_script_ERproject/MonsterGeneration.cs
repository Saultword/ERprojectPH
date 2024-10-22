using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGeneration : MonoBehaviour
{
    public GameObject m_prefab; // �����Ԥ����
    public int enemy_number; // �����Ĺ�������
    public string create_tag; // ���ڱ�ǹ���ı�ǩ
    private GameObject[] enemy; // ��ǰ�����еĹ�������
    private int number; // ��ǰ�����еĹ�������

    private void Start()
    {
        // ��ʼ��ʱ�������д���ָ����ǩ�Ĺ���
        enemy = GameObject.FindGameObjectsWithTag(create_tag);
        number = enemy.Length; // ��ȡ��ǰ��������
    }

    private void Update()
    {
        // ÿ֡����ʱ�������д���ָ����ǩ�Ĺ���
        enemy = GameObject.FindGameObjectsWithTag(create_tag);
        number = enemy.Length; // ���µ�ǰ��������

        // �����ǰ�����������������������������µĹ���
        if (number < enemy_number)
        {
            GameObject.Instantiate(m_prefab, transform.position, Quaternion.identity);
        }
    }
}
