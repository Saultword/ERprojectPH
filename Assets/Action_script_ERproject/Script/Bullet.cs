using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    public LayerMask bulletLayer; // �ӵ��Ĳ㼶
    public float damage; // �ӵ����˺�ֵ

    void Start()
    {
        // �����ӵ��Ĳ㼶
        gameObject.layer = Mathf.RoundToInt(Mathf.Log(bulletLayer.value, 2));
    }
}
