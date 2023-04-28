using Godot;
using System;

/// <summary>
/// FROM UNITY
/// </summary>
public static class VectorEx
{
    public static Vector3 ToVector3(this Vector2 vector)
    {
        return new Vector3(vector.X, vector.Y, 0);
    }

    public static Vector2 ToVector2(this Vector3 vector)
    {
        return new Vector2(vector.X, vector.Y);
    }

    public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, double smoothTime, double deltaTime, float maxSpeed = float.PositiveInfinity)
    {
        Vector3 currentVelocity3 = currentVelocity.ToVector3();

        Vector3 outVector3 = SmoothDamp(current.ToVector3(), target.ToVector3(), ref currentVelocity3, smoothTime, deltaTime, maxSpeed);

        currentVelocity = currentVelocity3.ToVector2();

        return outVector3.ToVector2();
    }

    public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, double smoothTime, double deltaTime, float maxSpeed = float.PositiveInfinity)
    {
        float soomthTimeF = (float)smoothTime;
        float deltaTimeF = (float)deltaTime;

        float num = 0f;
        float num2 = 0f;
        float num3 = 0f;
        soomthTimeF = Mathf.Max(0.0001f, soomthTimeF);
        float num4 = 2f / soomthTimeF;
        float num5 = num4 * deltaTimeF;
        float num6 = 1f / (1f + num5 + 0.48f * num5 * num5 + 0.235f * num5 * num5 * num5);
        float num7 = current.X - target.X;
        float num8 = current.Y - target.Y;
        float num9 = current.Z - target.Z;
        Vector3 vector = target;
        float num10 = maxSpeed * soomthTimeF;
        float num11 = num10 * num10;
        float num12 = num7 * num7 + num8 * num8 + num9 * num9;
        if (num12 > num11)
        {
            float num13 = (float)Math.Sqrt(num12);
            num7 = num7 / num13 * num10;
            num8 = num8 / num13 * num10;
            num9 = num9 / num13 * num10;
        }

        target.X = current.X - num7;
        target.Y = current.Y - num8;
        target.Z = current.Z - num9;
        float num14 = (currentVelocity.X + num4 * num7) * deltaTimeF;
        float num15 = (currentVelocity.Y + num4 * num8) * deltaTimeF;
        float num16 = (currentVelocity.Z + num4 * num9) * deltaTimeF;
        currentVelocity.X = (currentVelocity.X - num4 * num14) * num6;
        currentVelocity.Y = (currentVelocity.Y - num4 * num15) * num6;
        currentVelocity.Z = (currentVelocity.Z - num4 * num16) * num6;
        num = target.X + (num7 + num14) * num6;
        num2 = target.Y + (num8 + num15) * num6;
        num3 = target.Z + (num9 + num16) * num6;
        float num17 = vector.X - current.X;
        float num18 = vector.Y - current.Y;
        float num19 = vector.Z - current.Z;
        float num20 = num - vector.X;
        float num21 = num2 - vector.Y;
        float num22 = num3 - vector.Z;
        if (num17 * num20 + num18 * num21 + num19 * num22 > 0f)
        {
            num = vector.X;
            num2 = vector.Y;
            num3 = vector.Z;
            currentVelocity.X = (num - vector.X) / deltaTimeF;
            currentVelocity.Y = (num2 - vector.Y) / deltaTimeF;
            currentVelocity.Z = (num3 - vector.Z) / deltaTimeF;
        }

        return new Vector3(num, num2, num3);
    }

    /// <summary>
    /// Linearly interpolates between two points. (clamps t between 0 and 1)
    /// </summary>
    /// <param name="a">Start value, returned when t = 0.</param>
    /// <param name="b">End value, returned when t = 1.</param>
    /// <param name="t">Value used to interpolate between a and b.</param>
    /// <returns>Interpolated value, equals to a + (b - a) * t.</returns>
    public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
        => Lerp(a.ToVector3(), b.ToVector3(), t).ToVector2();

    /// <summary>
    /// Linearly interpolates between two vectors. (t is not clamped)
    /// </summary>
    /// <param name="a">Start value, returned when t = 0.</param>
    /// <param name="b">End value, returned when t = 1.</param>
    /// <param name="t">Value used to interpolate between a and b.</param>
    /// <returns>Interpolated value, equals to a + (b - a) * t.</returns>
    public static Vector2 LerpUnclamped(Vector2 a, Vector2 b, float t)
        => LerpUnclamped(a.ToVector3(), b.ToVector3(), t).ToVector2();

    /// <summary>
    /// Linearly interpolates between two points. (clamps t between 0 and 1)
    /// </summary>
    /// <param name="a">Start value, returned when t = 0.</param>
    /// <param name="b">End value, returned when t = 1.</param>
    /// <param name="t">Value used to interpolate between a and b.</param>
    /// <returns>Interpolated value, equals to a + (b - a) * t.</returns>
    public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
    {
        t = Mathf.Clamp(t, 0f, 1f);
        return new Vector3(a.X + (b.X - a.X) * t, a.Y + (b.Y - a.Y) * t, a.Z + (b.Z - a.Z) * t);
    }

    /// <summary>
    /// Linearly interpolates between two vectors. (t is not clamped)
    /// </summary>
    /// <param name="a">Start value, returned when t = 0.</param>
    /// <param name="b">End value, returned when t = 1.</param>
    /// <param name="t">Value used to interpolate between a and b.</param>
    /// <returns>Interpolated value, equals to a + (b - a) * t.</returns>
    public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, float t)
    {
        return new Vector3(a.X + (b.X - a.X) * t, a.Y + (b.Y - a.Y) * t, a.Z + (b.Z - a.Z) * t);
    }

    public static Vector2 PolarToCartesian(float radius, float theta)
    {
        return new Vector2(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta));
    }
}
