using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

// 命名空间声明，用于组织与库存物品管理相关的类和方法
namespace Keraz.Inventory
{
    /**
     * ItemManager负责处理游戏场景中物品的实例化与管理。
     * 它响应事件来在指定位置生成物品，并管理这些物品的父级变换。
     */
    public class ItemManager : MonoBehaviour
    {
        // 公有字段，用于存储作为模板的预制物品
        public Item itemPrefab;

        // 私有字段，保存物品实例化的父级变换对象
        private Transform itemParent;
        
        //记录场景Item
        private Dictionary<string,List<SceneItem>> sceneItemsDic = new Dictionary<string, List<SceneItem>>();

        /**
         * 当此组件启用时，注册事件监听以便在场景中即时生成物品。
         */
        private void OnEnable()
        {
            EventHandle.InstantItemInScence += OnInstantItemInScence;
            EventHandle.BeforeSceneUnloadEvent+=OnBeforeSceneUnload;
            EventHandle.AfterSceneLoadedEvent+=OnAfterSceneLoaded;
        }
        

        /**
         * 当此组件禁用时，注销事件监听。
         */
        private void OnDisable()
        {
            EventHandle.InstantItemInScence -= OnInstantItemInScence;
            EventHandle.BeforeSceneUnloadEvent -= OnBeforeSceneUnload;
            EventHandle.AfterSceneLoadedEvent -= OnAfterSceneLoaded;
        }
        private void OnBeforeSceneUnload()
        {
            GetAllSceneItem();
        }

        
        /**
         * 场景加载后调用的私有方法。
         * 该方法的目的是在场景加载后，找到并存储标签为"ItemParent"的游戏对象的变换组件。
         * 这样做的目的是为了后续操作这个父对象及其子对象提供便利。
         */
        private void OnAfterSceneLoaded()
        {
            // 通过标签查找场景中的"ItemParent"对象并获取其变换组件
            itemParent = GameObject.FindWithTag("ItemParent").transform;
            RecreateSceneItem();
        }

        /**
         * 根据接收到的物品ID和位置，在场景中实例化一个物品。
         *
         * @param ID 要实例化的物品的唯一标识符。
         * @param pos 物品在场景中的生成位置。
         */
        private void OnInstantItemInScence(int ID, Vector3 pos)
        {
            // 使用预制体在指定位置和方向实例化物品，并将其父级设为之前找到的itemParent
            var newItem = Instantiate(itemPrefab, pos, Quaternion.identity, itemParent);
            // 设置新实例化物品的ID
            newItem.itemID = ID;
        }
        /// <summary>
        /// 获取当前场景所有物品
        /// </summary>
        private void GetAllSceneItem()
        {
            List<SceneItem> currentSceneItems = new List<SceneItem>();
            foreach (var item in FindObjectsOfType<Item>())
            {
                SceneItem sceneItem = new SceneItem
                {
                    itemID =  item.itemID,
                    position = new SerializeVector3(item.transform.position) 
                };
                currentSceneItems.Add(sceneItem);
            }

            if (sceneItemsDic.ContainsKey(SceneManager.GetActiveScene().name))
            {
                sceneItemsDic[SceneManager.GetActiveScene().name] = currentSceneItems;
            }
            else
            {
                sceneItemsDic.Add(SceneManager.GetActiveScene().name, currentSceneItems);
            }
        }
        /// <summary>
        /// 刷新当前场景物品
        /// </summary>
        private void RecreateSceneItem()
        {
            List<SceneItem> currentSceneItems = new();
            if (sceneItemsDic.TryGetValue(SceneManager.GetActiveScene().name, out currentSceneItems))
            {
                if (currentSceneItems!=null)
                {
                    //清场
                    foreach (var item in FindObjectsOfType<Item>())
                    {
                        Destroy(item.gameObject);
                    }

                    foreach (var item in currentSceneItems)
                    {
                        Item newItem = Instantiate(itemPrefab, item.position.ToVector3(), quaternion.identity);
                        newItem.Init(item.itemID);
                    }
                }
            }
        }
    }
}