﻿using System;
using System.Collections;

namespace Samples
{
    // 用于对更改通知进行挂钩的委托类型。
    public delegate void ChangedEventHandler(object sender, EventArgs e);

    internal class EventListenDemo
    {
        // 测试 ListWithChangedEvent 类。
        public static void Run()
        {
            // 创建新列表。
            ListWithChangedEvent list = new ListWithChangedEvent();

            // 创建一个类，用于侦听列表的更改事件。
            EventListener listener = new EventListener(list);

            // 在列表中添加和移除项。
            list.Add("item 1");
            list.Clear();
            listener.Detach();
        }
    }

    // 一个类，其作用与 ArrayList 类似，
    // 但在每次列表更改时发送通知。
    public class ListWithChangedEvent : ArrayList
    {
        // 一个事件，每当列表元素更改时，客户端可利用该事件
        // 获得通知。

        //or this
        //public event ChangedEventHandler Changed;

        public event EventHandler Changed;

        // 调用 Changed 事件；每当列表更改时调用
        protected virtual void OnChanged(EventArgs e)
        {
            if (Changed != null)
                Changed(this, e);
        }

        // 重写可更改列表的某些方法；
        // 在每个重写后调用事件
        public override int Add(object value)
        {
            int i = base.Add(value);
            OnChanged(EventArgs.Empty);
            return i;
        }

        public override void Clear()
        {
            base.Clear();
            OnChanged(EventArgs.Empty);
        }

        public override object this[int index]
        {
            set
            {
                base[index] = value;
                OnChanged(EventArgs.Empty);
            }
        }
    }

    internal class EventListener
    {
        private ListWithChangedEvent List;

        public EventListener(ListWithChangedEvent list)
        {
            List = list;
            // 将“ListChanged”添加到“List”中的 Changed 事件。
            // or this
            //List.Changed += new ChangedEventHandler(ListChanged);
            List.Changed += new EventHandler(ListChanged);
        }

        // 每当列表更改时就会进行以下调用。
        private void ListChanged(object sender, EventArgs e)
        {
            Console.WriteLine("This is called when the event fires.");
        }

        public void Detach()
        {
            // 分离事件并删除列表
            //or this
            //List.Changed -= new ChangedEventHandler(ListChanged);
            List.Changed -= new EventHandler(ListChanged);
            List = null;
        }
    }
}