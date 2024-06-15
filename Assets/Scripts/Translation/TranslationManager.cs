using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Keraz.Transition
{
    public class TranslationManager : MonoBehaviour
    {
        public string startSceneName = string.Empty;

        private void Start()
        {
            StartCoroutine(LoadSceneSetActive(startSceneName));
        }

        private void OnEnable()
        {
            EventHandle.TransitionEvent += OnTransitionEvent;
        }

        private void OnDisable()
        {
            EventHandle.TransitionEvent -= OnTransitionEvent;
        }

        private void OnTransitionEvent(string sceneNameToGo, Vector3 positionToGo)
        {
            StartCoroutine(Translation(sceneNameToGo, positionToGo));
        }


        private IEnumerator Translation(string sceneName, Vector3 targetPosition)
        {
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            yield return SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);
        }

        /**
        * 异步加载场景并设置为活跃场景。
        * 使用Coroutine异步加载指定名称的场景，并在加载完成后将其设置为活跃场景。
        * 这种方法适用于在游戏运行时动态加载新场景或替换当前场景。
        *
        * @param name 待加载场景的名称。
        */
        private IEnumerator LoadSceneSetActive(string name)
        {
            // 异步加载指定名称的场景,场景叠加加载
            yield return SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);

            // 获取刚刚加载的场景对象。
            Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

            // 将新加载的场景设置为活跃场景。
            SceneManager.SetActiveScene(newScene);
        }
    }
}
