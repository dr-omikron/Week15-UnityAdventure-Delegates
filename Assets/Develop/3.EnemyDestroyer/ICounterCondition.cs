
namespace Develop._3.EnemyDestroyer
{
    public interface ICounterCondition
    {
        int Counter { get; }
        void AddCounter(int count, int maxCount);
    }
}
