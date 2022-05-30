namespace Project.Event
{
    public class PlaySoundEffectDetected
    {
        public SoundEffect soundEffect;

        public PlaySoundEffectDetected(SoundEffect _soundEffect)
        {
            soundEffect = _soundEffect;
        }
    }
}