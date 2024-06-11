using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 经典的单例模式
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;

    public static T Instance
    {
        get => instance;
    }

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance= (T)this;
        }
    }

    protected void OnDestroy()
    {
        if (instance==this)
        {
            instance = null;
        }
    }
}
