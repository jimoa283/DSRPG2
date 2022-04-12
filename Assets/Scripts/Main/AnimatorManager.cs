using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    public class AnimatorManager : MonoBehaviour
    {
        public CharacterController character;

        public bool canRootMotion;

        public Animator anim { get; private set; }
        public AnimatorOverrideController overrideAnim { get; private set; }

        private AnimationClipOverrides clipOverrides;

        public LocomotionDataBase locomotionDataBase;
        private Dictionary<LocomotionType, LocomotionAsset> locomotionDic = new Dictionary<LocomotionType, LocomotionAsset>();

        public void Init(CharacterController character)
        {
            this.character = character;

            anim = GetComponent<Animator>();

            overrideAnim = new AnimatorOverrideController(anim.runtimeAnimatorController);
            anim.runtimeAnimatorController = overrideAnim;

            clipOverrides = new AnimationClipOverrides(overrideAnim.overridesCount);

            foreach(var p in locomotionDataBase.locomotionAssets)
            {
                locomotionDic.Add(p.type, p);
            }
        }

        public void SetLocomotion(LocomotionType locomotionType,bool isBothHand)
        {
            List<LocomotionTypeAnimation> locomotions = locomotionDic[locomotionType].GetLocomotionAnimations(isBothHand);
            overrideAnim.GetOverrides(clipOverrides);
            foreach(var t in locomotions)
            {
                clipOverrides[t.type.ToString()] = t.clip;
            }
            overrideAnim.ApplyOverrides(clipOverrides);
        }

        public void SetAttack(List<AttackActionData> singlelist,List<AttackActionData> bothHandList) 
        {
            overrideAnim.GetOverrides(clipOverrides);
            foreach(var t in singlelist)
            {
                clipOverrides[t.animTrigger.animTypeName] = t.animTrigger.attackAnimationClip;
            }
            
            foreach(var t in bothHandList)
            {
                clipOverrides[t.animTrigger.animTypeName] = t.animTrigger.attackAnimationClip;
            }

            overrideAnim.ApplyOverrides(clipOverrides);
        }

        public AnimatorStateInfo GetCurrentStateInfo(int layerInex)
        {
            return anim.GetCurrentAnimatorStateInfo(layerInex);
        }

        public AnimatorClipInfo[] GetCurrentStateClipInfo(int layerIndex)
        {
            return anim.GetCurrentAnimatorClipInfo(layerIndex);
        }

        public void ChangeHandAnimation(bool bothHand)
        {
            anim.SetBool(AnimatorParam.Ban.ToString(),true);

            if(bothHand)
            {
                anim.SetInteger(AnimatorParam.ChangeTrans.ToString(), 2);
            }
            else
            {
                anim.SetInteger(AnimatorParam.ChangeTrans.ToString(), 1);
            }

        }

        private void OnAnimatorMove()
        {
            if (!canRootMotion)
                return;
            float y = character.rigid.velocity.y;
            Vector3 v = character.animatorManager.anim.deltaPosition / Time.fixedDeltaTime;
            v.y = y;
            character.rigid.velocity = v;
        }
    }

    public class AnimationClipOverrides:List<KeyValuePair<AnimationClip,AnimationClip>>
    {
        public AnimationClipOverrides(int capacity) : base(capacity) { }

        public AnimationClip this[string name]
        {
            get { return this.Find(x => x.Key.name.Equals(name)).Value; }
            set
            {
                int index = this.FindIndex(x => x.Key.name.Equals(name));
                if (index != -1)
                    this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
            }
        }
    }
}

