using Godot;

namespace PurpleCable
{
    public partial class SoundPlayerBehaviour : AudioStreamPlayer2D
    {
        #region Initialization

        public SoundPlayerBehaviour()
        {
            VolumeDb = SoundPlayer.Volume;
        }

        public override void _Ready()
        {
            VolumeDb = SoundPlayer.Volume;
        }

        #endregion
    }
}
