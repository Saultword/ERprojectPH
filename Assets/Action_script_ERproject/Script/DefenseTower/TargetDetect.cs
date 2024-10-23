using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetect : MonoBehaviour
{
    public float sightRange = 10f; // 检测范围
    public float attackRange = 10f; // 攻击范围
    public LayerMask enemyLayer; // 敌人图层
    private Transform currentTarget; // 当前目标
    public Transform bulletPrefab; // 子弹预制件
    public Transform firePoint; // 发射点
    public Vector3 firePointOffset; // 发射点偏移量
    public float bulletSpeed = 10f; // 子弹速度
    public float attackInterval = 1f; // 攻击间隔
    private float attackTimer; // 攻击计时器
    // Start is called before the first frame update
    void Start()
    {
        // 初始化代码可以放在这里
    }

    // Update is called once per frame
    void Update()
    {
        // 使用 Physics.OverlapSphere 检测范围内的所有敌人
        Collider[] enemies = Physics.OverlapSphere(transform.position, sightRange, enemyLayer);

        if (enemies.Length > 0)
        {
            // 找到最近的敌人
            currentTarget = GetNearestEnemy(enemies);
        }
        else
        {
            // 如果没有敌人，将当前目标设为 null
            currentTarget = null;
        }

        // 如果有目标，转向目标
        if (currentTarget != null)
        {
            Debug.Log("找到目标！");
            RotateTowards(currentTarget);

            // 更新攻击计时器
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                Shoot();
                attackTimer = attackInterval; // 重置攻击计时器
            }
        }
    }

    // 获取范围内最近的敌人
    private Transform GetNearestEnemy(Collider[] enemies)
    {
        Transform nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        // 遍历所有检测到的敌人，找到最近的一个
        foreach (Collider enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }

        return nearestEnemy;
    }

    // 使游戏对象转向目标
    private void RotateTowards(Transform target)
    {
        // 计算指向目标的方向
        Vector3 direction = (target.position - transform.position).normalized;
        // 计算目标方向的四元数旋转
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // 获取当前旋转角度
        Vector3 currentEulerAngles = transform.rotation.eulerAngles;
        // 获取目标旋转角度
        Vector3 targetEulerAngles = lookRotation.eulerAngles;

        // 计算新的旋转角度，限制Y轴旋转角度在-45到45度之间
        float newPitch = Mathf.Clamp(targetEulerAngles.x, -45f, 45f);
        Quaternion newRotation = Quaternion.Euler(newPitch, targetEulerAngles.y, currentEulerAngles.z);

        // 平滑地旋转到目标方向
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 5f);
    }

    private void Shoot()
    {
        Debug.Log("射了");
        if (bulletPrefab != null && firePoint != null && currentTarget != null)
        {
            // 计算发射点位置
            Vector3 firePosition = firePoint.position + firePointOffset;
            // 计算指向目标的方向
            Vector3 direction = (currentTarget.position - firePoint.position).normalized;
            // 计算目标方向的四元数旋转
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            // 实例化子弹
            Transform bullet = Instantiate(bulletPrefab, firePosition, lookRotation);
            // 设置子弹的速度和方向
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            if (bulletRigidbody != null)
            {
                bulletRigidbody.velocity = direction * bulletSpeed;
            }
        }
        else
        {
            Debug.LogWarning("子弹预制件或发射点未设置！");
        }
    }

    // 在编辑器中绘制攻击范围和视野范围的 Gizmos，方便调试
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
