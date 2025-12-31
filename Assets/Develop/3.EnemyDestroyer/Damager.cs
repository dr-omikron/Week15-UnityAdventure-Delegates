namespace Develop._3.EnemyDestroyer
{
    public class Damager
    {
        private readonly PlayerInput _playerInput;
        private readonly RaycastSelector _raycastSelector;
        private readonly int _damage;

        public Damager(PlayerInput playerInput, RaycastSelector raycastSelector, int damage)
        {
            _playerInput = playerInput;
            _raycastSelector = raycastSelector;
            _damage = damage;

            _playerInput.DamageTaken += OnDamaged;
        }

        public void Deinitialize() => _playerInput.DamageTaken -= OnDamaged;

        private void OnDamaged()
        {
            if(_raycastSelector.Selectable != null && _raycastSelector.Selectable.IsSelected())
                _raycastSelector.Selectable.TakeDamage(_damage);
        }
    }
}
