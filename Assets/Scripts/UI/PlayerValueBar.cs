using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace DS
{
    public class PlayerValueBar : BaseValueBar
    {
       // public Image fillBar;
       // public Image bufferBar;
        //private Tween[] downTweener;
        //Queue<Tween[]> tweens = new Queue<Tween[]>();
        //public float downSpeed;
        //private float targetValue;
        //public float delayTime;
        //private float currentValue;

        public string setMaxValueActionName;
        public string noSmoothActionName;
        public string smoothActionName;
        //public float maxValue;

        private void Awake()
        {
            Init();
        }

        public override void Init()
        {
            base.Init();
            EventCenter.Instance.AddEventListener<float>(setMaxValueActionName, SetMaxValue);
            EventCenter.Instance.AddEventListener<float>(noSmoothActionName, NoSmoothAdd);
            EventCenter.Instance.AddEventListener<float>(smoothActionName, SmoothAdd);

            switch (stateDataType)
            {
                case StateDataType.Health:
                    SetMaxValue(PlayerController.Instance.stateManager.maxHealth);
                    break;
                case StateDataType.Endurance:
                    SetMaxValue(PlayerController.Instance.stateManager.maxEndurance);
                    break;
                case StateDataType.Absorbe:
                    break;
                default:
                    break;
            }
        }



        /*public void SetMaxValue(float maxValue)
        {
            this.maxValue = maxValue;
            currentValue =maxValue;
        }*/

        /*public void SmoothAdd(float changeValue)
        {
            float lastCurrent = currentValue;
            currentValue += changeValue;

            targetValue = currentValue;
            float time = changeValue / downSpeed;

            Tween tF = DOVirtual.Float(lastCurrent, targetValue, time, (x) => { fillBar.fillAmount = x / maxValue; }).SetEase(Ease.Linear);
            Tween tB = DOVirtual.Float(lastCurrent, targetValue, time, (x) => { bufferBar.fillAmount = x / maxValue; }).SetEase(Ease.Linear);
            TweenCoupleReady(tF, tB);
        }*/

        /* private void TweenCoupleReady(Tween tF,Tween tB)
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
         }*/

        /* public void NoSmoothAdd( float changeValue)
         {
             float lastCurrent = currentValue;
             currentValue += changeValue;

             targetValue = currentValue;
             float time = changeValue / downSpeed;
             Tween tF = DOVirtual.Float(lastCurrent, targetValue, time, (x) => { fillBar.fillAmount = x / maxValue; }).SetEase(Ease.Linear);
             Tween tB = DOVirtual.Float(lastCurrent, targetValue, time+delayTime, (x) => { bufferBar.fillAmount = x / maxValue; }).SetEase(Ease.Linear);
             tB.SetDelay(delayTime);

             TweenCoupleReady(tF, tB);
         }*/

        /* private void PlayNextTweener()
         {

             if(tweens.Count>0)
             {
                 foreach(var t in downTweener)
                 {
                     t.Kill();
                 }
                 //downTweener.Kill();
                 var tweenCouple = tweens.Dequeue();
                 foreach(var t in tweenCouple)
                 {
                     t.Play();
                 }

                 downTweener = tweenCouple;
             }
             else
             {
                 foreach(var t in downTweener)
                 {
                     t.Kill();
                 }

                 downTweener = null;
             }
         }*/

        private void OnDestroy()
        {
            EventCenter.Instance.RemoveEvent<float>(noSmoothActionName, NoSmoothAdd);
            EventCenter.Instance.RemoveEvent<float>(smoothActionName, SmoothAdd);
            EventCenter.Instance.RemoveEvent<float>(setMaxValueActionName, SetMaxValue);
        }

    }

}
