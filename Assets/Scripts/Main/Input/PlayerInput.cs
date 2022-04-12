using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    public class PlayerInput : InputBase
    {
        public string BKey = "left shift";
        public string RKey = "r";
        public string DHorizontal = "Horizontal";
        public string DVertical = "Vertical";
        public string JHorizontal = "Mouse X";
        public string JVertical = "Mouse Y";
        public string RBKey = "mouse 0";
        public string RTKey = "mouse 1";
        public string LBKey = "mouse 3";
        public string LTKey = "mouse 4";
        public string YKey;
        public string XKey;
        public string AKey;
        public string MenuKey;
        public string walkCheckKey = "alt";

        private MyButton BBtn = new MyButton();
        private MyButton RBtn = new MyButton();
        private MyButton RBBtn = new MyButton();
        private MyButton RTBtn = new MyButton();
        private MyButton LBBtn = new MyButton();
        private MyButton LTBtn = new MyButton();
        private MyButton YBtn = new MyButton();
        private MyButton XBtn = new MyButton();
        private MyButton ABtn = new MyButton();
        private MyButton MenuBtn = new MyButton();
        private MyButton WalkCheckBtn = new MyButton();
        private MyButton LeftBtn = new MyButton();
        private MyButton RightBtn = new MyButton();
        private MyButton UpBtn = new MyButton();
        private MyButton DownButton = new MyButton();
        public float sampleGap = 0.1f;
        private float timer = 0;
        float[] DHInputBuffer = new float[3];
        float[] DVInputBuffer = new float[3];
        int DInputIndex = 0;

        protected override void InputExcute()
        {
            BBtn.Tick(Input.GetKey(BKey));
            RBtn.Tick(Input.GetKey(RKey));
            RBBtn.Tick(Input.GetKey(RBKey));
            RTBtn.Tick(Input.GetKey(RTKey));
            LBBtn.Tick(Input.GetKey(LBKey));
            LTBtn.Tick(Input.GetKey(LTKey));
            YBtn.Tick(Input.GetKey(YKey));
            //ABtn.Tick(Input.GetKey(AKey));
           // XBtn.Tick(Input.GetKey(XKey));
            //MenuBtn.Tick(Input.GetKey(MenuKey));
            WalkCheckBtn.Tick(Input.GetKey(walkCheckKey));

            timer += Time.deltaTime;
            if(timer>sampleGap)
            {
                timer = 0;
                DInputIndex=(++DInputIndex)%3;

                var dh = Input.GetAxis(DHorizontal);
                var dv = Input.GetAxis(DVertical);

                DHInputBuffer[DInputIndex] = dh;
                DVInputBuffer[DInputIndex] = dv;
            }

            var averagerH = (DHInputBuffer[0] + DHInputBuffer[1] + DHInputBuffer[2]) / 3;
            var averageV = (DVInputBuffer[0] + DVInputBuffer[1] + DVInputBuffer[2]) / 3;

            Dup = Mathf.SmoothDamp(Dup, averageV,ref velocityDup, 0.1f);
            Dright = Mathf.SmoothDamp(Dright, averagerH, ref velocityDright, 0.1f);

            Jup = Input.GetAxis(JVertical);
            Jright = Input.GetAxisRaw(JHorizontal);

            // Run = (BBtn.isPressing && !BBtn.isDelaying) || BBtn.isExtending;

            Run = BBtn.LongPressCheck(0.5f);

            Escape = BBtn.onReleased && BBtn.isDelaying;
            Lock = RBtn.onPressed;
            //RightSelect = false;

            RBAttack = RBBtn.onPressed;
            RTAttack = RTBtn.onPressed;
            LBAttack = LBBtn.onPressed;
            LTAttack = LTBtn.onPressed;
            Y = YBtn.onPressed;
            X = XBtn.onPressed;
            A = ABtn.onPressed;
            Menu = MenuBtn.onPressed;
            Left = LeftBtn.onPressed;
            Right = RightBtn.onPressed;
            Up = UpBtn.onPressed;
            Down = DownButton.onPressed;
            Shield = LBBtn.isPressing;
            WalkCheck = WalkCheckBtn.isPressing;

        }
    }
}


