using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class InputBuffer
    {
        private float bufferTime = 0.4f;                                    //按键缓冲时间
        private Queue<BufferItem> buffers = new Queue<BufferItem>();        //缓冲队列
        private Queue<BufferItem> lastBuffers = new Queue<BufferItem>();    //失效的缓冲存在这里，重复利用

        public InputBuffer(float bufferTime)
        {
            this.bufferTime = bufferTime;
        }

        /// <summary>
        /// 更新缓冲的时间
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Update(float deltaTime)                             
        {
            foreach (var b in buffers)
                b.Update(deltaTime);

            DeleteKey();
        }

        public void AddKey()
        {
            BufferItem bufferItem;
            if (lastBuffers.Count > 0)
            {
                bufferItem = lastBuffers.Dequeue();
                bufferItem.Reset();
            }
            else
            {
                bufferItem = new BufferItem(bufferTime);
            }

            buffers.Enqueue(bufferItem);
        }

        private void DeleteKey()
        {
            if (buffers.Count > 0)
            {
                BufferItem buffer = buffers.Peek();
                while (!buffer.isValid)
                {
                    lastBuffers.Enqueue(buffers.Dequeue());
                    if (buffers.Count > 0)
                        buffer = buffers.Peek();
                    else
                        break;
                }
            }
        }

        public bool ReadKey()
        {
            if (buffers.Count > 0)
            {
                BufferItem buffer = buffers.Peek();
                buffer.isValid = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool BlockKeyPass()
        {
            if (buffers.Count > 0)
            {
                BufferItem buffer = buffers.Peek();
                buffer.isValid = false;
                lastBuffers.Enqueue(buffers.Dequeue());
                return true;
            }
            else
                return false;
        }

     /// <summary>
     /// 缓冲物
     /// </summary>
        class BufferItem
        {
            public float restTime;    //剩余时间
            public float totalTime;   //总时间
            public bool isValid = true;  //是否有效

            public BufferItem(float totalTime)
            {
                this.totalTime = totalTime;
                restTime = totalTime;
            }

            public void Reset()
            {
                restTime = totalTime;
            }

            public void Update(float deltaTime)
            {
                if (!isValid)
                    return;
                restTime -= deltaTime;
                if (restTime < 0)
                    isValid = false;
            }
        }
    }


