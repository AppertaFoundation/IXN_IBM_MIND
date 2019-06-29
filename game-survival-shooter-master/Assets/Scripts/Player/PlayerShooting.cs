using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private int damagePerShot = 20;
    [SerializeField]
    private float timeBetweenBullets = 0.15f;
    [SerializeField]
    private float range = 100f;

    private float _timer;
    private Ray _shootRay = new Ray();
    private RaycastHit _shootHit;
    private int _shootableMask;
    private ParticleSystem _gunParticles;
    private LineRenderer _gunLine;
    private Light _gunLight;
    private float effectsDisplayTime = 0.2f;
    private AudioSource _gunAudioSource;

    void Awake()
    {
        _shootableMask = LayerMask.GetMask("Shootable");

        _gunParticles = GetComponent<ParticleSystem>();
        _gunLine = GetComponent<LineRenderer>();
        _gunLight = GetComponent<Light>();
        _gunAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && _timer >= timeBetweenBullets)
        {
            Shoot();
        }

        if (_timer > timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        _gunLine.enabled = false;
        _gunLight.enabled = false;
    }

    private void Shoot()
    {
        _timer = 0f;

        _gunAudioSource.Play();

        _gunLight.enabled = true;

        _gunParticles.Stop();
        _gunParticles.Play();

        _gunLine.enabled = true;
        _gunLine.SetPosition(0, transform.position);

        _shootRay.origin = transform.position;
        _shootRay.direction = transform.forward;

        if (Physics.Raycast(_shootRay, out _shootHit, range, _shootableMask))
        {
            EnemyHealth enemyHealth = _shootHit.collider.GetComponent<EnemyHealth>();

            if (null != enemyHealth)
            {
                enemyHealth.TakeDamage(damagePerShot, _shootHit.point);
            }

            _gunLine.SetPosition(1, _shootHit.point);
        }
        else
        {
            _gunLine.SetPosition(1, _shootRay.origin + _shootRay.direction * range);
        }
    }
}