
namespace Develop._3.EnemyDestroyer
{
    public interface ILifetimeCondition
    {
        float LifeTime { get; }
        void IsLifeTimeConditionActivated(float lifeTime, float maxLifeTime);
    }
}
