using UnityEngine;

public class MouseLocker : MonoBehaviour
{
    public bool isLockOnStart = false;
    public bool isUnlockOnStart = false;

    private void Start()
    {
        if(isLockOnStart) LockMouse();
        if(isUnlockOnStart) UnlockMouse();
    }
    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
