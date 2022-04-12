using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    public class LockManager : MonoBehaviour
    {
        public CharacterController character;
        public Vector3 halfBox = new Vector3(1, 1, 1.5f);
        public LayerMask targetLayer;
        public float lockLimitDistance = 10f;
        public bool isLock;
        public Transform target;
        public Transform model;
        public InputBase input;
        public Vector3 lastTargetPos;
        public Vector3 targetPos;

        public void Init(CharacterController character)
        {
            this.character = character;
            input = character.input;
            model = character.model;
        }

        private void Update()
        {
            if(input.Lock)
            {
                if (isLock)
                    UnLock();
                else
                    Lock();
            }

            if(isLock)
            {
                targetPos = lastTargetPos * 0.45f + targetPos * 0.45f + target.position*0.1f;
            }
        }

        public void UnLock()
        {
            target = null;
            isLock = false;
        }

        public void Lock()
        {
            Vector3 ori = model.position;
            Vector3 center = ori + model.forward*character.isMirror * halfBox.z + model.up * halfBox.y;
            var cs = Physics.OverlapBox(center, halfBox, model.rotation, targetLayer);

            float minDis=float.MaxValue;
            Transform targetTemp = null;

            foreach(var c in cs)
            {
                var d = Vector3.Distance(model.position, c.transform.position);
                if(d<minDis)
                {
                    minDis = d;
                    targetTemp = c.transform;
                }
            }

            if(targetTemp!=null)
            {
                if(targetTemp.CompareTag("Enemy"))
                    targetTemp.GetComponent<EnemyStateManager>().ShowHealthBar();
                Transform lockPoint = targetTemp.Find("LockPoint");
                target = lockPoint == null ? targetTemp: lockPoint;
                lastTargetPos = targetPos = target.transform.position;
                isLock = true;
            }
        }
    }
}

