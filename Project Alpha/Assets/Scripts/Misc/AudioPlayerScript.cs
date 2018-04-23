using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioPlayerScript : MonoBehaviour {
    public GameObject audioPlayerPrefab;

    [Range(0,1)]
    public float SoundVolume,
        MusicVolume;

    List<AudioClip> tracks = new List<AudioClip>();

    int currentrack;

    AudioSource audioSource;

    bool isPaused = false;
	// Use this for initialization
	void Start () {
       foreach (Object o in Resources.LoadAll("Audio/Music"))
        {
            tracks.Add((AudioClip)o);
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = tracks[Random.Range(0,tracks.Count)];
        audioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
        audioPlayerPrefab.GetComponent<AudioSource>().volume = SoundVolume;
        GetComponent<AudioSource>().volume = MusicVolume;


        if(!isPaused)
        {
            audioSource.UnPause();
        }

            if (!audioSource.isPlaying && !isPaused)
            {
                if (currentrack + 1 >= tracks.Count)
                {
                    currentrack = -1;
                }
                currentrack++;
                audioSource.clip = tracks[currentrack];
                audioSource.Play();
            }
        

	}

    private void OnApplicationPause(bool pause)
    {
        isPaused = pause;
    }
    private void OnApplicationFocus(bool focus)
    {
        isPaused = focus;
    }

    public void PlayAudio(string SoundName,Vector3 position,bool IsRanged)
    {
        GameObject g = Instantiate(audioPlayerPrefab, position, Quaternion.identity);
        Time.timeScale = 1;
        if(Resources.Load("Audio/Combat/" + SoundName) != null)
        {
            g.GetComponent<AudioSource>().volume = SoundVolume;
            g.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Audio/Combat/" + SoundName));
            Destroy(g, 120);
        }
        else if (Resources.Load("Audio/UI/" + SoundName) != null)
        {
            g.GetComponent<AudioSource>().volume = SoundVolume;
            g.GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Audio/UI/" + SoundName);
           // g.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Audio/UI/" + SoundName));
            g.GetComponent<AudioSource>().Play();
            Destroy(g, 120);
        }
        else if (Resources.Load("Audio/Misc/" + SoundName) != null)
        {
            g.GetComponent<AudioSource>().volume = SoundVolume;
            g.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Audio/Misc/" + SoundName));
            Destroy(g, 120);
        }
        else
        {
            print("Audio " + SoundName + " Not Found!");
        }
        if (FindObjectOfType<MenuOpenScript>().MenuOpen)
            Time.timeScale = 0;

        if(!IsRanged)
        {
            g.GetComponent<AudioSource>().spatialBlend = 0;
        }
    }

    public void AdjustSoundVolume(Slider s)
    {
        SoundVolume = s.value;
    }
    public void AdjustMusicVolume(Slider s)
    {
        MusicVolume = s.value;
    }
}
