using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;

namespace Valve.VR.InteractionSystem.Sample
{
    public class EnemyStart : MonoBehaviour
    {
        public WayPointManager wayPointManager; // ��Ӷ�WayPointManager������
        public AudioSource audioSource; // ���AudioSource����

        public void OnButtonDown(Hand fromHand)
        {
            if (wayPointManager != null)
            {
                wayPointManager.isSpawningEnabled = true; // ����isSpawningEnabledΪtrue
            }

            if (audioSource != null)
            {
                audioSource.Play(); // ��������
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
