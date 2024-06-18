using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    public Sprite normal, tool, seed, item;
    private Sprite currentSprite;
    private Image cursorImage;
    private RectTransform cursorCanvas;
    

    private void Start()
    {
        cursorCanvas = GameObject.FindGameObjectWithTag("CursorCanvas").GetComponent<RectTransform>();
        //获取UI组件
        cursorImage = cursorCanvas.GetChild(1).GetComponent<Image>();
        currentSprite = normal;
        currentSprite = normal;
        SetCursorImage(normal);
        
    }

    private void OnEnable()
    {
        EventHandle.ItemSelectedEvent += OntemSelectedEvent;
    }

    private void OnDisable()
    {
        EventHandle.ItemSelectedEvent -= OntemSelectedEvent;
    }
    private void Update()
    {
        if (cursorCanvas==null)
            return;
        cursorImage.transform.position = Input.mousePosition;
        SetCursorImage(currentSprite);
        if (!InteractWithUI())
        {
            SetCursorImage(currentSprite);
        }
        else
        {
            SetCursorImage(normal);
        }
    }
    

    private void OntemSelectedEvent(ItemDetails itemDetails, bool isSelected)
    {
        if (!isSelected)
        {
            currentSprite = normal;
        }
        else
        {
            //WWRKFLOW:添加所有类型对应的图片
            currentSprite=itemDetails.itemType switch
            {
                ItemType.ChopTool => tool,
                ItemType.Seed => seed,
                ItemType.Commodity => item,
                _ => normal
            };
        }
    }

    

    private void SetCursorImage(Sprite sprite)
    {
        cursorImage.sprite = sprite;
        cursorImage.color= Color.white;
    }
    
    /// <summary>
    /// UI的互动
    /// </summary>
    /// <returns></returns>
    private bool InteractWithUI()
    {
        if (EventSystem.current!=null & EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }

        return false;
    }
}
