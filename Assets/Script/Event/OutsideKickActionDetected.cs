namespace Project.Event
{
    public class OutsideKickActionDetected
    {
        public int ballSteppingActionCountData;
        public FootLevels nextFootLevels;

        public OutsideKickActionDetected(int _ballSteppingActionCountData, FootLevels _nextFootLevels)
        {
            ballSteppingActionCountData = _ballSteppingActionCountData;
            nextFootLevels = _nextFootLevels;
        }
    }
}