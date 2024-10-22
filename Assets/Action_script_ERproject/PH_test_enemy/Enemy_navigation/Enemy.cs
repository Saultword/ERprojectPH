using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent nav;

    private Vector3 offset;	// ƫ����

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
                // ��from��end�ķ�������
                Vector3 dir = des.transform.position - origin.transform.position;
                dir = new Vector3(dir.x, 0, dir.z).normalized;
                // ����������תΪ��Ԫ��
                Quaternion dirQua = Quaternion.LookRotation(dir);
                // �ó�������ƫ����
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

