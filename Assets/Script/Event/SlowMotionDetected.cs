namespace Project.Event
{
    public class SlowMotionDetected
    {
        public float slowDownMultiplier;

        public SlowMotionDetected(float _slowDownMultiplier)
        {
            slowDownMultiplier = _slowDownMultiplier;
        }
    }
}