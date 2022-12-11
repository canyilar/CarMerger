using UnityEngine;

namespace CarMerger
{
    public static class Extensions
    {
        public static bool IsSameWith(this Vector3 vector, Vector3 other)
        {
            return Mathf.Approximately(vector.x, other.x) && Mathf.Approximately(vector.y, other.y) && Mathf.Approximately(vector.z, other.z);
        }
    }
}