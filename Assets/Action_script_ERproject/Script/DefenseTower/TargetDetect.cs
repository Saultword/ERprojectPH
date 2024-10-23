using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetect : MonoBehaviour
{
    public float sightRange = 10f; // ��ⷶΧ
    public float attackRange = 10f; // ������Χ
    public LayerMask enemyLayer; // ����ͼ��
    private Transform currentTarget; // ��ǰĿ��
    public Transform bulletPrefab; // �ӵ�Ԥ�Ƽ�
    public Transform firePoint; // �����
    public Vector3 firePointOffset; // �����ƫ����
    public float bulletSpeed = 10f; // �ӵ��ٶ�
    public float attackInterval = 1f; // �������
    private float attackTimer; // ������ʱ��
    // Start is called before the first frame update
    void Start()
    {
        // ��ʼ��������Է�������
    }

    // Update is called once per frame
    void Update()
    {
        // ʹ�� Physics.OverlapSphere ��ⷶΧ�ڵ����е���
        Collider[] enemies = Physics.OverlapSphere(transform.position, sightRange, enemyLayer);

        if (enemies.Length > 0)
        {
            // �ҵ�����ĵ���
            currentTarget = GetNearestEnemy(enemies);
        }
        else
        {
            // ���û�е��ˣ�����ǰĿ����Ϊ null
            currentTarget = null;
        }

        // �����Ŀ�꣬ת��Ŀ��
        if (currentTarget != null)
        {
            Debug.Log("�ҵ�Ŀ�꣡");
            RotateTowards(currentTarget);

            // ���¹�����ʱ��
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                Shoot();
                attackTimer = attackInterval; // ���ù�����ʱ��
            }
        }
    }

    // ��ȡ��Χ������ĵ���
    private Transform GetNearestEnemy(Collider[] enemies)
    {
        Transform nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        // �������м�⵽�ĵ��ˣ��ҵ������һ��
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

    // ʹ��Ϸ����ת��Ŀ��
    private void RotateTowards(Transform target)
    {
        // ����ָ��Ŀ��ķ���
        Vector3 direction = (target.position - transform.position).normalized;
        // ����Ŀ�귽�����Ԫ����ת
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // ��ȡ��ǰ��ת�Ƕ�
        Vector3 currentEulerAngles = transform.rotation.eulerAngles;
        // ��ȡĿ����ת�Ƕ�
        Vector3 targetEulerAngles = lookRotation.eulerAngles;

        // �����µ���ת�Ƕȣ�����Y����ת�Ƕ���-45��45��֮��
        float newPitch = Mathf.Clamp(targetEulerAngles.x, -45f, 45f);
        Quaternion newRotation = Quaternion.Euler(newPitch, targetEulerAngles.y, currentEulerAngles.z);

        // ƽ������ת��Ŀ�귽��
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 5f);
    }

    private void Shoot()
    {
        Debug.Log("����");
        if (bulletPrefab != null && firePoint != null && currentTarget != null)
        {
            // ���㷢���λ��
            Vector3 firePosition = firePoint.position + firePointOffset;
            // ����ָ��Ŀ��ķ���
            Vector3 direction = (currentTarget.position - firePoint.position).normalized;
            // ����Ŀ�귽�����Ԫ����ת
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            // ʵ�����ӵ�
            Transform bullet = Instantiate(bulletPrefab, firePosition, lookRotation);
            // �����ӵ����ٶȺͷ���
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            if (bulletRigidbody != null)
            {
                bulletRigidbody.velocity = direction * bulletSpeed;
            }
        }
        else
        {
            Debug.LogWarning("�ӵ�Ԥ�Ƽ������δ���ã�");
        }
    }

    // �ڱ༭���л��ƹ�����Χ����Ұ��Χ�� Gizmos���������
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
