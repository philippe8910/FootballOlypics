using UnityEngine;

namespace Project.Event
{
    public class PlaySoundEffectDetected
    {
        public SoundEffect soundEffect;

        public Vector3 playPosition;

        public PlaySoundEffectDetected(SoundEffect _soundEffect)
        {
            soundEffect = _soundEffect;
        }
        
        public PlaySoundEffectDetected(SoundEffect _soundEffect , Vector3 _playPosition)
        {
            soundEffect = _soundEffect;
            playPosition = _playPosition;
        }
    }
}