using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    [CreateAssetMenu(fileName ="FaceAction",menuName ="FSM/Base/Locomotion/FaceAction")]
    public class FaceAction : UpdateAction
    {
        public float turnSpeed;

        public override void Excute(State state)
        {
            Transform model = state.sm.character.model.transform;
            Vector3 v = state.sm.character.input.Dvec;
            if (v.magnitude>0.01f)
            {
                if(!state.sm.character.lockManager.isLock)
                {
                    v.Normalize();
                    Quaternion q = Quaternion.LookRotation(v);
                    model.rotation = Quaternion.Slerp(model.rotation, q, turnSpeed * Time.deltaTime);
                }
                else
                {
                    var dir = state.sm.character.lockManager.target.position - state.sm.character.transform.position;
                    dir.y = 0;
                    dir.Normalize();
                    dir *= state.sm.character.isMirror;
                    Quaternion target = Quaternion.LookRotation(dir);
                    model.rotation = Quaternion.Slerp(model.rotation, target, turnSpeed * Time.deltaTime);
                }
            }
        }
    }
}

