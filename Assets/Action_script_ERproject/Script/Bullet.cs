using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    public LayerMask bulletLayer; // 子弹的层级
    public float damage; // 子弹的伤害值

    void Start()
    {
        // 设置子弹的层级
        gameObject.layer = Mathf.RoundToInt(Mathf.Log(bulletLayer.value, 2));
    }
}
