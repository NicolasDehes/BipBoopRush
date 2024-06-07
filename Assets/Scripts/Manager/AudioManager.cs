using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioClip gameOverSound;

    void Start()
    {
        backgroundMusic.Play();
    }

    public void PlayGameOverSound()
    {
        backgroundMusic.Stop();
        AudioSource.PlayClipAtPoint(gameOverSound, Camera.main.transform.position);
    }
}
