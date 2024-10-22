using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIsimple : MonoBehaviour
{
    public NavMeshAgent agent; // 导航网格代理，用于控制敌人的移动

    public Transform player; // 玩家对象的变换组件
    // 状态
    public float sightRange, attackRange; // 视野范围和攻击范围
    public bool playerInSightRange, playerInAttackRange; // 玩家是否在视野范围和攻击范围内
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

        }
    }
}
