using UnityEngine;

namespace Develop._3.EnemyDestroyer
{
    public class Raycaster
    {
        public bool CastRayFromCamera(Vector3 mousePosition, LayerMask raycastMask, out RaycastHit raycastHit)
        {
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            bool isSuccess = Physics.Raycast(ray, out raycastHit, Mathf.Infinity, raycastMask.value);
            return isSuccess;
        }

    }
}
