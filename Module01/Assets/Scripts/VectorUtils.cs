using UnityEngine;

public static class VectorUtils
{
    public static Vector3 ClampVector3(Vector3 value, Vector3 min, Vector3 max)
    {
        return new Vector3(
            Mathf.Clamp(value.x, min.x, max.x),
            Mathf.Clamp(value.y, min.y, max.y),
            Mathf.Clamp(value.z, min.z, max.z)
        );
    }

    public static Vector3 ClampVector3(Vector3 value, float min, float max)
    {
        return new Vector3(
            Mathf.Clamp(value.x, min, max),
            Mathf.Clamp(value.y, min, max),
            Mathf.Clamp(value.z, min, max)
        );
    }

    public static Vector3 AbsVector3(Vector3 v)
    {
        return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
    }

    public static Vector3 Vec3(float value)
    {
        return new Vector3(value, value, value);
    }
}
