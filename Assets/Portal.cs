using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<Player>(out _))
        {
            SceneControl.NextLevel();
        }
    }

    //private IEnumerator ChangeLevel(PlayerTriggerLogic player)
    //{
    //    _soundPlayer.PlayRandom();
    //    yield return player.StartCoroutine(player.PortalAnimation(transform));
    //    SceneManager.LoadScene(nextLevel);
    //}
}
