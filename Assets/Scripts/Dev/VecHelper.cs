using UnityEngine;
using System.Collections;

namespace DevCustom{
    public class VecHelper : MonoBehaviour {

        public static bool EqualsVec4(Vector4 v1, Vector4 v2)
        {
            if (v1 == null)
                return false;
            if (v2 == null)
                return false;
            return v1.x == v2.x && v1.y == v2.y && v1.z == v2.z && v1.w == v2.w;
        }
    }
}
