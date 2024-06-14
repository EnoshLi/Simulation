using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    /// <summary>
    /// 存储日夜背景的RectTransform组件。
    /// </summary>
    public Image dayNightImage;

    /// <summary>
    /// 存储时钟父物体的RectTransform组件。
    /// </summary>
    public RectTransform clockParent;

    /// <summary>
    /// 存储季节图片的Image组件。
    /// </summary>
    public Image seasonImage;

    public Sprite[] dayNightImages;

    /// <summary>
    /// 存储日期文本的TextMeshProUGUI组件。
    /// </summary>
    public TextMeshProUGUI dateText;

    /// <summary>
    /// 存储时间文本的TextMeshProUGUI组件。
    /// </summary>
    public TextMeshProUGUI timeText;

    /// <summary>
    /// 存储季节图片的Sprite数组。
    /// </summary>
    public Sprite[] seasonSprites;

    /// <summary>
    /// 存储时钟块的游戏对象列表，用于动态显示时间。
    /// </summary>
    private List<GameObject> clockBlocks = new();

    /// <summary>
    /// 当脚本实例醒来时调用，用于初始化时钟块列表。
    /// </summary>
    private void Awake()
    {
        // 遍历时钟父物体的所有子物体
        for (int i = 0; i < clockParent.childCount; i++)
        {
            // 将每个时钟块添加到列表中，并设置为初始不可用状态
            clockBlocks.Add(clockParent.GetChild(i).gameObject);
            clockParent.GetChild(i).gameObject.SetActive(false);
        }
    }
    

    /// <summary>
    /// 当此游戏对象启用时调用，用于订阅游戏分钟事件和游戏数据事件。
    /// </summary>
    private void OnEnable()
    {
        EventHandle.GameMinuteEvent += OnGameMinuteEvent; // 订阅游戏分钟事件
        EventHandle.GameDataEvent += OnGameDataEvent; // 订阅游戏数据事件
    }

    /// <summary>
    /// 当此游戏对象禁用时调用，用于取消订阅游戏分钟事件和游戏数据事件。
    /// </summary>
    private void OnDisable()
    {
        EventHandle.GameMinuteEvent -= OnGameMinuteEvent; // 取消订阅游戏分钟事件
        EventHandle.GameDataEvent -= OnGameDataEvent; // 取消订阅游戏数据事件
    }

    /// <summary>
    /// 处理游戏分钟事件，更新时间文本。
    /// </summary>
    /// <param name="minute">当前分钟数。</param>
    /// <param name="hour">当前小时数。</param>
    private void OnGameMinuteEvent(int minute, int hour)
    {
        timeText.text = hour.ToString("00") + ":"+minute.ToString("00"); // 格式化并更新时间文本
    }

    /// <summary>
    /// 处理游戏数据事件，更新日期文本、季节图片和小时图片。
    /// </summary>
    /// <param name="hour">当前小时数。</param>
    /// <param name="day">当前日数。</param>
    /// <param name="month">当前月数。</param>
    /// <param name="year">当前年数。</param>
    /// <param name="season">当前季节。</param>
    private void OnGameDataEvent(int hour, int day, int month, int year, Season season)
    {
        dateText.text = year + "年" + month.ToString("00") + "月" + day.ToString("00") + "日"; // 格式化并更新日期文本
        seasonImage.sprite = seasonSprites[(int)season]; // 更新季节图片
        SwitchHourImage(hour); // 更新小时图片
        DayNightImageRotate(hour);//转换日夜背景UI
    }

    /// <summary>
    /// 切换小时图片，根据当前小时数显示正确的小时块。
    /// </summary>
    /// <param name="hour">当前小时数。</param>
    private void SwitchHourImage(int hour)
    {
        int index = hour / 4; // 计算当前小时应该显示的小时块索引
        if (index == 0)
        {
            foreach (var item in clockBlocks)
            {
                item.SetActive(false); // 如果小时小于4，隐藏所有小时块
            }
        }
        else
        {
            for (int i = 0; i < clockBlocks.Count; i++)
            {
                if (i < index+1)
                {
                    clockBlocks[i].SetActive(true); // 启用当前及之前小时块
                }
                else
                {
                    clockBlocks[i].SetActive(false); // 隐藏之后的小时块
                }
            }
        }
    }
    /// <summary>
    /// 转换日夜背景UI
    /// </summary>
    /// <param name="hour">小时</param>
    private void DayNightImageRotate(int hour)
    {
        switch (hour)
        {
            case 7:
                dayNightImage.DOFade(0, 1).OnComplete(
                    () =>
                    {
                        dayNightImage.sprite = dayNightImages[0];
                        dayNightImage.DOFade(1, 1);
                        dayNightImage.SetNativeSize();
                    }
                );
                break;
            case 8:
                dayNightImage.DOFade(0, 1).OnComplete(
                    () =>
                    {
                        dayNightImage.sprite = dayNightImages[1];
                        dayNightImage.DOFade(1, 1);
                        dayNightImage.SetNativeSize();
                    }
                    );
                break;
            case 9:
                dayNightImage.DOFade(0, 1).OnComplete(
                    () =>
                    {
                        dayNightImage.sprite = dayNightImages[2];
                        dayNightImage.DOFade(1, 1);
                        dayNightImage.SetNativeSize();
                    }
                );
                break;
            case 10:
                dayNightImage.DOFade(0, 1).OnComplete(
                    () =>
                    {
                        dayNightImage.sprite = dayNightImages[3];
                        dayNightImage.DOFade(1, 1);
                        dayNightImage.SetNativeSize();
                    }
                );
                break;
            
        }
    }
}
