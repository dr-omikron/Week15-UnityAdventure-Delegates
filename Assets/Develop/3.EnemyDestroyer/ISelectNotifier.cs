using System;

namespace Develop._3.EnemyDestroyer
{
    public interface ISelectNotifier
    {
        event Action Selected;
        event Action Deselected;
    }
}
