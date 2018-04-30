using UnityEngine;

[System.Serializable]
public class Sound {

	public string name;
	public AudioClip clip;

	[Range(0f, 1f)]
	public float volume = 0.7f;
	[Range(0.5f, 1.5f)]
	public float pitch = 1f;

	[Range(0f, 0.5f)]
	public float randomVolume = 0.1f;
	[Range(0f, 0.5f)]
	public float randomPitch = 0.1f;

	public bool loop;

	private AudioSource source;

	public void SetSource(AudioSource _source)
	{
		source = _source;
		source.clip = clip;
	}

	public void Play()
	{
		source.volume = volume * (1 + Random.Range(-randomVolume/2, randomVolume/2));
		source.pitch = pitch * (1 + Random.Range(-randomPitch/2, randomPitch/2));
		source.loop = loop;
		source.Play ();
	}

	public void Stop()
	{
		source.Stop ();
	}

}

public class AudioManager : MonoBehaviour {

	public static AudioManager instance;

	[SerializeField]
	Sound[] sounds;

	void Awake()
	{
		if (instance != null) {
			if (instance != this) {
				Destroy (this.gameObject);
			}
		} else {
			instance = this;
			DontDestroyOnLoad(this);
		}
	}

	void Start()
	{
		for (int i = 0; i < sounds.Length; i++) {
			GameObject go = new GameObject ("Sound" + i + "_" + sounds [i].name);
			go.transform.SetParent(this.transform);
			sounds[i].SetSource(go.AddComponent<AudioSource> ());
		}

		PlaySound ("Music");
	}

	public void PlaySound(string _name)
	{
		for (int i = 0; i < sounds.Length; i++) {
			if (sounds [i].name == _name) {
				sounds [i].Play ();
				return;
			}
		}

		// no sound With _name
		Debug.LogWarning("AudioManager: Sound not found in list: " + _name);
	}

	public void StopSound(string _name)
	{
		for (int i = 0; i < sounds.Length; i++) {
			if (sounds [i].name == _name) {
				sounds[i].Stop();
				return;
			}
		}

		// no sound With _name
		Debug.LogWarning("AudioManager: Sound not found in list: " + _name);
	}
}
