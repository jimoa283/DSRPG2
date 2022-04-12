using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    public class AudioPlayer : MonoBehaviour
    {
        public CharacterController character;
        public AnimatorManager animatorManager;

        [Header("脚步")]
        public AudioSource foot;

        [Header("翻滚")]
        public AudioSource escape;

        public List<AudioClip> attackClips = new List<AudioClip>();


        public void Init(CharacterController character)
        {
            this.character = character;
            animatorManager = character.animatorManager;
        }

        public void PlayFootAudio()
        {
            PlayMaxWeightAudio(foot);
        }

        public void PlayEscapeAudio()
        {
            PlayMaxWeightAudio(escape);
        }
        
        public void PlayWeaponAttack()
        {

        }

        public void PlayHit()
        {

        }

        private void PlayMaxWeightAudio(AudioSource source)
        {
            var clipInfos = animatorManager.GetCurrentStateClipInfo(0);
            int maxIndex = -1;
            float maxWeight = -1;

            for (int i = 0; i < clipInfos.Length; i++)
            {
                if(clipInfos[i].weight>maxWeight)
                {
                    maxIndex = i;
                    maxWeight = clipInfos[i].weight;
                }
            }

            if(maxIndex!=-1)
            {
                for (int i = 0; i < clipInfos[maxIndex].clip.events.Length; i++)
                {
                    if (clipInfos[maxIndex].clip.events[i].isFiredByAnimator)
                    {
                        source.Play();
                        return;
                    }
                }
            }
        }
    }
}

