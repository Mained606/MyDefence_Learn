using UnityEngine;
using UnityEngine.UI;

namespace MyDefence
{
    //Enemy를 관리하는 클래스
    public class Enemy : MonoBehaviour, IDamageable
    {
        //필드
        #region Variable
        //체력
        private float health;
        [SerializeField] private float startHealth = 100;

        //죽음 효과
        public GameObject deathEffectPrefab;

        //보상 금액
        [SerializeField]
        private int rewardGold = 50;

        //HealthBar
        public Image healthBar;

        //죽음 여부
        private bool isDead = false;
        #endregion

        private void Start()
        {   
            //초기화
            health = startHealth;
        }

        //데미지 처리
        public void TakeDamage(float damage)
        {
            if(isDead) return;
            health -= damage;
            //Debug.Log($"health: {health}");

            //HealthBar : 현재값(량) / 총값(량)
            healthBar.fillAmount = health / startHealth;

            if (health <= 0)
            {
                Die();
            }
        }

        //죽음 처리
        void Die()
        {
            if(isDead) return;

            isDead = true;
            //죽는 파티클 이펙트 처리
            GameObject effectGo = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effectGo, 2f);

            //리워드로 50 Gold 지급
            PlayerStats.AddMoney(rewardGold);
            //살아있는 적 카운팅
            SpawnManager.enmeyAlive--;

            //kill
            Destroy(gameObject);
        }

    }
}
