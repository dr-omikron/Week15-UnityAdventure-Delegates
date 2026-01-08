using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Develop._3.EnemyDestroyer
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private Image _isDeathImage;
        [SerializeField] private TMP_Text _lifeTimeText;
        [SerializeField] private TMP_Text _countText;
        [SerializeField] private float _selectOutlineThickness;

        private Material _material;
        private SelectOutliner _selectOutliner;

        private void Start()
        {
            _material = GetComponent<Renderer>().materials[1];
            _selectOutliner = new SelectOutliner(_material, _selectOutlineThickness);

            _healthBar.SetHealthPercent(1);

            _enemy.HealthChanged += OnHealthChanged;
            _enemy.Selected += OnSelected;
            _enemy.Deselected += OnDeselected;
        }

        private void OnDestroy()
        {
            _enemy.HealthChanged -= OnHealthChanged;
            _enemy.Selected -= OnSelected;
            _enemy.Deselected -= OnDeselected;
        }

        private void OnSelected() => _selectOutliner.SwitchOnOutline();
        private void OnDeselected() => _selectOutliner.SwitchOffOutline();

        private void OnHealthChanged(float health, float maxHealth) => _healthBar.SetHealthPercent(health / maxHealth);
    }
}
