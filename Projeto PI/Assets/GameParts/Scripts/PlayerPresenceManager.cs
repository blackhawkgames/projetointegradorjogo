using UnityEngine;

public class PlayerPresenceManager : MonoBehaviour
{
    public static PlayerPresenceManager Instance;

    [Header("References")]
    public FirstPersonController player;

    [Header("States")]
    public bool isInCutscene;
    public bool isPaused;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        if (player == null)
            player = FindObjectOfType<FirstPersonController>();
    }

    public FirstPersonController GetPlayer()
    {
        return player;
    }
    public void SetCutscene(bool state)
    {
        isInCutscene = state;

        if (player != null)
            player.SetCutscene(state);
    }

    public void SetPause(bool state)
    {
        isPaused = state;

        Time.timeScale = state ? 0f : 1f;

        if (player != null)
            player.EnableLook(!state);
    }
}