using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

public class SoundSystem : MonoBehaviour {

    [Header("Volumes")]
    [SerializeField] float baseVolume = 0.1f;
    [SerializeField] float musicVolume = 0.1f;
    [SerializeField] bool muted = false;
    [SerializeField] AudioClip MenuMusic;
    [Range(0, 1)] [SerializeField] float MenuMusicVolume = 0.1f;
    [SerializeField] AudioClip GameMusic;
    [Range(0, 1)] [SerializeField] float GameMusicVolume = 0.1f;
    [Header("Sound Lists")]
    [SerializeField] AudioClip[] BrickSounds;
    [SerializeField] AudioClip[] AppleSounds;
    [SerializeField] AudioClip[] BossSounds;

    AudioSource audioSource;
    AudioClip playedMusic;
    // static SoundSystem instance = null;

    public enum PlayListID { Brick, Apple, Boss }

    // Use this for initialization
    void Start() {
        //if (instance != null && instance != this) {
        //    print("Destroying duplicate MusicPlayer");
        //    Destroy(gameObject);
        //} else {
        //    instance = this;
        //    DontDestroyOnLoad(this);
        //}
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = baseVolume;
        playedMusic = null;
        if (GetVolume() != 0) PlayMenuMusic();
        //print("MusicPlayer is now active");
    }

    public float GetVolume() {
        return baseVolume;
    }

    public void SetVolume(float newVolume) {
        audioSource.volume = newVolume;
    }

    private void onSceneLoaded(Scene loadedScene, LoadSceneMode mode) {
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += onSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= onSceneLoaded;
    }

    public void PlayClipOnce(AudioClip clip) {
        if (audioSource) audioSource.PlayOneShot(clip);
    }

    private void unMute() {
        if (audioSource) audioSource.volume = baseVolume;
    }

    public void musicOnOff() {
        //print("Toggling music on/off");
        if (muted) {
            audioSource.volume = baseVolume;
            muted = false;
        } else {
            audioSource.volume = 0f;
            muted = true;
        }
    }
    public void PlayRandomSoundFromList(PlayListID playListID) {
        AudioClip[] playList = null;
        switch (playListID) {
            case PlayListID.Apple: playList = AppleSounds; break;
            case PlayListID.Brick: playList = BrickSounds; break;
            case PlayListID.Boss: playList = BossSounds; break;
        }
        //print("SoundSystem/PlayRandomSoundFromList: playing based on PlayListID: " + playListID.ToString());
        playFromPlayList(playList);
    }

    private void playFromPlayList(AudioClip[] playList) {
        if (playList != null || playList.Length != 0) {
            PlayClipOnce(playList[Random.Range(0, playList.Length)]);
        } else {
            print("SoundSystem/playFromPlayList: failed geting playlist, playing BrickSounds instead");
            PlayClipOnce(BrickSounds[0]);
        }
    }

    public void PlayMenuMusic() {
        PlayMusicClip(MenuMusic);
        musicVolume = MenuMusicVolume;
    }

    public void PlayGameMusic() {
        PlayMusicClip(GameMusic);
        musicVolume = GameMusicVolume;
    }

    private void PlayMusicClip(AudioClip musicClip) {
        if (playedMusic != musicClip) {
            playedMusic = musicClip;
            audioSource.Play();
        }
    }
}
