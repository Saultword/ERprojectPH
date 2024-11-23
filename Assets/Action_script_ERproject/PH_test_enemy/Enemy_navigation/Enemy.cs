using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent nav;
    private Vector3 offset; // ƫ����
    private GameObject des;
    static public GameObject[] waypoints;
    public float health = 100f; // ����ֵ
    static public GameObject bornpoint;
    public GameObject endpoint; // �յ�
    // �ӵ���LayerMask
    public LayerMask bulletLayer;

    public Renderer enemyRenderer; // ���˵���Ⱦ��
    public Color hitColor = Color.red; // �ܻ�ʱ����ɫ
    public float hitDuration = 0.1f; // �ܻ���ɫ����ʱ��

    public delegate void EnemyDied(Vector3 position);
    public event EnemyDied OnEnemyDied;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        enemyRenderer = GetComponent<Renderer>(); // ��ȡ��Ⱦ�����
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

        // �����ײ�����Ƿ����ӵ��㼶��
        if (((1 << collision.gameObject.layer) & bulletLayer) != 0)
        {
            // ��ȡ�ӵ����
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                // �����˺�
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
        Color originalColor = enemyRenderer.material.color; // ����ԭʼ��ɫ
        enemyRenderer.material.color = hitColor; // ����Ϊ�ܻ���ɫ
        yield return new WaitForSeconds(hitDuration); // �ȴ�һ��ʱ��
        enemyRenderer.material.color = originalColor; // �ָ�ԭʼ��ɫ
    }

    void Die()
    {
        // ���������߼�...

        // ���������¼�


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
         * todo: ����Ѱ·�㷨��ʵ��
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
