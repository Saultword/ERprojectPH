using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public GameObject Plane_Fire;
    private bool isOnFire = false;

    void Start()
    {
        // 在5秒后触发火焰特效
        Invoke("StartFireEffect", 5f);
    }

    void StartFireEffect()
    {
        if (!isOnFire)
        {
            Instantiate(Plane_Fire, transform.position, transform.rotation, transform);
            isOnFire = true;
        }
    }
}
