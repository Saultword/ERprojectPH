using UnityEngine;
using UnityEngine.AI;

public class TestMoveAndAttack : MonoBehaviour
{
    public NavMeshAgent agent; // 导航网格代理，用于控制敌人的移动

    public Transform player; // 玩家对象的变换组件
    public Transform towerdefense;
    public Transform basedefense;

    public LayerMask whatIsGround, whatIsPlayer, whatIsBaseDefense,whatIsTowerDefense; // 地面和玩家、塔、基地的图层掩码

    public float health; // 敌人的生命值

    // 巡逻
    public Vector3 walkPoint; // 巡逻点
    bool walkPointSet; // 是否设置了巡逻点
    public float walkPointRange; // 巡逻点的范围

    // 攻击
    public float timeBetweenAttacks; // 攻击间隔时间
    bool alreadyAttacked; // 是否已经攻击过
    public GameObject projectile; // 投射物

    // 状态
    public float sightRange, attackRange; // 视野范围和攻击范围
    public bool playerInSightRange, playerInAttackRange; // 玩家是否在视野范围和攻击范围内
    public bool towerDefenseInSightRange, towerDefenseInAttackRange; // 防御塔是否在视野范围和攻击范围内
    public bool baseDefenseInSightRange, baseDefenseInAttackRange; // 基地是否在视野范围和攻击范围内
    private void Awake()
    {
        // 获取玩家对象的变换组件
        player = GameObject.Find("Player").transform;
        // 获取导航网格代理组件
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        basedefense =GameObject.Find("BaseDefense").transform;


    }

    private void Update()
    {
        // 检查玩家是否在视野范围和攻击范围内
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        baseDefenseInSightRange= Physics.CheckSphere(transform.position, sightRange, whatIsBaseDefense);
        baseDefenseInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsBaseDefense);
        // 根据玩家的位置状态执行不同的行为
        //if (!playerInSightRange && !playerInAttackRange) SearchingBase();

       // if (playerInAttackRange && playerInSightRange && !baseDefenseInAttackRange && !baseDefenseInSightRange) AttackPlayer();

        if (baseDefenseInSightRange && !baseDefenseInAttackRange)
            ChaseBaseDefense();
        //if (baseDefenseInAttackRange && baseDefenseInSightRange)
          //  AttackBaseDefense();


    }

    private void SearchingBase()
    {
        // 如果没有设置巡逻点，则搜索一个新的巡逻点
        //if (!walkPointSet) SearchWalkPoint();

        // 设置导航目标为巡逻点
       // if (walkPointSet)
            agent.SetDestination(walkPoint);

        // Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // 如果到达巡逻点，则重置巡逻点设置
        // if (distanceToWalkPoint.magnitude < 1f)
        // walkPointSet = false;
        agent.destination = basedefense.position;
    }

    private void SearchWalkPoint()
    {
        // 计算范围内的随机点
        //float randomZ = Random.Range(-walkPointRange, walkPointRange);
       // float randomX = Random.Range(-walkPointRange, walkPointRange);

       // walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // 检查随机点是否在地面上
       // if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            //walkPointSet = true;

    }

    private void ChasePlayer()
    {
        // 设置导航目标为玩家位置
        agent.SetDestination(player.position);
    }
    private void ChaseBaseDefense()
    {
        // 设置导航目标为基地位置
        agent.destination = basedefense.position;
        //agent.SetDestination(basedefense.position);
    }
    private void ChaseBasedefense()
    {
        // 设置导航目标为基地位置
        agent.SetDestination(basedefense.position);
    }
    private void AttackPlayer()
    {
        // 确保敌人不移动
        agent.SetDestination(transform.position);

        // 面向玩家
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // 攻击代码
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            // 攻击代码结束

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks); // 调用重置攻击方法
        }
    }
    private void AttackBaseDefense()
    {
        // 确保敌人不移动
        agent.SetDestination(transform.position);

        // 面向玩家
        transform.LookAt(basedefense);

        if (!alreadyAttacked)
        {
            // 攻击代码
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            // 攻击代码结束

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks); // 调用重置攻击方法
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false; // 重置攻击状态
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // 减少生命值

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f); // 如果生命值小于等于0，调用销毁敌人方法
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject); // 销毁敌人对象
    }

    private void OnDrawGizmosSelected()
    {
        // 绘制攻击范围和视野范围的Gizmos
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}