using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton 
{
    public bool isPressing = false; //正在按下
    public bool onPressed = false; //按下瞬间
    public bool onReleased = false; //释放瞬间
    public bool isExtending = false;//处于释放按键之后的延长时间
    public bool isDelaying = false; //长按
    public float extendingDuration = 0.15f; //延长时间
    public float delayingDuration = 0.15f; //长按时间

    private bool curState = false; //当前按下状态
    private bool lastState = false; //上一帧按下状态
    private float pressTime = 0;
    private Timer exitTimer = new Timer();
    private Timer delayTimer = new Timer();

    public void Tick(bool input) //update调用
    {
        exitTimer.Tick();
        delayTimer.Tick();
        curState = input;
        isPressing = curState;

        if (isPressing)
            pressTime += Time.deltaTime;
        else
            pressTime = 0;

        onPressed = false;
        onReleased = false;

        if(curState!=lastState)
        {
            if(curState)           //按下
            {
                onPressed = true;
                StartTimer(delayTimer, delayingDuration);          //开始延迟计时器
            }
            else                                                 //释放
            {
                onReleased = true;
                StartTimer(exitTimer, extendingDuration);          //开始延伸计时器
            }
        }

        lastState = curState;

        isExtending = exitTimer.state == Timer.STATE.RUN;
        isDelaying = delayTimer.state == Timer.STATE.RUN;
    }

    private void StartTimer(Timer timer,float duration)
    {
        timer.duration = duration;
        timer.Go();
    }

    public bool LongPressCheck(float minTime)
    {
        return pressTime > minTime;
    }

    class Timer
    {
        public float duration = 1f; //持续时间
        private float elapseTime = 0; //当前运行时间

        public enum STATE
        {
            IDLE, //未启动
            RUN, //正在运行
            FINISHED//运行完毕
        }

        public STATE state = STATE.IDLE;

        public void Tick()//每一帧调用
        {
            switch (state)
            {
                case STATE.IDLE:
                    break;
                case STATE.RUN:
                    elapseTime += Time.deltaTime;
                    if (elapseTime >= duration)
                        state = STATE.FINISHED;
                    break;
                case STATE.FINISHED:
                    break;
                default:
                    break;
            }
        }

        public void Go() //开启定时器
        {
            elapseTime = 0;
            state = STATE.RUN;
        }
    }
}
