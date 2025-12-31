using UnityEngine;

namespace Develop._3.EnemyDestroyer
{
    public class SelectOutliner
    {
        private const string OutlineThicknessKey = "_OutlineThickness";
        private static readonly int OutlineThickness = Shader.PropertyToID(OutlineThicknessKey);

        private readonly Material _outlineMaterial;
        private readonly float _defaultOutlineThickness;

        public SelectOutliner(Material outlineMaterial, float defaultOutlineThickness)
        {
            _outlineMaterial = outlineMaterial;
            _defaultOutlineThickness = defaultOutlineThickness;
        }

        public void SwitchOnOutline() => _outlineMaterial.SetFloat(OutlineThickness, _defaultOutlineThickness);

        public void SwitchOffOutline() => _outlineMaterial.SetFloat(OutlineThickness, 0);
    }
}
