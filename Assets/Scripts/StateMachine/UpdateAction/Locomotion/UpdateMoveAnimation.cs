using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    [CreateAssetMenu(fileName ="UpdateMoveAnimation",menuName ="FSM/Base/Locomotion/UpdateMoveAnimation")]
    public class UpdateMoveAnimation : UpdateAction
    {
        public float changeSpeed=10;

        public override void Excute(State state)
        {
            var character = state.sm.character;
            Animator anim =character.animatorManager.anim;
            var input = character.input;
            float speed = character.input.Dmag;
            float s = anim.GetFloat(AnimatorParam.Speed.ToString());
            float L = anim.GetFloat(AnimatorParam.Lock.ToString());
            float f = anim.GetFloat(AnimatorParam.Forward.ToString());
            float r = anim.GetFloat(AnimatorParam.Right.ToString());

            float deltaTime = Time.deltaTime;

            if(speed<0.01f)
            {
                anim.SetFloat(AnimatorParam.Speed.ToString(), Mathf.Lerp(s, 0, changeSpeed * deltaTime));
                character.stateManager.ChangeEndurance(2 * deltaTime,true);
            }
            else if(input.Run&&character.stateManager.currentEndurance>5)
            {
                anim.SetFloat(AnimatorParam.Speed.ToString(), Mathf.Lerp(s, 3, changeSpeed * deltaTime));
                character.stateManager.ChangeEndurance(-5 * deltaTime, true);
            }
            else if(input.WalkCheck)
            {
                anim.SetFloat(AnimatorParam.Speed.ToString(), Mathf.Lerp(s, 1, changeSpeed * deltaTime));
                character.stateManager.ChangeEndurance(2 * deltaTime, true);
            }
            else
            {
                anim.SetFloat(AnimatorParam.Speed.ToString(), Mathf.Lerp(s, 2, changeSpeed * deltaTime));
            }

            if(state.sm.character.lockManager.isLock)
            {
                Vector3 v = input.Dvec;
                v.y = 0;

                float y = Vector3.Dot(v, state.sm.character.model.forward);
                float x = Vector3.Dot(v, state.sm.character.model.right);

                anim.SetFloat(AnimatorParam.Lock.ToString(), Mathf.Lerp(L, 1, changeSpeed * deltaTime));
                anim.SetFloat(AnimatorParam.Forward.ToString(), Mathf.Lerp(f, y, changeSpeed * deltaTime));
                anim.SetFloat(AnimatorParam.Right.ToString(), Mathf.Lerp(r, x, changeSpeed * deltaTime));
            }
            else
            {
                anim.SetFloat(AnimatorParam.Lock.ToString(), Mathf.Lerp(L, 0, changeSpeed * deltaTime));
                //anim.SetFloat(AnimatorParam.Right.ToString(), Mathf.Lerp(r, 0, changeSpeed * deltaTime));
                //anim.SetFloat(AnimatorParam.Forward.ToString(), Mathf.Lerp(f, 0, changeSpeed * deltaTime));
            }

            if(character.rigid.velocity.y>-2f)
            {
                character.rigid.velocity += Vector3.down;
            }
            
        }
    }
}

