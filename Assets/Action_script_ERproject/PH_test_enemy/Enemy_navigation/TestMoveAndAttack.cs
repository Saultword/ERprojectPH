using UnityEngine;
using UnityEngine.AI;

public class TestMoveAndAttack : MonoBehaviour
{
    public NavMeshAgent agent; // ��������������ڿ��Ƶ��˵��ƶ�

    public Transform player; // ��Ҷ���ı任���
    public Transform towerdefense;
    public Transform basedefense;

    public LayerMask whatIsGround, whatIsPlayer, whatIsBaseDefense,whatIsTowerDefense; // �������ҡ��������ص�ͼ������

    public float health; // ���˵�����ֵ

    // Ѳ��
    public Vector3 walkPoint; // Ѳ�ߵ�
    bool walkPointSet; // �Ƿ�������Ѳ�ߵ�
    public float walkPointRange; // Ѳ�ߵ�ķ�Χ

    // ����
    public float timeBetweenAttacks; // �������ʱ��
    bool alreadyAttacked; // �Ƿ��Ѿ�������
    public GameObject projectile; // Ͷ����

    // ״̬
    public float sightRange, attackRange; // ��Ұ��Χ�͹�����Χ
    public bool playerInSightRange, playerInAttackRange; // ����Ƿ�����Ұ��Χ�͹�����Χ��
    public bool towerDefenseInSightRange, towerDefenseInAttackRange; // �������Ƿ�����Ұ��Χ�͹�����Χ��
    public bool baseDefenseInSightRange, baseDefenseInAttackRange; // �����Ƿ�����Ұ��Χ�͹�����Χ��
    private void Awake()
    {
        // ��ȡ��Ҷ���ı任���
        player = GameObject.Find("Player").transform;
        // ��ȡ��������������
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        basedefense =GameObject.Find("BaseDefense").transform;


    }

    private void Update()
    {
        // �������Ƿ�����Ұ��Χ�͹�����Χ��
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        baseDefenseInSightRange= Physics.CheckSphere(transform.position, sightRange, whatIsBaseDefense);
        baseDefenseInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsBaseDefense);
        // ������ҵ�λ��״ִ̬�в�ͬ����Ϊ
        //if (!playerInSightRange && !playerInAttackRange) SearchingBase();

       // if (playerInAttackRange && playerInSightRange && !baseDefenseInAttackRange && !baseDefenseInSightRange) AttackPlayer();

        if (baseDefenseInSightRange && !baseDefenseInAttackRange)
            ChaseBaseDefense();
        //if (baseDefenseInAttackRange && baseDefenseInSightRange)
          //  AttackBaseDefense();


    }

    private void SearchingBase()
    {
        // ���û������Ѳ�ߵ㣬������һ���µ�Ѳ�ߵ�
        //if (!walkPointSet) SearchWalkPoint();

        // ���õ���Ŀ��ΪѲ�ߵ�
       // if (walkPointSet)
            agent.SetDestination(walkPoint);

        // Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // �������Ѳ�ߵ㣬������Ѳ�ߵ�����
        // if (distanceToWalkPoint.magnitude < 1f)
        // walkPointSet = false;
        agent.destination = basedefense.position;
    }

    private void SearchWalkPoint()
    {
        // ���㷶Χ�ڵ������
        //float randomZ = Random.Range(-walkPointRange, walkPointRange);
       // float randomX = Random.Range(-walkPointRange, walkPointRange);

       // walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // ���������Ƿ��ڵ�����
       // if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            //walkPointSet = true;

    }

    private void ChasePlayer()
    {
        // ���õ���Ŀ��Ϊ���λ��
        agent.SetDestination(player.position);
    }
    private void ChaseBaseDefense()
    {
        // ���õ���Ŀ��Ϊ����λ��
        agent.destination = basedefense.position;
        //agent.SetDestination(basedefense.position);
    }
    private void ChaseBasedefense()
    {
        // ���õ���Ŀ��Ϊ����λ��
        agent.SetDestination(basedefense.position);
    }
    private void AttackPlayer()
    {
        // ȷ�����˲��ƶ�
        agent.SetDestination(transform.position);

        // �������
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // ��������
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            // �����������

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks); // �������ù�������
        }
    }
    private void AttackBaseDefense()
    {
        // ȷ�����˲��ƶ�
        agent.SetDestination(transform.position);

        // �������
        transform.LookAt(basedefense);

        if (!alreadyAttacked)
        {
            // ��������
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            // �����������

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks); // �������ù�������
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false; // ���ù���״̬
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // ��������ֵ

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f); // �������ֵС�ڵ���0���������ٵ��˷���
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject); // ���ٵ��˶���
    }

    private void OnDrawGizmosSelected()
    {
        // ���ƹ�����Χ����Ұ��Χ��Gizmos
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}