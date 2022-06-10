namespace Project.Event
{
    public class OutsideKickActionDetected
    {
        public int ballSteppingActionCountData;

        public OutsideKickActionDetected(int _ballSteppingActionCountData)
        {
            ballSteppingActionCountData = _ballSteppingActionCountData;
        }
    }
}