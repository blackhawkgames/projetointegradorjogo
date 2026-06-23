using UnityEngine;
using UnityEngine.UI;

public class FloatingIconManager : MonoBehaviour
{
    public static FloatingIconManager Instance;

    [Header("Setup")]
    [SerializeField] private GameObject iconPrefab;

    [Header("Icons")]
    [SerializeField] private Sprite interactIcon;
    [SerializeField] private Sprite collectIcon;
    [SerializeField] private Sprite documentIcon;
    [SerializeField] private Sprite computerIcon;

    private GameObject currentIcon;
    private FloatingIcon floatingIcon;
    private Image iconImage;

    private Transform currentTarget;

    private void Awake()
    {
        Instance = this;

        currentIcon = Instantiate(iconPrefab);

        floatingIcon =
            currentIcon.GetComponent<FloatingIcon>();

        iconImage =
            currentIcon.GetComponentInChildren<Image>();

        currentIcon.SetActive(false);
    }

    public void ShowIcon(Transform target, FloatingIconType type, Vector3 offset)
    {
        if (currentTarget == target)
            return;

        if (floatingIcon == null)
        {
            currentIcon = Instantiate(iconPrefab);

            floatingIcon =
                currentIcon.GetComponent<FloatingIcon>();

            iconImage =
                currentIcon.GetComponentInChildren<Image>();

            currentIcon.SetActive(false);
        }

        currentTarget = target;

        currentIcon.transform.SetParent(target, false);
        currentIcon.transform.localPosition = offset;
        currentIcon.transform.localRotation = Quaternion.identity;

        iconImage.sprite = GetSprite(type);

        floatingIcon.Show();
    }

    public void HideIcon()
    {
        currentTarget = null;

        if (floatingIcon == null)
        {
            floatingIcon = null;
            currentIcon = null;
            return;
        }

        floatingIcon.Hide();
    }

    public void RemoveTarget(Transform target)
    {
        if (currentTarget != target)
            return;

        HideIcon();
    }

    Sprite GetSprite(FloatingIconType type)
    {
        switch (type)
        {
            case FloatingIconType.Collect:
                return collectIcon;

            case FloatingIconType.Document:
                return documentIcon;

            case FloatingIconType.Computer:
                return computerIcon;

            default:
                return interactIcon;
        }
    }
}