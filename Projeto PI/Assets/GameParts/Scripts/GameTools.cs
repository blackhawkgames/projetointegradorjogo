using UnityEngine;

public class GameTools : MonoBehaviour
{
    public PlayerPresenceManager ppm;

    void Start()
    {
        ppm = PlayerPresenceManager.Instance;
    }

    public void StartCutscene()
    {
        ppm.SetCutscene(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void EndCutscene()
    {
        ppm.SetCutscene(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PauseGame()
    {
        ppm.SetPause(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        ppm.SetPause(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void DisableMovement()
    {
        ppm.GetPlayer()?.EnableMovement(false);
    }

    public void EnableMovement()
    {
        ppm.GetPlayer()?.EnableMovement(true);
    }

    public void TeleportPlayer(Vector3 pos)
    {
        if (ppm.GetPlayer() != null)
            ppm.GetPlayer().transform.position = pos;
    }
}