using System;
using System.Collections.Generic;

namespace Develop._3.EnemyDestroyer
{
    public interface IDestroyable : ICounterCondition, ILifetimeCondition, IDeadCondition
    {
        List<Func<bool>> DestroyConditions { get; }
        void AddDestroyCondition(Func<bool> condition);
        void Destroy();
    }
}
