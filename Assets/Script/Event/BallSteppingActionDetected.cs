namespace Project.Event
{
    public class BallSteppingActionDetected
    {
        public int ballSteppingActionCountData;

        public BallSteppingActionDetected(int _ballSteppingActionCountData)
        {
            ballSteppingActionCountData = _ballSteppingActionCountData;
        }
    }
}