using System;
using UnityEditor;
using UnityEngine;

namespace Attribute
{
    /// <summary>
    /// 自定义属性绘制器，用于SceneNameAttribute属性。
    /// </summary>
    [CustomPropertyDrawer(typeof(SceneNameAttribute))]
    public class SceneNameDrawer : PropertyDrawer
    {
        
        /// <summary>
        /// 当前选中的场景索引。
        /// </summary>
        int sceneIndex = -1;
        
        /// <summary>
        /// 场景名称的数组，用于GUI显示。
        /// </summary>
        GUIContent[] sceneNames;
        
        /// <summary>
        /// 用于分割场景路径的字符串数组。
        /// </summary>
        private readonly string[] scenePathSplit = { "/", ".unity" };

        /// <summary>
        /// 在GUI上绘制属性字段。
        /// </summary>
        /// <param name="position">属性字段在GUI上的位置。</param>
        /// <param name="property">要绘制的序列化属性。</param>
        /// <param name="label">属性字段的标签。</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // 如果没有场景设置，则不进行任何操作
            if (EditorBuildSettings.scenes.Length == 0)
            {
                return;
            }

            // 如果场景名称数组未初始化，则初始化它
            if (sceneIndex == -1)
            {
                GetSceneNameArray(property);
            }

            // 显示场景下拉列表，并更新选中的场景索引
            int oldIndex = sceneIndex;
            sceneIndex = EditorGUI.Popup(position, label, sceneIndex, sceneNames);

            // 如果选中的场景发生了变化，则更新属性值
            if (oldIndex != sceneIndex)
            {
                property.stringValue = sceneNames[sceneIndex].text;
            }
        }

        /// <summary>
        /// 获取场景名称数组，并根据当前属性值设置选中的场景索引。
        /// </summary>
        /// <param name="property">要获取场景名称的序列化属性。</param>
        private void GetSceneNameArray(SerializedProperty property)
        {
            // 获取编辑器构建设置中的场景数组
            var scenes = EditorBuildSettings.scenes;

            // 初始化场景名称数组
            sceneNames = new GUIContent[scenes.Length];
            for (int i = 0; i < sceneNames.Length; i++)
            {
                // 分割场景路径以获取场景名称
                string path = scenes[i].path;
                string[] splitPath = path.Split(scenePathSplit, StringSplitOptions.RemoveEmptyEntries);
                string sceneName = "";
                if (splitPath.Length > 0)
                {
                    sceneName = splitPath[splitPath.Length - 1];
                }
                else
                {
                    sceneName = "(Deleted Scene)";
                }
                // 将场景名称添加到数组中
                sceneNames[i] = new GUIContent(sceneName);
            }

            // 如果没有场景，显示提示信息
            if (sceneNames.Length == 0)
            {
                sceneNames = new[] { new GUIContent("Check Your Build Settings") };
            }

            // 根据属性值设置选中的场景索引
            if (!string.IsNullOrEmpty(property.stringValue))
            {
                bool nameFound = false;
                for (int i = 0; i < sceneNames.Length; i++)
                {
                    if (sceneNames[i].text == property.stringValue)
                    {
                        sceneIndex = i;
                        nameFound = true;
                        break;
                    }
                }
                if (nameFound == false)
                {
                    sceneIndex = 0;
                }
            }
            else
            {
                sceneIndex = 0;
            }
            // 更新属性值为当前选中的场景名称
            property.stringValue = sceneNames[sceneIndex].text;
        }
    }
}
