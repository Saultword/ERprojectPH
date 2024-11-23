using System.Collections;
using System.Collections.Generic;
using System.Net;
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
    public GameObject endpoint; // 终点
    // 子弹的LayerMask
    public LayerMask bulletLayer;

    public Renderer enemyRenderer; // 敌人的渲染器
    public Color hitColor = Color.red; // 受击时的颜色
    public float hitDuration = 0.1f; // 受击颜色持续时间

    public delegate void EnemyDied(Vector3 position);
    public event EnemyDied OnEnemyDied;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        enemyRenderer = GetComponent<Renderer>(); // 获取渲染器组件
        // Transform bornTransform = WayPointManager.BornPoint.transform;
        Transform bornTransform = bornpoint.transform;
        transform.position = bornTransform.position + bornTransform.TransformDirection(offset);
    }

    public void init(Vector3 offset)
    {
        this.offset = offset;

    }

    public void init(Vector3 offset, GameObject BornPoint, GameObject[] Instance,GameObject EndPoint)
    {
        this.offset = offset;
        bornpoint = BornPoint;
        waypoints = Instance;
        endpoint = EndPoint;
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

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {

            Die();
        }
        else
        {
            StartCoroutine(FlashHitColor());
        }
    }

    private IEnumerator FlashHitColor()
    {
        Color originalColor = enemyRenderer.material.color; // 保存原始颜色
        enemyRenderer.material.color = hitColor; // 设置为受击颜色
        yield return new WaitForSeconds(hitDuration); // 等待一段时间
        enemyRenderer.material.color = originalColor; // 恢复原始颜色
    }

    void Die()
    {
        // 其他死亡逻辑...

        // 触发死亡事件


        Destroy(gameObject);
        if (OnEnemyDied != null)
        {
            OnEnemyDied(transform.position);
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
