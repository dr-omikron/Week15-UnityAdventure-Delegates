using UnityEngine;
using UnityEngine.UI;

namespace Develop._3.EnemyDestroyer
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;

        public void SetHealthPercent(float percent) => _healthBar.fillAmount = percent;
    }
}
