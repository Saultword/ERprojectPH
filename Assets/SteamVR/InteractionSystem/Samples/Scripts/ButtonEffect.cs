using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;

namespace Valve.VR.InteractionSystem.Sample
{
    public class ButtonEffect : MonoBehaviour
    {
        public SceneAsset sceneAsset; // 要切换到的场景
        private float buttonPressTime = 0f; // 按钮按下的时间
        private bool isButtonPressed = false; // 按钮是否被按下

        public void OnButtonDown(Hand fromHand)
        {
            ColorSelf(Color.cyan);
            fromHand.TriggerHapticPulse(1000);
            isButtonPressed = true;
            buttonPressTime = 0f; // 重置计时器
        }

        public void OnButtonUp(Hand fromHand)
        {
            ColorSelf(Color.white);
            isButtonPressed = false;
        }

        private void Update()
        {
            if (isButtonPressed)
            {
                buttonPressTime += Time.deltaTime;
                if (buttonPressTime >= 2f)
                {
                    if (sceneAsset != null)
                    {
                        string sceneName = sceneAsset.name;
                        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
                    }
                    else
                    {
                        Debug.LogError("SceneAsset is not assigned.");
                    }
                }
            }
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
