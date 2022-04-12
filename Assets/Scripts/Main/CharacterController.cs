using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    public class CharacterController : MonoBehaviour
    {
        public AnimatorManager animatorManager;
        public Transform model;
        public InputBase input;
        public Rigidbody rigid;
        public StateMachine baseStateMachine;
        public StateMachine changeStateMachine;
        public AudioPlayer audioPlayer;
        public AttackManager attackManager;
        public WeaponManager weaponManager;
        public LockManager lockManager;
        public HitManager hitManager;
        public StateManager stateManager;
        public Transform refrenceDir;

        public Vector3 velocityBeforeTransition;

        public int isMirror;

        private void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            GetComponentForCC();
            ComponentInit();         
        }

        protected virtual void GetComponentForCC()
        {
            rigid = GetComponent<Rigidbody>();
            animatorManager = GetComponentInChildren<AnimatorManager>();
            input = GetComponent<InputBase>();
            StateMachine[] tempSM = GetComponents<StateMachine>();
            foreach(var sm in tempSM)
            {
                if (sm.layIndex == 0)
                    baseStateMachine = sm;
                else
                    changeStateMachine = sm;
            }
            //baseStateMachine = GetComponent<StateMachine>();
            audioPlayer = GetComponentInChildren<AudioPlayer>();
            attackManager = GetComponent<AttackManager>();
            weaponManager = GetComponent<WeaponManager>();
            lockManager = GetComponent<LockManager>();
            hitManager = GetComponent<HitManager>();
            stateManager = GetComponent<StateManager>();
            refrenceDir = model.transform;
        }

        protected virtual void ComponentInit()
        {
            animatorManager.Init(this);
            input.Init(this);
            weaponManager.Init(this);
            baseStateMachine.Init(this);
            changeStateMachine.Init(this);
            audioPlayer.Init(this);
            attackManager.Init(this);          
            lockManager.Init(this);
            hitManager.Init(this);
            stateManager.Init(this);
        }

        // Update is called once per frame
        void Update()
        {
            float deltaTime = Time.deltaTime;
            GroundDetect(deltaTime);
        }

        private void FixedUpdate()
        {
            if(animatorManager.anim.GetAnimatorTransitionInfo(0).normalizedTime<=0)
            {
                velocityBeforeTransition = rigid.velocity;
            }
        }

        [Header("地面检测")]
        public bool isGround = true;
        public LayerMask groundLayer;
        public Vector3 groundDetectBox = new Vector3(0.15f, 0.1f, 0.05f);
        public float airCheckTime = 0.2f;
        private float airTime = 0;
        public bool groundDebug = true;

        public void GroundDetect(float deltaTime)
        {
            if (Physics.OverlapBox(transform.position, groundDetectBox, model.rotation, groundLayer).Length > 0)
            {
                isGround = true;
                airTime = 0;
            }
            else
            {
                airTime += deltaTime;
                if (airTime >= airCheckTime)
                {
                    isGround = false;
                }
                else
                {
                    isGround = true;
                }
            }
        }
    }
}

