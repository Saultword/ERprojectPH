using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour
{

    // ·���ű�
    [SerializeField]
    private WaypointCircuit circuit;
    public float speed=10;
    //�ƶ�����
    public float dis=0;
    //�ƶ��ٶ�
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //�������
        dis += Time.deltaTime * speed;
        //��ȡ��Ӧ������·���ϵ�λ������
        transform.position = circuit.GetRoutePoint(dis).position;
        //��ȡ��Ӧ������·���ϵķ���
        transform.rotation = Quaternion.LookRotation(circuit.GetRoutePoint(dis).direction);
    }
}

