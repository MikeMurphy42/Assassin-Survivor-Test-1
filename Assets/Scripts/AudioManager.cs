using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "AudioManager", menuName = "AudioManager")]
public class AudioManager : ScriptableObject
{
    [System.Serializable]
    public class LevelMusic
    {
        public string levelName;
        public AudioClip[] songs;
    }

    public LevelMusic[] levelsMusic;
    public bool playInRandomOrder = false;

    private AudioSource audioSource;
    private int currentLevelIndex = -1;
    private int currentSongIndex = 0;
    private AudioClip[] currentLevelSongs;

    public void Initialize(AudioSource source)
    {
        audioSource = source;
        SelectLevelMusic(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void SelectLevelMusic(string levelName)
    {
        for (int i = 0; i < levelsMusic.Length; i++)
        {
            if (levelsMusic[i].levelName == levelName)
            {
                currentLevelIndex = i;
                currentLevelSongs = levelsMusic[i].songs;
                currentSongIndex = 0;
                PlayNextSong();
                return;
            }
        }
        Debug.LogWarning("Level music not found for level " + levelName);
    }

    public void PlayNextSong()
    {
        if (currentLevelIndex == -1 || currentLevelSongs.Length == 0 || audioSource.isPlaying)
        {
            return;
        }

        if (playInRandomOrder)
        {
            int nextSongIndex;
            do
            {
                nextSongIndex = Random.Range(0, currentLevelSongs.Length);
            } while (nextSongIndex == currentSongIndex);

            currentSongIndex = nextSongIndex;
        }

        audioSource.clip = currentLevelSongs[currentSongIndex];
        audioSource.Play();

        currentSongIndex = (currentSongIndex + 1) % currentLevelSongs.Length;
    }

}