using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    public int currentHealth { get; private set; }
    [SerializeField]
    private int startingHealth = 100;
    [SerializeField]
    private float sinkSpeed = 2.5f;
    [SerializeField]
    private int scoreValue = 10;
    [SerializeField]
    private AudioClip deathClip;

    private Rigidbody _rigidbody;
    private Animator _enemyAnimator;
    private AudioSource _enemyAudioSource;
    private ParticleSystem _hitParticles;
    private CapsuleCollider _capsuleCollider;
    private NavMeshAgent _navMeshAgent;
    private bool _isDead;
    private bool _isSinking;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _enemyAnimator = GetComponent<Animator>();
        _enemyAudioSource = GetComponent<AudioSource>();
        _hitParticles = GetComponentInChildren<ParticleSystem>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        currentHealth = startingHealth;
    }

    void Update()
    {
        if (_isSinking)
        {
            transform.Translate(Vector3.down * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (_isDead)
        {
            return;
        }

        _enemyAudioSource.Play();

        currentHealth -= amount;

        _hitParticles.transform.position = hitPoint;
        _hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        _isDead = true;

        _capsuleCollider.isTrigger = true;

        _enemyAnimator.SetTrigger("Dead");

        _enemyAudioSource.clip = deathClip;
        _enemyAudioSource.Play();
    }

    public void StartSinking()
    {
        _navMeshAgent.enabled = false;
        _rigidbody.isKinematic = true;

        _isSinking = true;

        ScoreManager.score += scoreValue;

        Destroy(gameObject, 2f);
    }
}
