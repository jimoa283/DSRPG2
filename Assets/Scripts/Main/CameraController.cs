using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DS
{
    public class CameraController : MonoBehaviour
    {
        [Header("镜头移动设置")]
        public float moveSpeedX = 30;
        public float moveSpeedY = 10;
        public float Ymax = 45f;
        public float Ymin = -30f;
        public float x = 0;
        public float y = 0;

        [Header("相机参数设置")]
        public Transform camHolder;
        public Transform cameraPivit;
        public Transform _camera;
        public float fallowRate = 0.1f;
        public float rotationRate = 0.5f;
        public Vector3 followOffset = Vector3.zero;
        public LayerMask avoidLayer;
        public float maxDistance;

        [Header("锁定")]
        private LockManager lockManager;
        public Image lockImage;
        public Vector3 lockOffset = new Vector3(0, 0.5f, 0);
        public float lockSmoothSpeed = 20f;

        public CharacterController character;

        public void Init(CharacterController character)
        {
            this.character = character;
            _camera = Camera.main.transform;
            camHolder.position = cameraPivit.position + followOffset;
            _camera.localPosition = new Vector3(0, 0, -maxDistance);
            x = character.model.transform.rotation.eulerAngles.y;
            lockManager = character.lockManager;
        }

        private void LateUpdate()
        {
            UpdateCameraHolderPosition();
            UpdateCameraPosition();
            UpateCameraRotation(Time.fixedDeltaTime);
        }


        private void UpdateCameraHolderPosition()
        {
            var pos = Vector3.Lerp(camHolder.position, cameraPivit.position + followOffset+lockOffset*(lockManager.isLock?1:0), fallowRate);
            camHolder.transform.position = pos;
        }

        private void UpdateCameraPosition()
        {
            float tempD = maxDistance;
            Vector3 dir = _camera.position - camHolder.position;
            dir.Normalize();
            if (Physics.Raycast(camHolder.position, dir, out RaycastHit hit, maxDistance, avoidLayer))   //对于摄像机与人物之间有遮挡的处理
            {
                var p = hit.point;
                tempD = Vector3.Distance(p, camHolder.position);
                tempD = tempD - 0.2f > 0 ? (tempD - 0.2f) : 0.2f;
            }

            _camera.localPosition = new Vector3(0, 0, -tempD);
        }

        private void UpateCameraRotation(float deltaTime)
        {
            if(!lockManager.isLock)
            {
                HideLockUI();
                float inputX = character.input.Jright;
                float inputY = character.input.Jup;
                x += inputX * moveSpeedX * deltaTime;
                y -= inputY * moveSpeedY * deltaTime;
                y = Mathf.Clamp(y, Ymin, Ymax);
                Quaternion q = Quaternion.Euler(y, x, 0);
                camHolder.transform.rotation = Quaternion.Slerp(camHolder.transform.rotation, q, rotationRate);
            }
            else
            {
                SetLockUI(lockManager.targetPos);
                var dir = lockManager.targetPos - camHolder.position;
                dir.Normalize();
                Quaternion q = Quaternion.LookRotation(dir);
                var rotation = Quaternion.Slerp(camHolder.transform.rotation, q, lockSmoothSpeed * deltaTime);
                camHolder.transform.rotation = rotation;
                var e = camHolder.transform.rotation.eulerAngles;
                x = e.y;
                y = e.x;
                lockManager.lastTargetPos = lockManager.targetPos;
            }
        }

        private void SetLockUI(Vector3 targetPos)
        {
            if (lockImage == null)
                return;

           lockImage.gameObject.SetActive(true);
            Vector3 pos = Camera.main.WorldToScreenPoint(targetPos);
            var rect = lockImage.transform as RectTransform;
            rect.position = pos;
        }

        private void HideLockUI()
        {
            if (lockImage == null)
                return;

            lockImage.gameObject.SetActive(false);
        }
    }
}

