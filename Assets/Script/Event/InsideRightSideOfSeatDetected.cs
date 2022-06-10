namespace Project.Event
{
    public class InsideRightSideOfSeatDetected
    {
        public int ballSteppingActionCountData;
        public FootLevels nextFootLevels;

        public InsideRightSideOfSeatDetected(int _ballSteppingActionCountData, FootLevels _nextFootLevels)
        {
            ballSteppingActionCountData = _ballSteppingActionCountData;
            nextFootLevels = _nextFootLevels;
        }
    }
}