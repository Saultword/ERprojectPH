using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GameObject enemyPrefab; // 每波的敌人Prefab
        public int clusterSize; // 每个集群的怪物数量
        public float spawnInterval; // 每波之间的冷却时间
        public Transform spawnPoint; // 每波的出生点
    }

    public List<Wave> waves; // 所有波次的配置
    public List<Transform> pathPoints; // 路径点
    public float clusterInterval = 1.0f; // 每个集群之间的冷却时间
    public float spawnRadius = 1.0f; // 怪物生成时的半径

    private void Start()
    {
        // 检查是否已设置波次和路径点
        if (waves.Count == 0 || pathPoints.Count == 0)
        {
            Debug.LogError("请确保已设置波次和路径点！");
            return;
        }

        // 开始生成波次
        StartCoroutine(SpawnWaves());
    }

    // 协程：生成所有波次
    private IEnumerator SpawnWaves()
    {
        foreach (Wave wave in waves)
        {
            for (int i = 0; i < wave.clusterSize; i++)
            {
                // 生成一个集群
                SpawnCluster(wave);
                // 等待集群间隔时间
                yield return new WaitForSeconds(clusterInterval);
            }

            // 等待波次间隔时间
            yield return new WaitForSeconds(wave.spawnInterval);
        }
    }

    // 生成一个集群
    private void SpawnCluster(Wave wave)
    {
        // 计算生成位置
        Vector3 spawnPosition = wave.spawnPoint.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = wave.spawnPoint.position.y; // 保持在同一高度

        // 实例化敌人
        GameObject enemyInstance = Instantiate(wave.enemyPrefab, spawnPosition, Quaternion.identity);
        NavMeshAgent agent = enemyInstance.GetComponent<NavMeshAgent>();

        // 检查敌人Prefab上是否有NavMeshAgent组件
        if (agent == null)
        {
            Debug.LogError("敌人Prefab上缺少NavMeshAgent组件！");
            return;
        }

        // 开始跟随路径
        StartCoroutine(FollowPath(agent));
    }

    // 协程：让敌人跟随路径点
    private IEnumerator FollowPath(NavMeshAgent agent)
    {
        foreach (Transform pathPoint in pathPoints)
        {
            // 设置目标路径点
            agent.SetDestination(pathPoint.position);

            // 等待直到到达目标路径点
            while (!agent.pathPending && agent.remainingDistance > agent.stoppingDistance)
            {
                yield return null;
            }

            // 等待一段时间再移动到下一个路径点
            yield return new WaitForSeconds(0.5f);
        }
    }
}
