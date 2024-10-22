using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour
{

    // 路径脚本
    [SerializeField]
    private WaypointCircuit circuit;
    public float speed=10;
    //移动距离
    public float dis=0;
    //移动速度
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //计算距离
        dis += Time.deltaTime * speed;
        //获取相应距离在路径上的位置坐标
        transform.position = circuit.GetRoutePoint(dis).position;
        //获取相应距离在路径上的方向
        transform.rotation = Quaternion.LookRotation(circuit.GetRoutePoint(dis).direction);
    }
}

