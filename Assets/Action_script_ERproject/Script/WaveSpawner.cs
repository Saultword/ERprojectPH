using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GameObject enemyPrefab; // ÿ���ĵ���Prefab
        public int clusterSize; // ÿ����Ⱥ�Ĺ�������
        public float spawnInterval; // ÿ��֮�����ȴʱ��
        public Transform spawnPoint; // ÿ���ĳ�����
    }

    public List<Wave> waves; // ���в��ε�����
    public List<Transform> pathPoints; // ·����
    public float clusterInterval = 1.0f; // ÿ����Ⱥ֮�����ȴʱ��
    public float spawnRadius = 1.0f; // ��������ʱ�İ뾶

    private void Start()
    {
        // ����Ƿ������ò��κ�·����
        if (waves.Count == 0 || pathPoints.Count == 0)
        {
            Debug.LogError("��ȷ�������ò��κ�·���㣡");
            return;
        }

        // ��ʼ���ɲ���
        StartCoroutine(SpawnWaves());
    }

    // Э�̣��������в���
    private IEnumerator SpawnWaves()
    {
        foreach (Wave wave in waves)
        {
            for (int i = 0; i < wave.clusterSize; i++)
            {
                // ����һ����Ⱥ
                SpawnCluster(wave);
                // �ȴ���Ⱥ���ʱ��
                yield return new WaitForSeconds(clusterInterval);
            }

            // �ȴ����μ��ʱ��
            yield return new WaitForSeconds(wave.spawnInterval);
        }
    }

    // ����һ����Ⱥ
    private void SpawnCluster(Wave wave)
    {
        // ��������λ��
        Vector3 spawnPosition = wave.spawnPoint.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = wave.spawnPoint.position.y; // ������ͬһ�߶�

        // ʵ��������
        GameObject enemyInstance = Instantiate(wave.enemyPrefab, spawnPosition, Quaternion.identity);
        NavMeshAgent agent = enemyInstance.GetComponent<NavMeshAgent>();

        // ������Prefab���Ƿ���NavMeshAgent���
        if (agent == null)
        {
            Debug.LogError("����Prefab��ȱ��NavMeshAgent�����");
            return;
        }

        // ��ʼ����·��
        StartCoroutine(FollowPath(agent));
    }

    // Э�̣��õ��˸���·����
    private IEnumerator FollowPath(NavMeshAgent agent)
    {
        foreach (Transform pathPoint in pathPoints)
        {
            // ����Ŀ��·����
            agent.SetDestination(pathPoint.position);

            // �ȴ�ֱ������Ŀ��·����
            while (!agent.pathPending && agent.remainingDistance > agent.stoppingDistance)
            {
                yield return null;
            }

            // �ȴ�һ��ʱ�����ƶ�����һ��·����
            yield return new WaitForSeconds(0.5f);
        }
    }
}
