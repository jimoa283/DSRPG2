using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    public abstract class FixedUpdateAction : ScriptableObject, IAction
    {
        public abstract void Excute(State state);
    }
}

