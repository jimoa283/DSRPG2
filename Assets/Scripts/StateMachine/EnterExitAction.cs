using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    public abstract class EnterExitAction : ScriptableObject, IAction
    {
        public abstract void Excute(State state);
    }
}

