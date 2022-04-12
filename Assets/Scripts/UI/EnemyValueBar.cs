using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    public class EnemyValueBar : BaseValueBar
    {

        public override void Init()
        {
            base.Init();
        }

        private void Update()
        {
            Vector3 relativePos = transform.position - Camera.main.transform.position;
            relativePos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            transform.rotation = rotation;
        }
    }

}
