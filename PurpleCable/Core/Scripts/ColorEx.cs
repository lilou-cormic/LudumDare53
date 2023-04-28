using Godot;
/// <summary>
/// FROM UNITY
/// </summary>
public static class ColorEx
{
    /// <summary>
    /// Linearly interpolates between colors a and b by t. (clamps t between 0 and 1)
    /// </summary>
    /// <param name="a">Start value, returned when t = 0.</param>
    /// <param name="b">End value, returned when t = 1.</param>
    /// <param name="t">Value used to interpolate between a and b.</param>
    /// <returns>Interpolated value, equals to a + (b - a) * t.</returns>
    public static Color Lerp(Color a, Color b, float t)
    {
        t = Mathf.Clamp(t, 0f, 1f);
        return new Color(a.R + (b.R - a.R) * t, a.G + (b.G - a.G) * t, a.B + (b.B - a.B) * t, a.A + (b.A - a.A) * t);
    }

    /// <summary>
    /// Linearly interpolates between colors a and b by t. (t is not clamped)
    /// </summary>
    /// <param name="a">Start value, returned when t = 0.</param>
    /// <param name="b">End value, returned when t = 1.</param>
    /// <param name="t">Value used to interpolate between a and b.</param>
    /// <returns>Interpolated value, equals to a + (b - a) * t.</returns>
    public static Color LerpUnclamped(Color a, Color b, float t)
    {
        return new Color(a.R + (b.R - a.R) * t, a.G + (b.G - a.G) * t, a.B + (b.B - a.B) * t, a.A + (b.A - a.A) * t);
    }
}
