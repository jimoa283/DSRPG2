using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    [CreateAssetMenu(fileName ="locomotionAsset",menuName ="Locomotion/AnimationAsset")]
    public class LocomotionAsset : ScriptableObject
    {
        public LocomotionType type = LocomotionType.LongSword;

        [Header("单手")]
        [SerializeField]
        private List<LocomotionTypeAnimation> singleHand = null;

        [Header("双手")]
        [SerializeField]
        private List<LocomotionTypeAnimation> bothHand = null;

        [Header("翻滚")]
        [SerializeField]
        private List<LocomotionTypeAnimation> escape = null;

        public List<LocomotionTypeAnimation> GetLocomotionAnimations(bool isBothHand)
        {
            return isBothHand ? bothHand : singleHand;
        }

        public List<LocomotionTypeAnimation> GetEscapeAnimation()
        {
            return escape;
        }

    }

    [System.Serializable]
    public struct LocomotionTypeAnimation
    {
        public AnimationType type;
        public AnimationClip clip;
    }
}

