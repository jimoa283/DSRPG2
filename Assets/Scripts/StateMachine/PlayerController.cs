using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    public class PlayerController : CharacterController
    {
        public static PlayerController Instance { get; private set; }

        public CameraController cameraController;

        protected override void ComponentInit()
        {
            base.ComponentInit();
            cameraController.Init(this);            
        }

        protected override void GetComponentForCC()
        {          
            base.GetComponentForCC();
            cameraController = GetComponent<CameraController>();
            refrenceDir = Camera.main.transform;
        }

        protected override void Init()
        {
            Instance = this;
            base.Init();
        }
    }
}

