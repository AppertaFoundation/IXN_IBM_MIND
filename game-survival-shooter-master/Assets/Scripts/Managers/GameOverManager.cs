using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private PlayerHealth playerHealth;
    [SerializeField]
    private Animator gameOverAnimator;
    [SerializeField]
    private float restartDelay = 5f;

    private bool _isGameOver = false;
    private float _timer = 0f;

    void Update()
    {
        if (playerHealth.currentHealth <= 0 && !_isGameOver)
        {
            _isGameOver = true;

            gameOverAnimator.SetTrigger("GameOver");
        }

        if (_isGameOver)
        {
            _timer += Time.deltaTime;

            if (_timer >= restartDelay)
            {
                SceneManager.LoadScene("Level 01");
            }
        }
    }
}
