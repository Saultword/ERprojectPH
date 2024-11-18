using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyFactory : ScriptableObject
{
    [SerializeField]
    Enemy prefabs;
    public Enemy Get()
    {

        return Instantiate(prefabs);
    }
}



