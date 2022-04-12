using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    public enum InputBufferKey
    {
        RB,
        RT,
        LB,
        LT,
        Y,
        Left,
        Right,
        Up,
        Down,
        B,
        A,
        X,
        Menu,
        WalkCheck
    }

    public enum AttackType
    {
        RB=1,
        RT=2
    }

    public enum AnimatorParam
    {
        Speed,
        Lock,
        Forward,
        Right,
        Shield,
        Trans,
        ChangeAttackType,
        BothHand,
        Ban,
        ChangeTrans,
        HitLevel,
        HitDirection
    }

    public enum HitDirection
    {
        Forward=1,
        Back=2,
        Left=3,
        Right=4
    }

    public enum TransIndex
    {
        Locomotion = 1,
        RBAttack = 2,
        RTAttack = 3,
        LTAttack = 4,
        Hit = 5,
        Escape = 6,
        DeathAttack = 7,
        DeathHit = 8,//目前没有使用
        ShieldHit = 9,
        ShieldHitBack = 10,
        Death = 11,
        Run2Stop = 12,
        Turn180 = 13,
        FallLoop = 14,
        FallLand = 15,
        CounterBack=16,
        BeCounterBack=17
    }

    public enum StateDataType
    {
        Health,
        Endurance,
        Absorbe
    }

    public enum WeaponType
    {
        LongSword=1,
        GreatSword=2,
        Shield=1
    }

    public enum LocomotionType
    {
        LongSword,
        GreatSword,
        Shield,
    }

    public enum AnimationType
    {
        Idle,
        WalkF,
        WalkB,
        WalkL,
        WalkR,
        RunF,
        RunB,
        RunL,
        RunR,
        DashRun,
        ShieldIdle2,
        ShieldWalk2,
        ShieldRun2,
        ShieldDashRun2,
        EscapeF,
        EscapeB,
        EscapeL,
        EscapeR,
        Jab,
        DashRun2Stop,
        Turn180,
        FallLoop,
        FallLand,
        ShieldHit,
        HardShieldHit
    }
}

