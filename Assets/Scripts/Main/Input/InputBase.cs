using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    public class InputBase : MonoBehaviour
    {
        public CharacterController character;

        private float _Dup;
        private float _Dright;
        public float Dmag;
        public Vector3 Dvec;

        protected float velocityDup;
        protected float velocityDright;

        private float _Jup;
        private float _Jright;

        private bool _Y;
        private bool _B;
        private bool _A;
        private bool _X;
        private bool _Menu;
        private bool _WalkCheck;

        private bool _Lock;
        private bool _RightSelect;
        private bool _LeftSelect;
        private bool _RBAttack;
        private bool _RTAttack;
        private bool _LBAttack;
        private bool _LTAttack;

        private bool _Left;
        private bool _Right;
        private bool _Up;
        private bool _Down;

        private bool _Run;
        private bool _Shield;

        public bool forbind = false;

        //public bool isMirror;

        public Dictionary<InputBufferKey, InputBuffer> inputBufferDic = new Dictionary<InputBufferKey, InputBuffer>();
        private InputBuffer RBBuffer = new InputBuffer(0.2f);
        private InputBuffer RTBuffer = new InputBuffer(0.2f);
        private InputBuffer LBBuffer = new InputBuffer(0.2f);
        private InputBuffer LTBuffer = new InputBuffer(0.2f);

        private InputBuffer YBuffer = new InputBuffer(1f); //单双手切换缓存
        private InputBuffer ABuffer = new InputBuffer(0.2f);
        private InputBuffer XBuffer = new InputBuffer(0.2f);
        private InputBuffer BBuffer = new InputBuffer(0.2f);
        private InputBuffer MenuBuffer = new InputBuffer(0.2f);
        private InputBuffer WalkCheckBuffer = new InputBuffer(0.2f);

        private InputBuffer LeftBuffer = new InputBuffer(1f);
        private InputBuffer RightBuffer = new InputBuffer(1f);
        private InputBuffer UpBuffer = new InputBuffer(1f);
        private InputBuffer DownBuffer = new InputBuffer(1f);

        public virtual void Init(CharacterController character)
        {
            this.character = character;
            inputBufferDic.Add(InputBufferKey.RB, RBBuffer);
            inputBufferDic.Add(InputBufferKey.RT, RTBuffer);
            inputBufferDic.Add(InputBufferKey.LB, LBBuffer);
            inputBufferDic.Add(InputBufferKey.LT, LTBuffer);
            inputBufferDic.Add(InputBufferKey.Y, YBuffer);
            inputBufferDic.Add(InputBufferKey.B, BBuffer);
            inputBufferDic.Add(InputBufferKey.A, ABuffer);
            inputBufferDic.Add(InputBufferKey.X, XBuffer);
            inputBufferDic.Add(InputBufferKey.Menu, MenuBuffer);
            inputBufferDic.Add(InputBufferKey.WalkCheck, WalkCheckBuffer);
            inputBufferDic.Add(InputBufferKey.Left, LeftBuffer);
            inputBufferDic.Add(InputBufferKey.Right, RightBuffer);
            inputBufferDic.Add(InputBufferKey.Up, UpBuffer);
            inputBufferDic.Add(InputBufferKey.Down, DownBuffer);
        }

        public float Dup{ get { return forbind ? 0 : _Dup; } protected set { _Dup = value; }}
        public float Dright{ get { return forbind ? 0 : _Dright; } protected set { _Dright = value; } }

        //public float Dmag { get { return Mathf.Sqrt(Dup * Dup + Dright * Dright); } }
        public float Jup{ get { return forbind ? 0 : _Jup; } protected set { _Jup = value; } }
        public float Jright{get { return forbind ? 0 : _Jright; } protected set { _Jright = value; } }
        public bool Run { get { return forbind ? false : _Run; } protected set { _Run = value; } }
        public bool Escape {get { return forbind ? false : _B; } protected set { _B = value; } }
        public bool Lock { get { return forbind ? false : _Lock; } protected set { _Lock = value; } }
        public bool RightSelect { get { return forbind ? false : _RightSelect; } protected set { _RightSelect = value; } }
        public bool LeftSelect { get { return forbind ? false : _LeftSelect; } protected set { _LeftSelect = value; } }
        public bool RBAttack { get { return forbind ? false : _RBAttack; } protected set { _RBAttack = value; } }
        public bool RTAttack { get { return forbind ? false : _RTAttack; } protected set { _RTAttack = value; } }
        public bool LBAttack { get { return forbind ? false : _LBAttack; } protected set { _LBAttack = value; } }
        public bool LTAttack { get { return forbind ? false : _LTAttack; } protected set {_LTAttack = value; } }
        public bool Y { get { return forbind ? false : _Y; } protected set { _Y = value; } }
        public bool A { get { return forbind ? false : _A; } protected set { _A = value; } }
        public bool X { get { return forbind ? false : _X; } protected set { _X = value; } }
        public bool Menu { get { return forbind ? false : _Menu; } protected set { _Menu = value; } }

        public bool WalkCheck { get { return forbind ? false : _WalkCheck; }protected set { _WalkCheck = value; } }
        public bool Left { get { return forbind ? false : _Left; } protected set { _Left = value; } }
        public bool Right { get { return forbind ? false : _Right; } protected set {  _Right = value; } }
        public bool Up { get { return forbind ? false : _Up; } protected set { _Up = value; } }
        public bool Down { get { return forbind ? false : _Down; } protected set {  _Down = value; } }
        public bool Shield { get { return forbind ? false : _Shield; } protected set { _Shield = value; } }


        protected virtual void InputExcute()
        {

        }

        private void Update()
        {
            InputExcute();
            UpdateDmagDvec();
            UpdateInputBuffers();
        }

        private void UpdateDmagDvec()
        {
            Vector2 tempD = SquareToCircle(Dright, Dup);

            Dmag = tempD.magnitude;

            Vector3 forward = character.refrenceDir.forward*character.isMirror;
            forward.y = 0;
            forward.Normalize();
            Vector3 right = character.refrenceDir.right*character.isMirror;
            right.y = 0;
            right.Normalize();

            Dvec = forward * tempD.y + right * tempD.x;
        }

        private Vector2 SquareToCircle(float right,float up)
        {
            Vector2 output = Vector2.zero;

            output.x = right * Mathf.Sqrt(1 - (up * up) / 2f);
            output.y = up * Mathf.Sqrt(1 - (right * right) / 2f);

            return output;
        }

        private void UpdateOneBuffer(float deltaTime,InputBuffer buffer,bool check)
        {
            buffer.Update(deltaTime);
            if (check)
                buffer.AddKey();
        }

        public bool ReadKey(InputBufferKey key)
        {
            return inputBufferDic[key].ReadKey();
        }

        private void UpdateInputBuffers()
        {
            float deltaTime = Time.deltaTime;

            UpdateOneBuffer(deltaTime, RBBuffer, RBAttack);
            UpdateOneBuffer(deltaTime, RTBuffer, RTAttack);
            UpdateOneBuffer(deltaTime, LBBuffer, LBAttack);
            UpdateOneBuffer(deltaTime, LTBuffer, LTAttack);
            UpdateOneBuffer(deltaTime, YBuffer, Y);
            UpdateOneBuffer(deltaTime, ABuffer, A);
            UpdateOneBuffer(deltaTime, XBuffer, X);
            UpdateOneBuffer(deltaTime, MenuBuffer, Menu);
            UpdateOneBuffer(deltaTime, LeftBuffer, Left);
            UpdateOneBuffer(deltaTime, RightBuffer, Right);
            UpdateOneBuffer(deltaTime, UpBuffer, Up);
            UpdateOneBuffer(deltaTime, DownBuffer, Down);
            UpdateOneBuffer(deltaTime, BBuffer, Escape);
        }
    }
}

