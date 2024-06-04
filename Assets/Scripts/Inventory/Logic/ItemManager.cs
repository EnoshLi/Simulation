using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Keraz.Inventory
{


    public class ItemManager : MonoBehaviour
    {
        //地图上的物品
        public Item itemPrefab;
        //物品所在的父级目录
        private Transform itemParent;

        private void OnEnable()
        {
            EventHandle.InstantItemInScence += OnInstantItemInScence;
        }

        private void OnDisable()
        {
            EventHandle.InstantItemInScence -= OnInstantItemInScence;
        }

        private void Start()
        {
            itemParent = GameObject.FindWithTag("ItemParent").transform;
        }
        
        /// <summary>
        /// 生成物品在场景里
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="pos"></param>
        private void OnInstantItemInScence(int ID, Vector3 pos)
        {
            
            //将物品栏里的物品生成地图上
            var  item = Instantiate(itemPrefab, pos, Quaternion.identity,itemParent);
            item.itemID = ID;
        }
    }
}
