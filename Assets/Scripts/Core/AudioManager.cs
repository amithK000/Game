using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("AudioManager");
                    _instance = go.AddComponent<AudioManager>();
                }
            }
            return _instance;
        }
    }

    [System.Serializable]
    public class SoundEffect
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume = 1f;
        [Range(0.1f, 3f)]
        public float pitch = 1f;
        public bool loop = false;
        public bool spatialBlend = false;
        [HideInInspector]
        public AudioSource source;
    }

    public SoundEffect[] soundEffects;
    public AudioSource musicSource;
    public AudioClip[] backgroundTracks;
    
    // Default sound effects that should be included in the inspector
    [Header("Default Sound Effects")]
    [SerializeField] private AudioClip levelCompleteSound;
    [SerializeField] private AudioClip zombieDeathSound;
    [SerializeField] private AudioClip pauseSound;
    [SerializeField] private AudioClip resumeSound;
    [SerializeField] private AudioClip scoreSound;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioClip truckDeathSound;
    [SerializeField] private AudioClip collisionSound;
    [SerializeField] private AudioClip engineSound;
    
    [Header("Performance Settings")]
    [SerializeField] private int maxConcurrentSounds = 10;
    [SerializeField] private float cullingDistance = 50f;
    [SerializeField] private bool enableDistanceCulling = true;
    
    private Queue<AudioSource> _audioSourcePool = new Queue<AudioSource>();
    private List<AudioSource> _activeSources = new List<AudioSource>();
    private Transform _playerTransform;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        // Initialize all sound effects
        foreach (SoundEffect s in soundEffects)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.spatialBlend ? 1f : 0f;
        }

        // Initialize background music source
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
        }
        
        // Initialize audio source pool
        for (int i = 0; i < maxConcurrentSounds; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.enabled = false;
            _audioSourcePool.Enqueue(source);
        }
        
        // Find player transform for distance culling
        _playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
    }
    
    private void Update()
    {
        if (enableDistanceCulling && _playerTransform != null)
        {
            // Manage active audio sources and cull distant ones
            for (int i = _activeSources.Count - 1; i >= 0; i--)
            {
                AudioSource source = _activeSources[i];
                
                // Return finished sounds to the pool
                if (!source.isPlaying)
                {
                    ReturnSourceToPool(source);
                    _activeSources.RemoveAt(i);
                    continue;
                }
                
                // Cull distant sounds
                if (Vector3.Distance(source.transform.position, _playerTransform.position) > cullingDistance)
                {
                    ReturnSourceToPool(source);
                    _activeSources.RemoveAt(i);
                }
            }
        }
    }

    public void PlaySound(string name, Vector3? position = null)
    {
        SoundEffect s = System.Array.Find(soundEffects, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        
        // For looping sounds, use the dedicated source
        if (s.loop)
        {
            if (position.HasValue)
            {
                s.source.transform.position = position.Value;
            }
            s.source.Play();
            return;
        }
        
        // For non-looping sounds, use the audio source pool
        if (_playerTransform != null && position.HasValue && 
            enableDistanceCulling && 
            Vector3.Distance(position.Value, _playerTransform.position) > cullingDistance)
        {
            // Skip playing sounds that are too far away
            return;
        }
        
        // Get audio source from pool
        AudioSource source = GetSourceFromPool();
        if (source == null) return; // No available sources
        
        // Configure the source
        source.clip = s.clip;
        source.volume = s.volume;
        source.pitch = s.pitch;
        source.spatialBlend = s.spatialBlend ? 1f : 0f;
        
        if (position.HasValue)
        {
            source.transform.position = position.Value;
        }
        
        source.Play();
        _activeSources.Add(source);
    }

    public void StopSound(string name)
    {
        SoundEffect s = System.Array.Find(soundEffects, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Stop();
    }

    private AudioSource GetSourceFromPool()
    {
        if (_audioSourcePool.Count > 0)
        {
            AudioSource source = _audioSourcePool.Dequeue();
            source.enabled = true;
            return source;
        }
        
        // If no sources are available in the pool, find the oldest playing sound
        // that isn't looping and replace it
        if (_activeSources.Count > 0)
        {
            AudioSource oldestSource = _activeSources[0];
            _activeSources.RemoveAt(0);
            oldestSource.Stop();
            return oldestSource;
        }
        
        return null;
    }
    
    private void ReturnSourceToPool(AudioSource source)
    {
        if (source == null) return;
        
        source.Stop();
        source.clip = null;
        source.enabled = false;
        _audioSourcePool.Enqueue(source);
    }

    public void PlayRandomBackgroundTrack()
    {
        if (backgroundTracks.Length > 0)
        {
            int randomIndex = Random.Range(0, backgroundTracks.Length);
            musicSource.clip = backgroundTracks[randomIndex];
            musicSource.Play();
        }
    }

    public void StopBackgroundMusic()
    {
        musicSource.Stop();
    }

    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}