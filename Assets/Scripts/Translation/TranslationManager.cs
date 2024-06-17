using System;
using System.Collections;
using System.Collections.Generic;
using Attribute;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Keraz.Transition
{
    public class TranslationManager : MonoBehaviour
    {
        [SceneName]
        public string startSceneName = string.Empty;

        private CanvasGroup fadeCanvasGroup;
        private bool isFade;

        private void Start()
        {
            StartCoroutine(LoadSceneSetActive(startSceneName));
            fadeCanvasGroup = FindObjectOfType<CanvasGroup>();
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
            if(!isFade)
                StartCoroutine(Translation(sceneNameToGo, positionToGo));
        }


        /**
         * 异步加载新场景并卸载当前场景的协程。
         * 此协程用于处理场景之间的过渡，通过首先触发卸载前的事件，
         * 然后卸载当前场景，再加载新场景，并最后触发加载后的事件来实现。
         * 这种设计允许游戏在场景过渡时进行必要的逻辑处理，比如资源释放和加载。
         *
         * @param sceneName 新场景的名称，用于加载新场景。
         * @param targetPosition 新场景中物体的目标位置，用于加载后物体的定位。
         */
        private IEnumerator Translation(string sceneName, Vector3 targetPosition)
        {
            // 触发场景卸载前的事件，允许进行任何必要的准备工作。
            EventHandle.CallBeforeSceneUnloadEvent();
            yield return Fade(1);
            // 卸载当前场景，异步操作并等待完成。
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            
            // 加载新场景
            yield return LoadSceneSetActive(sceneName);
            
            //人物移动到新场景的坐标
            EventHandle.CallMoveToPosition(targetPosition);
            
            // 触发场景卸载后的事件，此时场景已经卸载完成。
            EventHandle.CallAfterSceneLoadedEvent();
            yield return  Fade(0);

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
        /// <summary>
        /// 场景切换淡入淡出
        /// </summary>
        /// <param name="targetAlpha">1黑，0透明</param>
        /// <returns></returns>
        private IEnumerator Fade(float targetAlpha)
        {
            isFade = true;
            fadeCanvasGroup.blocksRaycasts = true;
            float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha)/Settings.fadeDuration;
            while (!Mathf.Approximately(fadeCanvasGroup.alpha,targetAlpha))
            {
                fadeCanvasGroup.alpha=Mathf.MoveTowards(fadeCanvasGroup.alpha,targetAlpha,speed*Time.deltaTime);
                yield return null;
            }

            fadeCanvasGroup.blocksRaycasts = false;
            isFade = false;
        }
    }
}
