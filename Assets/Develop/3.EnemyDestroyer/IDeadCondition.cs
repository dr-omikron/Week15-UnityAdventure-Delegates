
namespace Develop._3.EnemyDestroyer
{
    public interface IDeadCondition
    {
        bool IsDead { get; }
        void IsDeadConditionActivated();
    }
}
