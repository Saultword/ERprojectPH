using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using System;
using UnityEditor;

namespace Valve.VR.InteractionSystem.Sample
{
    public class AircraftCrash : MonoBehaviour
    {
        public SceneAsset sceneAsset; // 要切换到的场景
        private PlayableDirector playableDirector; // PlayableDirector组件

        void Start()
        {
            playableDirector = GetComponent<PlayableDirector>();
            if (playableDirector != null)
            {
                playableDirector.stopped += OnPlayableDirectorStopped;
            }
        }

        private void OnPlayableDirectorStopped(PlayableDirector director)
        {
            LoadScene();
        }

        private void LoadScene()
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
