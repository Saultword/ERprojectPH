using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGeneration : MonoBehaviour
{
    public GameObject m_prefab; // 怪物的预制体
    public int enemy_number; // 期望的怪物数量
    public string create_tag; // 用于标记怪物的标签
    private GameObject[] enemy; // 当前场景中的怪物数组
    private int number; // 当前场景中的怪物数量

    private void Start()
    {
        // 初始化时查找所有带有指定标签的怪物
        enemy = GameObject.FindGameObjectsWithTag(create_tag);
        number = enemy.Length; // 获取当前怪物数量
    }

    private void Update()
    {
        // 每帧更新时查找所有带有指定标签的怪物
        enemy = GameObject.FindGameObjectsWithTag(create_tag);
        number = enemy.Length; // 更新当前怪物数量

        // 如果当前怪物数量少于期望数量，则生成新的怪物
        if (number < enemy_number)
        {
            GameObject.Instantiate(m_prefab, transform.position, Quaternion.identity);
        }
    }
}
