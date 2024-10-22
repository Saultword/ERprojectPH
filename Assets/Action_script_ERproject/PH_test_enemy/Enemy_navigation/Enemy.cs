using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent nav;

    private Vector3 offset;	// 偏移量

    private GameObject des;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        Transform bornTransform = WayPointManager.BornPoint.transform;
        transform.position = bornTransform.position + bornTransform.TransformDirection(offset);
    }


    public void init(Vector3 offset)
    {
        this.offset = offset;
    }


    void Update()
    {
        if (!nav.pathPending && nav.remainingDistance <= 0.5f)
        {
            GameObject origin = des ? des : WayPointManager.BornPoint;
            des = WayPointManager.getNextDes(des);

            if (des)
            {
                // 从from到end的方向向量
                Vector3 dir = des.transform.position - origin.transform.position;
                dir = new Vector3(dir.x, 0, dir.z).normalized;
                // 将方向向量转为四元数
                Quaternion dirQua = Quaternion.LookRotation(dir);
                // 得出真正的偏移量
                Vector3 realOffset = dirQua * offset;

                nav.SetDestination(des.transform.position + realOffset);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}

