using Godot;

/// <summary>
/// Extensions for AudioStreams
/// </summary>
public static class AudioStreamExtensions
{
    #region Play
    /// <summary>
    /// Plays the audio clip using the SoundPlayer
    /// </summary>
    /// <param name="audioClip">Audio clip (can be null)</param>
    /// <param name="pitch">The change in pitch (can be null or from 1 to 10) [null by default]</param>
    public static void Play(this AudioStream audioClip, int? pitch = null)
        => PurpleCable.SoundPlayer.Play(audioClip, pitch);

    public static void Play(this AudioStreamPlayer2D soundPlayer, AudioStream clip)
    {
        soundPlayer.Stream = clip;
        soundPlayer.Play();
    }
    #endregion

    #region PlayRandomPitch
    /// <summary>
    /// Plays the audio clip using the SoundPlayer with a random pitch
    /// </summary>
    /// <param name="audioClip">Audio clip (can be null)</param>
    public static void PlayRandomPitch(this AudioStream audioClip)
        => PurpleCable.SoundPlayer.Play(audioClip, Random.Range(1, 10));
    #endregion
}
