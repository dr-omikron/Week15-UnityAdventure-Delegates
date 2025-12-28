namespace Develop._2.Timer
{
    public interface IProgressUpdater
    {
        void UpdateProgress(float progress, float limit);
        void ResetProgress(float limit);
    }
}
