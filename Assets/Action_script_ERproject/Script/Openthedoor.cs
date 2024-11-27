using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openthedoor : MonoBehaviour
{
    public LayerMask playerLayer; // 用于检测玩家的LayerMask
    public Animator animator; // Animator组件
    public GameManager startflag;
    void Start()
    {
       
    }

    void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0&&startflag.isstart)
        {
            animator.SetTrigger("PlayerProximity");
            animator.ResetTrigger("Closedoor");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0 && startflag.isstart)
        {
            animator.SetTrigger("Closedoor");
            animator.ResetTrigger("PlayerProximity");
        }
    }
}