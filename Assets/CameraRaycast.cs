using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Physics2DRaycaster),
    typeof(EventSystem),
    typeof(StandaloneInputModule))]
public class CameraRaycast : MonoBehaviour
{
    [SerializeField] private LayerMask basic;
    [SerializeField] private LayerMask ignoreCard;

    private Physics2DRaycaster _raycaster;

    private void Awake()
    {
        _raycaster = GetComponent<Physics2DRaycaster>();
    }

    private void Start()
    {
        _raycaster.eventMask = basic;
        GlobalEventManager.OnStartCardTransition.AddListener(() => _raycaster.eventMask = ignoreCard);
        GlobalEventManager.OnEndCardTransition.AddListener(() => _raycaster.eventMask = basic);
    }
}
