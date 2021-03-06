﻿using System;
using UnityEngine;

namespace UI
{
    // base class for windows
    public abstract class WindowBase : MonoBehaviour, IWindow
    {
        public virtual void Show()
        {
            
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public static GameObject Create(WindowBase window)
        {
            return Instantiate(window.gameObject);
        }
    }
}