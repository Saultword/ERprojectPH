using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIsimple : MonoBehaviour
{
    public NavMeshAgent agent; // ��������������ڿ��Ƶ��˵��ƶ�

    public Transform player; // ��Ҷ���ı任���
    // ״̬
    public float sightRange, attackRange; // ��Ұ��Χ�͹�����Χ
    public bool playerInSightRange, playerInAttackRange; // ����Ƿ�����Ұ��Χ�͹�����Χ��
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
