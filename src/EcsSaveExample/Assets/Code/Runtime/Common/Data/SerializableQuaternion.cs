using System;
using UnityEngine;

namespace Code.Runtime.Common.Data
{
    [Serializable]
    public struct SerializableQuaternion
    {
        // ReSharper disable InconsistentNaming
        public float x;
        public float y;
        public float z;
        public float w;
        // ReSharper restore InconsistentNaming

        public SerializableQuaternion(float rX, float rY, float rZ, float rW)
        {
            x = rX;
            y = rY;
            z = rZ;
            w = rW;
        }

        public override string ToString() =>
            $"[{x}, {y}, {z}, {w}]";

        public static implicit operator Quaternion(SerializableQuaternion rValue) =>
            new(rValue.x, rValue.y, rValue.z, rValue.w);

        public static implicit operator SerializableQuaternion(Quaternion rValue) =>
            new(rValue.x, rValue.y, rValue.z, rValue.w);
    }
}