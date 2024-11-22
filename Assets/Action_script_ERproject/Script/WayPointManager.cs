using UnityEngine;

public class WayPointManager : MonoBehaviour
{
    [SerializeField]
    public GameObject[] bornPoints; // 修改为数组，支持多个出生点

    [SerializeField]
    public GameObject[] wayPoints;

    static public GameObject[] BornPoints;
    public EnemyFactory enemyFactory;

    [SerializeField]
    public float waveColdDown = 5.0f; // 每波冷却

    private float progress = 5.0f; // 速度

    static WayPointManager instance;

    // 新增的公开数组，用于控制怪物的波数和数量
    public int[] waveEnemyCounts;
    public GameObject endpoint;

    // 新增的公开数组，用于指定每波中随机一个怪物掉落 towerpoint
    public int[] towerPointDropWave;

    // 新增的公开变量，用于绑定 towerpoint 预制体
    public GameObject towerPointPrefab;

    // 新增的数组，用于定义每一波的出生点
    public GameObject[] waveBornPoints;

    private int currentWave = 0;
    private int enemiesSpawnedInWave = 0;
    private bool towerPointDropped = false;

    void Start()
    {
        instance = this;
        BornPoints = bornPoints;
    }

    private void Update()
    {
        if (currentWave >= waveEnemyCounts.Length)
        {
            return; // 所有波数的怪物都已生成完毕
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

                    // 使用当前波次的指定出生点
                    GameObject currentWaveBornPoint = waveBornPoints[currentWave];

                    Enemy enemy = enemyFactory.Get();
                    enemy.init(randomOffset, currentWaveBornPoint, wayPoints, endpoint);

                    // 监听怪物的死亡事件
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
        // 检查当前波次是否在 towerPointDropWave 数组中
        if (!towerPointDropped && System.Array.Exists(towerPointDropWave, wave => wave == currentWave))
        {
            GameObject towerPoint = Instantiate(towerPointPrefab, position, Quaternion.identity);
            towerPointDropped = true;
            Debug.Log("Tower point dropped");
        }
    }

    // 在 Inspector 中更新时自动调整 waveBornPoints 数组的大小
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
