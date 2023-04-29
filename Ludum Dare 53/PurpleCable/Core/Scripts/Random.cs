using Godot;

public static class Random
{
    private static RandomNumberGenerator _randomNumberGenerator = new RandomNumberGenerator();

    /// <summary>
    /// Generates a pseudo-random float between 0.0 and 1.0 (inclusive).
    /// </summary>
    public static float value => _randomNumberGenerator.Randf();

    /// <summary>
    /// Generates a pseudo-random float between from and to (inclusive).
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static float Range(float from, float to) => _randomNumberGenerator.RandfRange(from, to);

    /// <summary>
    /// Generates a pseudo-random 32-bit signed integer between from (inclusive) and to (exclusive).
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static int Range(int from, int to) => _randomNumberGenerator.RandiRange(from, to - 1);
}