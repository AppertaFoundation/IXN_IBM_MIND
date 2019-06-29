using UnityEngine;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixerSnapshot paused;
    [SerializeField]
    private AudioMixerSnapshot unpaused;

    private Canvas _canvas;

    void Start()
    {
        _canvas = GetComponent<Canvas>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _canvas.enabled = !_canvas.enabled;
            Pause();
        }
    }

    public void Pause()
    {
        Time.timeScale = (Time.timeScale == 0f ? 1f : 0f);
        Lowpass();
    }

    void Lowpass()
    {
        if (0f == Time.timeScale)
        {
            paused.TransitionTo(0.01f);
        }
        else
        {
            unpaused.TransitionTo(0.01f);
        }
    }

    public void Quit()
    {
        Pause();
    }
}
