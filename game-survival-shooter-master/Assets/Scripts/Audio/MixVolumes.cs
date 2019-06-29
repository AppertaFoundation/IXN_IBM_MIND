using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixVolumes : MonoBehaviour
{

    [SerializeField]
    private AudioMixer masterMixer;

    [SerializeField]
    private Toggle audioToggle;

    public void SetMusicVol(float musicVol)
    {
        masterMixer.SetFloat("musicVol", musicVol);
    }

    public void SetSoundEffectsVol(float soundEffectsVol)
    {
        masterMixer.SetFloat("soundEffectsVol", soundEffectsVol);
    }

    public void ToggleAudio()
    {
        if (audioToggle.isOn)
        {
            masterMixer.SetFloat("audioVol", 0.0f);
        }
        else
        {
            masterMixer.SetFloat("audioVol", -80f);
        }
    }
}
