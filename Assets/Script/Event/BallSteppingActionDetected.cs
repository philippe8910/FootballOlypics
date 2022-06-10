namespace Project.Event
{
    public class BallSteppingActionDetected
    {
        public int ballSteppingActionCountData;
        public FootLevels nextFootLevels;

        public BallSteppingActionDetected(int _ballSteppingActionCountData, FootLevels _nextFootLevels)
        {
            ballSteppingActionCountData = _ballSteppingActionCountData;
            nextFootLevels = _nextFootLevels;
        }
    }
}