using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public BoardManager boardScript;

    private int level = 3;
    public int playerCoins;
    public AudioClip DeathSound;

    private AudioSource _audioSource;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        _audioSource = GetComponent<AudioSource>();
        InitGame();
    }

    void InitGame()
    {
        boardScript.SetupScene(level);
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void GameOver()
    {
        _audioSource.clip = DeathSound;
        _audioSource.Play();
    }
}
