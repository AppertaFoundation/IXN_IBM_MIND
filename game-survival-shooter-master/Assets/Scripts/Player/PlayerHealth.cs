using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    public int currentHealth { get; private set; }
    [SerializeField]
    private int startingHealth = 100;
    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private Image damageImage;
    [SerializeField]
    private float flashSpeed = 5f;
    [SerializeField]
    private Color flashColour = new Color(1f, 0f, 0f, 0.3f);
    [SerializeField]
    private AudioClip deathClip;

    private Animator _playerAnimator;
    private AudioSource _playerAudioSource;
    private PlayerMovement _playerMovement;
    private PlayerShooting _playerShooting;
    private bool _isDead;
    private bool _isDamaged;

    void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        _playerAudioSource = GetComponent<AudioSource>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerShooting = GetComponentInChildren<PlayerShooting>();

        currentHealth = startingHealth;
    }

    void Update()
    {
        if (_isDamaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, Time.deltaTime * flashSpeed);
        }

        _isDamaged = false;
    }

    public void TakeDamage(int amount)
    {
        _isDamaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;
        _playerAudioSource.Play();

        if (currentHealth <= 0 && !_isDead)
        {
            Death();
        }
    }

    private void Death()
    {
        _isDead = true;

        _playerShooting.DisableEffects();

        _playerAnimator.SetTrigger("Die");

        _playerAudioSource.clip = deathClip;
        _playerAudioSource.Play();

        _playerMovement.enabled = false;
        _playerShooting.enabled = false;
    }
}
