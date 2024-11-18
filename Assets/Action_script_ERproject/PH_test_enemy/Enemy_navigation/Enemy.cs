using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent nav;
    private Vector3 offset; // 偏移量
    private GameObject des;
    static public GameObject[] waypoints;
    public float health = 100f; // 生命值
    static public GameObject bornpoint;
    // 子弹的LayerMask
    public LayerMask bulletLayer;

    void Start()
    {

        nav = GetComponent<NavMeshAgent>();
        // Transform bornTransform = WayPointManager.BornPoint.transform;
        Transform bornTransform = bornpoint.transform;
        transform.position = bornTransform.position + bornTransform.TransformDirection(offset);
    }

    public void init(Vector3 offset)
    {
        this.offset = offset;

    }
    public void init(Vector3 offset,GameObject BornPoint, GameObject[] Instance)
    {
        this.offset = offset;
        bornpoint = BornPoint;
        waypoints = Instance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("OnCollisionEnter");

        // 检查碰撞对象是否在子弹层级中
        if (((1 << collision.gameObject.layer) & bulletLayer) != 0)
        {
            // 获取子弹组件
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                // 处理伤害
                TakeDamage(bullet.damage);
            }
        }
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
     public GameObject getNextDes(GameObject des)
    {
        if (!des)
        {
            return waypoints[0];
        }

        int idx = 0;
        for (int i = 0; i < waypoints.Length; ++i)
        {
            if (waypoints[i] == des)
            {
                idx = i;
                break;
            }
        }

        if (idx < waypoints.Length - 1)
        {
            return waypoints[idx + 1];
        }
        else
        {
            return null;
        }

        /*
         * todo: 后续寻路算法的实现
         */
    }
    void Update()
    {
        if (!nav.pathPending && nav.remainingDistance <= 0.5f)
        {
            GameObject origin = des ? des : bornpoint;
            des = getNextDes(des);

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
