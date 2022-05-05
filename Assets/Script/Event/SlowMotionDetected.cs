namespace Project.Event
{
    public class SlowMotionDetected
    {
        public bool isSlowmotion;

        public SlowMotionDetected(bool _isSlowmotion)
        {
            isSlowmotion = _isSlowmotion;
        }
    }
}