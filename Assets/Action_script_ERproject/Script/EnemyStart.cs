using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;

namespace Valve.VR.InteractionSystem.Sample
{
    public class EnemyStart : MonoBehaviour
    {
        public WayPointManager wayPointManager; // 添加对WayPointManager的引用
        public AudioSource audioSource; // 添加AudioSource变量

        public void OnButtonDown(Hand fromHand)
        {
            if (wayPointManager != null)
            {
                wayPointManager.isSpawningEnabled = true; // 设置isSpawningEnabled为true
            }

            if (audioSource != null)
            {
                audioSource.Play(); // 播放音乐
            }
        }

        public void OnButtonUp(Hand fromHand)
        {

        }

        private void Update()
        {

        }

        private void ColorSelf(Color newColor)
        {
            Renderer[] renderers = this.GetComponentsInChildren<Renderer>();
            for (int rendererIndex = 0; rendererIndex < renderers.Length; rendererIndex++)
            {
                renderers[rendererIndex].material.color = newColor;
            }
        }
    }
}
