using UnityEngine;

public class WayPointManager : MonoBehaviour
{
    [SerializeField]
    public GameObject[] bornPoints; // �޸�Ϊ���飬֧�ֶ��������

    [SerializeField]
    public GameObject[] wayPoints;

    static public GameObject[] BornPoints;
    public EnemyFactory enemyFactory;

    [SerializeField]
    public float waveColdDown = 5.0f; // ÿ����ȴ

    private float progress = 5.0f; // �ٶ�

    static WayPointManager instance;

    // �����Ĺ������飬���ڿ��ƹ���Ĳ���������
    public int[] waveEnemyCounts;
    public GameObject endpoint;

    // �����Ĺ������飬����ָ��ÿ�������һ��������� towerpoint
    public int[] towerPointDropWave;

    // �����Ĺ������������ڰ� towerpoint Ԥ����
    public GameObject towerPointPrefab;

    // ���������飬���ڶ���ÿһ���ĳ�����
    public GameObject[] waveBornPoints;

    private int currentWave = 0;
    private int enemiesSpawnedInWave = 0;
    private bool towerPointDropped = false;

    // �����Ĺ����������������ڿ����Ƿ�ʼ����
    public bool isSpawningEnabled = false;

    private int totalEnemiesSpawned = 0; // �ܹ����ֵ�����
    private int enemiesDestroyed = 0; // �����ٵĹ�������
    public Endgame endgame; // ����Endgame�ű�

    void Start()
    {
        instance = this;
        BornPoints = bornPoints;

        // �����ܹ����ֵ�����
        foreach (int count in waveEnemyCounts)
        {
            totalEnemiesSpawned += count;
        }
    }

    private void Update()
    {
        if (!isSpawningEnabled)
        {
            return; // ���isSpawningEnabledΪfalse�������ɹ���
        }

        if (currentWave >= waveEnemyCounts.Length)
        {
            return; // ���в����Ĺ��ﶼ���������
        }

        progress += Time.deltaTime;
        if (progress >= waveColdDown)
        {
            progress = 0;

            if (enemiesSpawnedInWave < waveEnemyCounts[currentWave])
            {
                for (int i = 0; i < waveEnemyCounts[currentWave]; ++i)
                {
                    Vector3 randomOffset = new Vector3(
                        Random.Range(-3f, 3f),
                        0,
                        Random.Range(-3f, 3f)
                    );

                    // ʹ�õ�ǰ���ε�ָ��������
                    GameObject currentWaveBornPoint = waveBornPoints[currentWave];

                    Enemy enemy = enemyFactory.Get();
                    enemy.init(randomOffset, currentWaveBornPoint, wayPoints, endpoint);

                    // ��������������¼�
                    enemy.OnEnemyDied += HandleEnemyDied;

                    enemiesSpawnedInWave++;
                    if (enemiesSpawnedInWave >= waveEnemyCounts[currentWave])
                    {
                        break;
                    }
                }
            }
            else
            {
                currentWave++;
                enemiesSpawnedInWave = 0;
                towerPointDropped = false;
            }
        }
    }

    private void HandleEnemyDied(Vector3 position)
    {
        Debug.Log("Enemy died at " + position);
        enemiesDestroyed++;

        // ��鵱ǰ�����Ƿ��� towerPointDropWave ������
        if (!towerPointDropped && System.Array.Exists(towerPointDropWave, wave => wave == currentWave))
        {
            GameObject towerPoint = Instantiate(towerPointPrefab, position, Quaternion.identity);
            towerPointDropped = true;
            Debug.Log("Tower point dropped");
        }

        // ���ʤ������
        CheckVictoryCondition();
    }

    private void CheckVictoryCondition()
    {
        if (enemiesDestroyed == totalEnemiesSpawned && endgame.Basehealth > 0)
        {
            endgame.victory = true;
        }
    }

    // �� Inspector �и���ʱ�Զ����� waveBornPoints ����Ĵ�С
    private void OnValidate()
    {
        if (waveEnemyCounts != null)
        {
            if (waveBornPoints == null || waveBornPoints.Length != waveEnemyCounts.Length)
            {
                waveBornPoints = new GameObject[waveEnemyCounts.Length];
            }
        }
    }
}
