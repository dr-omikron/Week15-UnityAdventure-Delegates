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

            _enemy.LifetimeChanged += OnLifeTimeChanged;
            _enemy.CounterChanged += OnCounterChanged;
            _enemy.DeadConditionActivated += OnDeadConditionActivated;
        }

        private void OnDestroy()
        {
            _enemy.HealthChanged -= OnHealthChanged;
            _enemy.Selected -= OnSelected;
            _enemy.Deselected -= OnDeselected;

            _enemy.LifetimeChanged -= OnLifeTimeChanged;
            _enemy.CounterChanged -= OnCounterChanged;
            _enemy.DeadConditionActivated -= OnDeadConditionActivated;
        }

        private void OnSelected() => _selectOutliner.SwitchOnOutline();
        private void OnDeselected() => _selectOutliner.SwitchOffOutline();

        private void OnCounterChanged(int counter, int maxCount)
        {
            if(_countText.gameObject.activeSelf == false)
                _countText.gameObject.SetActive(true);

            _countText.text = counter + "/" + maxCount;
        }

        private void OnLifeTimeChanged(float lifeTime, float maxLifeTime)
        {
            if(_lifeTimeText.gameObject.activeSelf == false)
                _lifeTimeText.gameObject.SetActive(true);

            _lifeTimeText.text = lifeTime.ToString("F2") + "/" + maxLifeTime.ToString("F2");
        }

        private void OnDeadConditionActivated() => _isDeathImage.gameObject.SetActive(true);
        private void OnHealthChanged(float health, float maxHealth) => _healthBar.SetHealthPercent(health / maxHealth);
    }
}
