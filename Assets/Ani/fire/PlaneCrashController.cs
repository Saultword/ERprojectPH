using UnityEngine;

public class PlaneCrashController : MonoBehaviour
{
    public GameObject bigExplosionEffectPrefab;
    void Start()
    {

    }
    private void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        // ��⵽��ײʱ�����������Ч
        GameObject explosion = Instantiate(bigExplosionEffectPrefab, transform.position, transform.rotation);
        // ���ٷɻ�����
        Destroy(explosion, 2f);
        Destroy(gameObject);
    }
}
