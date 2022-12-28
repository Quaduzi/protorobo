using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<Player>(out var player))
        {
            StartCoroutine(ChangeLevel(player.transform));
        }
    }

    private IEnumerator SuctionAnimation(Transform target)
    {
        while (target.position != transform.position)
        {
            target.position = Vector3.MoveTowards(target.position, transform.transform.position, Time.unscaledDeltaTime);
            target.localScale = Vector3.MoveTowards(target.localScale, Vector3.zero, Time.unscaledDeltaTime);
            target.Rotate(Vector3.forward, 360 * Time.unscaledDeltaTime);
            yield return null;
        }
    }

    private IEnumerator ChangeLevel(Transform initiator)
    {
        _animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        GlobalEventManager.SendStartLevelTransition();
        yield return StartCoroutine(SuctionAnimation(initiator));
        SceneControl.NextLevel();
    }
}
