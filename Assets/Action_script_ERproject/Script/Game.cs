using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game:MonoBehaviour
{
    public bool gamestart = true;
    public EnemyFactory enemyFactory;

    [SerializeField]
    public float waveColdDown = 5.0f;

    private float progress = 5.0f;

    public Vector3[] bornOffset = { new Vector3(-1.5f, 0, 0), new Vector3(0, 0, 0), new Vector3(1.5f, 0, 0) };

    void Update()
    {
        
        progress += Time.deltaTime;
        if (progress >= waveColdDown)
        {
            progress = 0;

            for (int i = 0; i < bornOffset.Length; ++i)
            {
                Enemy enemy = enemyFactory.Get();
                enemy.init(bornOffset[i]);
            }
        }
    }

}
