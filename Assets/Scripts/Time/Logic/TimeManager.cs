using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    //初始化年份，用于游戏时间初始化
    private const int MaxYear = 9999;
    private const int ResetYear = 2024;

    private const int MonthsInSeason = 3;

    // 游戏时间相关变量，用于存储游戏内的小时、分钟、秒、月份、日期和年份
    private int gameSecond, gameMinute, gameHour, gameMonth, gameDay, gameYear;

    // 游戏季节，初始化为春季
    private Season gameSeason;

    // 季节中所处的月份，假设每个季节有三个月，初始值为3表示季节的第三个月
    private int monthInSeason = 3;

    // 控制游戏时间是否暂停的标志位
    public bool gameClockPause;
    private float tikTime;

    private void Awake()
    {
        InitGameTime();
    }

    private void Start()
    {
        EventHandle.CallGameDataEvent(gameHour,gameDay,gameMonth,gameYear,gameSeason);
        EventHandle.CallGameMinuteEvent(gameMinute,gameHour);
    }

    private void Update()
    {
        // 检查游戏是否处于暂停状态
        if (!gameClockPause)
        {
            // 更新游戏时间累积量，以deltaTime为增量
            tikTime += Time.deltaTime;
            // 检查累积时间是否超过一秒阈值
            if (tikTime >= Settings.secondThreshold)
            {
                // 如果超过，减去阈值以防止时间过度累积
                tikTime -= Settings.secondThreshold;
                // 触发更新游戏时间的函数，通常用于更新UI或游戏逻辑
                UpdateGameTime();
            }
        }
    }

    /// <summary>
    /// 初始化游戏时间
    /// </summary>
    private void InitGameTime()
    {
        gameSecond = 0;
        gameMinute = 0;
        gameHour = 7;
        gameDay = 1;
        gameMonth = 3;
        gameYear = 2024;
        gameSeason = Season.Spring;
    }

    /// <summary>
    /// 更新游戏时间。
    /// 此方法负责根据当前时间状态递增时间，并处理时间的循环和边界条件。
    /// </summary>
    public void UpdateGameTime()
    {
        gameSecond++;
        // 当秒数超过预设的最大值时，重置秒数，并递增分钟
        if (gameSecond > Settings.secondHold)
        {
            gameSecond = 0;
            IncrementMinute();
            EventHandle.CallGameMinuteEvent(gameMinute,gameHour);
        }

        // 当分钟超过预设的最大值时，重置分钟，并递增小时
        if (gameMinute > Settings.minuteHold)
        {
            gameMinute = 0;
            IncrementHour();
            EventHandle.CallGameDataEvent(gameHour,gameDay,gameMonth,gameYear,gameSeason);
        }

        // 当小时超过预设的最大值时，重置小时，并递增天数
        if (gameHour > Settings.hourHold)
        {
            gameHour = 0;
            IncrementDay();
            
        }

        // 当天数超过预设的最大值时，重置天数为1，并递增月份
        if (gameDay > Settings.dayHold)
        {
            gameDay = 1;
            IncrementMonth();
        }

        // 当月份超过预设的最大值时，重置月份为1，并递增季节内的月份
        if (gameMonth > Settings.monthHold)
        {
            gameMonth = 1;
            IncrementMonthInSeason();
        }

        // 当季节内的月份归零时，重置季节内的月份，并递增季节
        if (monthInSeason == 0)
        {
            monthInSeason = MonthsInSeason;
            IncrementSeason();
        }

        // 调试用：输出当前时间
        DebugLogCurrentTime();
    }

    /// <summary>
    /// 递增分钟。
    /// </summary>
    private void IncrementMinute()
    {
        gameMinute++;
    }

    /// <summary>
    /// 递增小时。
    /// </summary>
    private void IncrementHour()
    {
        gameHour++;
    }

    /// <summary>
    /// 递增天数。
    /// </summary>
    private void IncrementDay()
    {
        gameDay++;
    }

    /// <summary>
    /// 递增月份。
    /// </summary>
    private void IncrementMonth()
    {
        gameMonth++;
    }

    /// <summary>
    /// 递减季节内的月份，用于处理季节循环。
    /// </summary>
    private void IncrementMonthInSeason()
    {
        monthInSeason--;
    }

    /// <summary>
    /// 递增季节，并处理季节循环和年份递增。
    /// </summary>
    private void IncrementSeason()
    {
        int seasonNumber = (int)gameSeason + 1;
        // 当季节超过预设的最大值时，重置季节为0，并递增年份
        if (seasonNumber > Settings.seasonHold)
        {
            seasonNumber = 0;
            gameYear++;
            // 当年份超过预设的最大值时，重置年份为重置年
            if (gameYear > MaxYear)
            {
                gameYear = ResetYear;
            }
        }

        gameSeason = (Season)seasonNumber;
    }

    /// <summary>
    /// 测试时间
    /// </summary>
    private void DebugLogCurrentTime()
    {
        Debug.Log($"{gameSecond}:{gameMinute}:{gameHour}:{gameDay}:{gameMonth}:{gameYear}");
    }
}