using Godot;

namespace PurpleCable
{
    /// <summary>
    /// Sound player [MonoBehaviour] - Requires AudioSource
    /// </summary>
    //[RequireComponent(typeof(AudioSource))]
    public partial class SoundPlayer : Singleton<SoundPlayer>
    {
        #region Components

        /// <summary>
        /// Audio source
        /// </summary>
        private static AudioStreamPlayer2D _audioSource;

        #endregion

        #region Properties

        /// <summary>
        /// Volume
        /// </summary>
        public static float Volume
        {
            get => PlayerPrefs.GetFloat("SoundVolume", 0.5f);

            set => PlayerPrefs.SetFloat("SoundVolume", value);
        }

        #endregion

        #region Initialization

        public SoundPlayer()
        { }

        #endregion

        #region Functions

        public static void SetAudioSource(AudioStreamPlayer2D audioSource)
        {
            _audioSource = audioSource;

            // Get saved sfx volume
            _audioSource.VolumeDb = Volume;
        }

        /// <summary>
        /// Plays the audio clip though the audio source
        /// </summary>
        /// <param name="clip">The audio clip (can be null)</param>
        /// <param name="pitch">The change in pitch (can be null or from 1 to 10) [null by default]</param>
        public static void Play(AudioStream clip, int? pitch = null)
        {
            if (clip != null && _audioSource != null)
            {
                _audioSource.Stream = clip;

                if (pitch.HasValue)
                    _audioSource.PitchScale = 1 + (pitch.Value / 10f);
                else
                    _audioSource.PitchScale = 1;

                _audioSource.Play();
            }
        }

        #endregion
    }
}
