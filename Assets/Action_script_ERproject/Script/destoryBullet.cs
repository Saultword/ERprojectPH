using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destoryBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3.0f); // 表示子弹三秒后被销毁
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        // 发生碰撞后销毁自己
        Destroy(gameObject);
    }
}
