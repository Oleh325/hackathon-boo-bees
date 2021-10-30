using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private AudioClip _PumpkinPickedUpSound;
    [SerializeField] private AudioSource _AudioSource;
    [SerializeField] private AudioSource _BatAudioSource;
    private AudioClip _TransformationSound;
    private AudioClip _BatFly;

    // Start is called before the first frame update
    void Start()
    {
        _PumpkinPickedUpSound = Resources.Load<AudioClip>("Sounds/PumpkinPickedUp");
        _TransformationSound = Resources.Load<AudioClip>("Sounds/Transformation");
        _BatFly = Resources.Load<AudioClip>("Sounds/BatFly");
    }

    public void playSound(string clip) {
        switch (clip) {
            case "PumpkinPickedUp":
                Debug.Log("playing sound");
                _AudioSource.PlayOneShot(_PumpkinPickedUpSound);
                break;
            case "Transformation":
                Debug.Log("playing sound");
                _AudioSource.PlayOneShot(_TransformationSound);
                break;
            case "BatFly":
                Debug.Log("playing sound");
                _BatAudioSource.Play();
                break;
        }
    }
    public void stopSound(string clip) {
        switch (clip) {
            case "BatFly":
                Debug.Log("stopping sound");
                _BatAudioSource.Stop();
                break;
        }
    }

}
