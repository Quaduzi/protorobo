using UnityEngine;

public class DisableMultiTouch : MonoBehaviour
{
    private void Awake()
    {
        Input.multiTouchEnabled = false;
    }
}
