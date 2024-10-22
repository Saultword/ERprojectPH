using UnityEngine;

public class WayPointManager : MonoBehaviour
{
    [SerializeField]
    GameObject bornPoint;

    [SerializeField]
    GameObject[] wayPoints;

    static public GameObject BornPoint;

    static WayPointManager instance;

    void Start()
    {
        instance = this;
        BornPoint = bornPoint;
    }


    // ��ȡ��һ��·����
    static public GameObject getNextDes(GameObject des)
    {
        if (!des)
        {
            return instance.wayPoints[0];
        }

        int idx = 0;
        for (int i = 0; i < instance.wayPoints.Length; ++i)
        {
            if (instance.wayPoints[i] == des)
            {
                idx = i;
                break;
            }
        }

        if (idx < instance.wayPoints.Length - 1)
        {
            return instance.wayPoints[idx + 1];
        }
        else
        {
            return null;
        }

        /*
         * todo: ����Ѱ·�㷨��ʵ��
         */
    }
}
