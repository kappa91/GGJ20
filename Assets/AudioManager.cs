using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton

    public static AudioManager _thisInstance;

    public static AudioManager Get()
    {
        if (_thisInstance == null)
        {
            GameObject newGameObject = new GameObject("AudioManager");
            _thisInstance = newGameObject.AddComponent<AudioManager>();
        }

        return _thisInstance;
    }

    void Awake()
    {
        if (_thisInstance == null)
        {
            _thisInstance = this;
        }
        else
        {
            Destroy(this);
        }

    }

    #endregion

    public AudioSource m_soundsSource;

    public IEnumerator PlayIndependentSoundClipRoutine(AudioClip clip)
    {
        if (m_soundsSource.enabled && clip != null)
        {
            AudioSource tempSource = this.gameObject.AddComponent<AudioSource>();
            tempSource.clip = clip;
            tempSource.volume = m_soundsSource.volume;

            tempSource.Play();

            yield return new WaitForSeconds(clip.length);

            Destroy(tempSource);
        }

        yield return new WaitForEndOfFrame();
    }
}
