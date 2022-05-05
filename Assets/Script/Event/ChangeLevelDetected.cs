namespace Project.Event
{
    public class ChangeLevelDetected
    {
        public FootLevels currentFootLevels = FootLevels.Defult;

        public ChangeLevelDetected(FootLevels _currentFootLevels)
        {
            currentFootLevels = _currentFootLevels;
        }
    }
}