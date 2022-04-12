using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace DS
{
    public class BaseValueBar : MonoBehaviour
    {
        public Image fillBar;
        public Image bufferBar;
        protected Tween[] downTweener;
        public StateDataType stateDataType;
        Queue<Tween[]> tweens = new Queue<Tween[]>();
        public float downSpeed;
        protected float targetValue;
        public float delayTime;
        protected float currentValue;
        public float maxValue;


/*        private void Awake()
        {
            Init();
        }*/

        public virtual void Init()
        {
            fillBar = transform.Find("FillBar").GetComponent<Image>();
            bufferBar = transform.Find("BufferBar").GetComponent<Image>();
        }

        public void SetValue(float currentValue,float maxValue)
        {
            if (fillBar == null)
                Init();
            this.currentValue = currentValue;
            this.maxValue = currentValue;

            fillBar.fillAmount = currentValue / maxValue;
            bufferBar.fillAmount = currentValue / maxValue;
        }

        public void SetMaxValue(float maxValue)
        {
            this.maxValue = maxValue;
            currentValue = maxValue;
        }

        public void SmoothAdd(float changeValue)
        {
            float lastCurrent = currentValue;
            currentValue += changeValue;

            targetValue = currentValue;
            float time = Mathf.Abs(changeValue) / downSpeed;

            Tween tF = DOVirtual.Float(lastCurrent, targetValue, time, (x) => { fillBar.fillAmount = x / maxValue;}).SetEase(Ease.Linear);
            Tween tB = DOVirtual.Float(lastCurrent, targetValue, time, (x) => { bufferBar.fillAmount = x / maxValue; }).SetEase(Ease.Linear);
            TweenCoupleReady(tF, tB);
        }

        protected void TweenCoupleReady(Tween tF, Tween tB)
        {
            tB.onComplete += PlayNextTweener;
            tF.SetAutoKill(false);
            tB.SetAutoKill(false);
            tF.Pause();
            tB.Pause();

            tweens.Enqueue(new Tween[] { tF, tB });
            if (tweens.Count == 1 && downTweener == null)
            {
                downTweener = tweens.Dequeue();
                foreach (var t in downTweener)
                {
                    t.Play();
                }
            }
        }

        public void NoSmoothAdd(float changeValue)
        {
            float lastCurrent = currentValue;
            currentValue += changeValue;

            targetValue = currentValue;
            float time = Mathf.Abs(changeValue) / downSpeed;
            Tween tF = DOVirtual.Float(lastCurrent, targetValue, time, (x) => { fillBar.fillAmount = x / maxValue; }).SetEase(Ease.Linear);
            Tween tB = DOVirtual.Float(lastCurrent, targetValue, time + delayTime, (x) => { bufferBar.fillAmount = x / maxValue; }).SetEase(Ease.Linear);
            tB.SetDelay(delayTime);

            TweenCoupleReady(tF, tB);
        }

        protected void PlayNextTweener()
        {

            if (tweens.Count > 0)
            {
                foreach (var t in downTweener)
                {
                    t.Kill();
                }
                //downTweener.Kill();
                var tweenCouple = tweens.Dequeue();
                foreach (var t in tweenCouple)
                {
                    t.Play();
                }

                downTweener = tweenCouple;
            }
            else
            {
                foreach (var t in downTweener)
                {
                    t.Kill();
                }

                downTweener = null;
            }
        }
    }
}

