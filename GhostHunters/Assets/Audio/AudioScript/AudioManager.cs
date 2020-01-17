using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	public AudioMixerGroup mixerGroupMusic;
	public AudioMixerGroup mixerGroupSound;

	public Sound[] musics;
	public Sound[] sounds;
	public Sound[] apparitionSounds;

	public enum MUSIC
	{
		Music,
		IdlePresence,
		CachePresence,
	};

	public enum SFX
	{
		NouvelleVague,
		Disparition,
		Death,
		StunSound,
		Laser,
		Apparition,
        LaserBurst,
	}

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in musics)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;

			if (s.source.outputAudioMixerGroup == null)
			{
				s.source.outputAudioMixerGroup = mixerGroupMusic;
			}
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;

			if (s.source.outputAudioMixerGroup == null)
			{
				s.source.outputAudioMixerGroup = mixerGroupSound;
			}
		}

		foreach (Sound s in apparitionSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;

			if (s.source.outputAudioMixerGroup == null)
			{
				s.source.outputAudioMixerGroup = mixerGroupSound;
			}
		}
	}

	private void Start()
	{
		PlayMusic(MUSIC.Music);
	}
    

	public void PlaySFX(SFX sfx)
	{
		switch (sfx)
		{
			case SFX.NouvelleVague:
				Play(sounds, "NouvelleVague", true);
				break;
			case SFX.Disparition:
				Play(sounds, "Disparition", true);
				break;
			case SFX.Death:
				Play(sounds, "Death", true);
				break;
			case SFX.StunSound:
				Play(sounds, "StunSound", true);
				break;
			case SFX.Laser: 
				Play(sounds, "Laser", false);
				break;
            case SFX.LaserBurst:
                Play(sounds, "LaserBurst", false);
                break;
            case SFX.Apparition:
				int rNum = UnityEngine.Random.Range(0, apparitionSounds.Length);
				Play(apparitionSounds, apparitionSounds[rNum].name, true);
				break;
			
		}
	}

	public void PlayMusic(MUSIC musicsEnum)
	{
		switch (musicsEnum)
		{
			case MUSIC.Music:
				Play(musics, "Music");
				break;
			case MUSIC.IdlePresence:
				Play(musics, "IdlePresence");
				break;
			case MUSIC.CachePresence:
				Play(musics, "CachePresence");
				break;

		}
	}

	public void StopMusic(MUSIC musicsEnum)
	{
		switch (musicsEnum)
		{
			case MUSIC.Music:
				StopPlaying(musics, "Music");
				break;
			case MUSIC.IdlePresence:
				StopPlaying(musics, "IdlePresence");
				break;
			case MUSIC.CachePresence:
				StopPlaying(musics, "CachePresence");
				break;

		}
	}

	public void StopSfx(SFX sfxEnum)
	{
		switch (sfxEnum)
		{
			case SFX.Laser:
				StopPlaying(sounds, "Laser");
				break;
		}
	}

	private void Play(Sound[] sounds, string sound, bool SFX = false, bool doNotLoop = false)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		if(doNotLoop)
		{
			s.source.loop = false;
		}

		if (!SFX)
		{
			s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
			s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

			
			s.source.Play();
		}
		else
		{
			s.source.PlayOneShot(s.clip);
		}
	}

	private void StopPlaying(Sound[] sounds, string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Stop();
	}
}
