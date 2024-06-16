using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Conf.Scripts
{
    [CreateAssetMenu(fileName = "CameraConfig", menuName = "Configs/CameraConfig")]
    public class CameraConfig : ScriptableObject
    {
        public float pLerp = 0.2f;
        public float rLerp = 0.1f;
    }
}
