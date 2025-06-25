using System;
using UnityEngine;

namespace Code.Runtime.Common.Data
{
    [Serializable]
    public struct SerializableVector3
    {
        // ReSharper disable InconsistentNaming
        public float x;
        public float y;
        public float z;
        // ReSharper restore InconsistentNaming

        public SerializableVector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        
        public override string ToString() =>
            $"[{x}, {y}, {z}]";

        public static implicit operator Vector3(SerializableVector3 serializableVector) =>
            new(serializableVector.x, serializableVector.y, serializableVector.z);

        public static implicit operator SerializableVector3(Vector3 vector) =>
            new(vector.x, vector.y, vector.z);
    }
}