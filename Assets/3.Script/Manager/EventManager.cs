using System;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class EventManager
{
    private Dictionary<EVENT_TYPE, List<IListener>> Listeners = new();
    public Action<Define.DB_Event> DBEvent;

    public void AddListener(EVENT_TYPE Event_Type, IListener Listener)
    {

        List<IListener> ListenList = null;

        if (Listeners.TryGetValue(Event_Type, out ListenList))
        {
            ListenList.Add(Listener);
            return;
        }
        ListenList = new List<IListener>
        {
            Listener
        };
        Listeners.Add(Event_Type, ListenList);
    }

    // 이벤트를 발송하는 메서드
    public void PostNotification(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        List<IListener> ListenList = null;
        if (!Listeners.TryGetValue(Event_Type, out ListenList))
            return;

        for (int i = 0; i < ListenList.Count; i++)
        {
            if (!ListenList[i].Equals(null))
                ListenList[i].OnEvent(Event_Type, Sender, Param);
        }
    }
    public void RemoveEvent(EVENT_TYPE Event_Type)
    {
        Listeners.Remove(Event_Type);
    }

    public void RemoveRedundancies()
    {
        Dictionary<EVENT_TYPE, List<IListener>> TmpListeners = new Dictionary<EVENT_TYPE, List<IListener>>();
        foreach (KeyValuePair<EVENT_TYPE, List<IListener>> Item in Listeners)
        {
            for (int i = Item.Value.Count - 1; i >= 0; i--)
            {
                if (Item.Value[i].Equals(null))
                    Item.Value.RemoveAt(i);
            }

            if (Item.Value.Count > 0)
                TmpListeners.Add(Item.Key, Item.Value);
        }
        Listeners = TmpListeners;
    }
    public void ClearEventList()
    {
        Listeners?.Clear();
    }
}