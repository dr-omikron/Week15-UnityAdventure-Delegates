using UnityEngine;

namespace Develop._3.EnemyDestroyer
{
    public class RaycastSelector
    {
        private readonly Raycaster _raycaster;
        private readonly PlayerInput _playerInput;
        private readonly LayerMask _selectionMask;

        public ISelectable Selectable { get; private set; }

        public RaycastSelector(Raycaster raycaster, PlayerInput playerInput, LayerMask selectionMask)
        {
            _raycaster = raycaster;
            _playerInput = playerInput;
            _selectionMask = selectionMask;

            _playerInput.RayCasted += OnRaycast;
        }

        private void OnRaycast()
        {
            if(Selectable != null)
            {
                Selectable.Deselect();
                Selectable = null;
            }

            if(_raycaster.CastRayFromCamera(_playerInput.MousePosition, _selectionMask, out RaycastHit hit))
            {
                Selectable = hit.collider.gameObject.GetComponent<ISelectable>();

                if (Selectable != null)
                    Selectable.Select();
            }
        }

        public void Deinitialize() => _playerInput.RayCasted -= OnRaycast;
    }
}
