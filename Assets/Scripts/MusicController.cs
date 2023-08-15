using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioManager audioManager;

    private void Start()
    {
        audioManager.Initialize(GetComponent<AudioSource>());
    }

    private void Update()
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            audioManager.PlayNextSong();
        }
    }
}