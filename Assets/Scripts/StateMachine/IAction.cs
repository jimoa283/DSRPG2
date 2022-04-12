using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    public interface IAction
    {
        void Excute(State state);
    }
}

