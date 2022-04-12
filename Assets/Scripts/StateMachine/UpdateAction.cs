using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    public abstract class UpdateAction : ScriptableObject, IAction
    {
        public abstract void Excute(State state);
    }
}

