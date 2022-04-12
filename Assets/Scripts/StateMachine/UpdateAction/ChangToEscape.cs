using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    [CreateAssetMenu(fileName ="ChangeToEscape",menuName ="FSM/Base/ToEscape")]
    public class ChangToEscape : UpdateAction
    {
        private TransIndex transIndex = TransIndex.Escape;

        public float jabLimit=0.05f;
        public float startTime;

        public override void Excute(State state)
        {
            var input = state.sm.character.input;
            bool escape = input.inputBufferDic[InputBufferKey.B].ReadKey();

            if (!escape)
                return;

            Animator anim = state.sm.character.animatorManager.anim;

            

            if(!state.sm.character.lockManager.isLock)
            {
                if (input.Dmag > jabLimit)
                {
                    anim.SetFloat(AnimatorParam.Forward.ToString(), 1);
                    anim.SetFloat(AnimatorParam.Right.ToString(), 0);
                }
                else
                {
                    anim.SetFloat(AnimatorParam.Forward.ToString(), 0);
                    anim.SetFloat(AnimatorParam.Right.ToString(), 0);
                }
            }
            else
            {
                Vector3 v = input.Dvec;
                v.y = 0;
                if(v.magnitude<0.1f)
                {
                    anim.SetFloat(AnimatorParam.Forward.ToString(), 0);
                    anim.SetFloat(AnimatorParam.Right.ToString(), 0);
                    return;
                }

                v.Normalize();
                var model = state.sm.character.model;
                var res1 = Vector3.Angle(model.forward, v);
                var res2 = Vector3.Angle(model.right, v);

                if(res1<=45)
                {
                    anim.SetFloat(AnimatorParam.Right.ToString(), 0);
                    anim.SetFloat(AnimatorParam.Forward.ToString(), 1);
                }
                else if(res1>=135)
                {
                    anim.SetFloat(AnimatorParam.Right.ToString(), 0);
                    anim.SetFloat(AnimatorParam.Forward.ToString(), -1);
                }
                else
                {
                    if(res2<=45)
                    {
                        anim.SetFloat(AnimatorParam.Right.ToString(), 1);
                        anim.SetFloat(AnimatorParam.Forward.ToString(), 0);
                    }
                    else
                    {
                        anim.SetFloat(AnimatorParam.Right.ToString(), -1);
                        anim.SetFloat(AnimatorParam.Forward.ToString(), 0);
                    }
                }
            }

            anim.SetInteger(AnimatorParam.Trans.ToString(), transIndex.GetHashCode());
        }
    }
}

