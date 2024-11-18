using UnityEngine;

public class WayPointManager : MonoBehaviour
{
    [SerializeField]
    public GameObject bornPoint;

    [SerializeField]
    public GameObject[] wayPoints;

    static public GameObject BornPoint;
    public EnemyFactory enemyFactory;

    [SerializeField]
    public float waveColdDown = 5.0f;//每波冷却

    private float progress = 5.0f;//速度

    public Vector3[] bornOffset = { new Vector3(-1.5f, 0, 0), new Vector3(0, 0, 0), new Vector3(1.5f, 0, 0) };
    // public GameObject[] BornPoints;//获取多个出生点

    static WayPointManager instance;

    void Start()
    {
        //int playerPointLayer = LayerMask.NameToLayer("PlayerPoint");
        //BornPoints = FindObjectsOfType<GameObject>();
        //BornPoints = System.Array.FindAll(BornPoints, go => go.layer == playerPointLayer);
        instance = this;
        BornPoint = bornPoint;
    }
    private void Update()
    {
        progress += Time.deltaTime;
        if (progress >= waveColdDown)
        {
            progress = 0;

            for (int i = 0; i < bornOffset.Length; ++i)
            {
                Enemy enemy = enemyFactory.Get();
                enemy.init(bornOffset[i], BornPoint, wayPoints);
            }
        }
    }

    // 获取下一个路径点

}
