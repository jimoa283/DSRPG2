using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    [CreateAssetMenu(fileName ="locomotionDataBase",menuName ="Locomotion/LocomotionDataBase")]
    public class LocomotionDataBase : ScriptableObject
    {
        public List<LocomotionAsset> locomotionAssets;
    }
}

