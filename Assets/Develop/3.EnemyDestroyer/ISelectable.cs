namespace Develop._3.EnemyDestroyer
{
    public interface ISelectable : IDamageable 
    {
        void Select();
        void Deselect();
        bool IsSelected();
    }
}
