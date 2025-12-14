using System.Net.Security;
using Unity.VisualScripting;
using UnityEngine;

public class SoundFXObject : MonoBehaviour
{
    public static SoundFXObject instance;
    [SerializeField] private AudioSource soundFXObject;
    private AudioSource musicFX;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void PlaySoundFX(AudioClip audioClip, Transform spawnTransfrom, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransfrom.position, Quaternion.identity);

        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        float clipLength = audioClip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayMusicFX(AudioClip audioClip, float volume)
    {
        musicFX = Instantiate(soundFXObject, Vector3.zero, Quaternion.identity);

        musicFX.clip = audioClip;
        musicFX.volume = volume;
        musicFX.loop = true;
        musicFX.Play();
    }

    public void StopMusicFX()
    {
        if (musicFX != null)
            Destroy(musicFX.gameObject);
    }

    public void PlayWinSound(AudioClip audioClip, float volume)
    {
        AudioSource winClip = Instantiate(soundFXObject, Vector3.zero, Quaternion.identity);
        winClip.clip = audioClip;
        winClip.volume = volume;
        winClip.Play();

        float clipLength = 10.0f;
        Destroy(winClip.gameObject, clipLength);
        

        
    }
    

    
}
