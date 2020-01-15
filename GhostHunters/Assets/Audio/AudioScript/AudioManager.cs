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

	public enum MUSIC
	{
		Swirl,
		Heartbeat,
		Crowd,
		Ambiance1,
		Ambiance2,
		Pad,
	};

	public enum SFX
	{
		DemonBreath,
		HorrorDrum,
		MetalGhost,
		BreathLoudBass,
		GhostBreath,
		ScratchScary,
		HumanBone,
		Laser,
		ChoirGhostRise,
		TownGhost,
		Sob,
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
	}


	public void PlaySFX(SFX sfx)
	{
		switch (sfx)
		{
			case SFX.DemonBreath:
				Play(sounds, "DemonBreath", true);
				break;
			case SFX.HorrorDrum:
				Play(sounds, "HorrorDrum", true);
				break;
			case SFX.MetalGhost:
				Play(sounds, "MetalGhost", true);
				break;
			case SFX.BreathLoudBass:
				Play(sounds, "DemonBreath", true);
				break;
			case SFX.GhostBreath:
				Play(sounds, "GhostBreath", true);
				break;
			case SFX.ScratchScary:
				Play(sounds, "ScaryScratch", true);
				break;
			case SFX.HumanBone:
				Play(sounds, "HumanBrokedBone", true);
				break;
			case SFX.Laser:
				Play(sounds, "Laser", true);
				break;
			case SFX.ChoirGhostRise:
				Play(sounds, "GhostChoir", true);
				break;
			case SFX.TownGhost:
				Play(sounds, "GhostTown", true);
				break;
			case SFX.Sob:
				Play(sounds, "Sob", true);
				break;
		}
	}

	public void PlayMusic(MUSIC musicsEnum)
	{
		switch (musicsEnum)
		{
			case MUSIC.Swirl:
				Play(musics, "Swirls");
				break;
			case MUSIC.Heartbeat:
				Play(musics, "Heartbeat");
				break;
			case MUSIC.Crowd:
				Play(musics, "GhostCrowd");
				break;
			case MUSIC.Ambiance1:
				Play(musics, "GhostAmbiance");
				break;
			case MUSIC.Ambiance2:
				Play(musics, "BreathAmbiance");
				break;
			case MUSIC.Pad:
				Play(musics, "Ghostpad");
				break;
		}
	}

	public void StopMusic(MUSIC musicsEnum)
	{
		switch (musicsEnum)
		{
			case MUSIC.Swirl:
				StopPlaying(musics, "Swirls");
				break;
			case MUSIC.Heartbeat:
				StopPlaying(musics, "Heartbeat");
				break;
			case MUSIC.Crowd:
				StopPlaying(musics, "GhostCrowd");
				break;
			case MUSIC.Ambiance1:
				StopPlaying(musics, "GhostAmbiance");
				break;
			case MUSIC.Ambiance2:
				StopPlaying(musics, "BreathAmbiance");
				break;
			case MUSIC.Pad:
				StopPlaying(musics, "Ghostpad");
				break;
		}
	}

	public void Play(Sound[] sounds, string sound, bool SFX = false, bool doNotLoop = false)
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


	public void StopPlaying(Sound[] sounds, string sound)
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
