using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private float timeBetweenAttacks = 0.5f;
    [SerializeField]
    private int attackDamage = 10;

    private Animator _enemyAnimator;
    private GameObject _player;
    private PlayerHealth _playerHealth;
    private EnemyHealth _enemyHealth;

    private bool _isPlayerInRange;
    private float _timer;

    void Awake()
    {
        _enemyAnimator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerHealth = _player.GetComponent<PlayerHealth>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= timeBetweenAttacks && _isPlayerInRange && _enemyHealth.currentHealth > 0)
        {
            Attack();
        }

        if (_playerHealth.currentHealth <= 0)
        {
            _enemyAnimator.SetTrigger("PlayerDead");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
        {
            _isPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player)
        {
            _isPlayerInRange = false;
        }
    }

    private void Attack()
    {
        _timer = 0f;

        if (_playerHealth.currentHealth > 0)
        {
            _playerHealth.TakeDamage(attackDamage);
        }
    }
}
