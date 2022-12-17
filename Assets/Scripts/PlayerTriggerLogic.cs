using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTriggerLogic : MonoBehaviour
{
    public SoundPlayer deathSoundPlayer;
    public SoundPlayer flipSoundPlayer;

    private PlayerMovement _playerMovement;
    private SpriteRenderer _renderer;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("FlipTrigger"))
        {
            _playerMovement.Flip();
            flipSoundPlayer.PlayRandom();
        }
        if (col.CompareTag("CardTrigger") && col.transform != transform.parent) ChangeParent(col.transform);
        if (col.CompareTag("GravityTrigger"))
        {
            EventManager.SendStartTransition();
            _playerMovement.DisableGravity();
        }
        if (col.CompareTag("DeathTrigger")) StartCoroutine(Death());
        if (col.CompareTag("Portal")) StartCoroutine(PortalAnimation(col.transform));
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("GravityTrigger"))
        {
            EventManager.SendEndTransition();
            _playerMovement.EnableGravity();
        }
    }

    private void ChangeParent(Transform parent)
    {
        var player = GetComponent<Rigidbody2D>().transform;
        var currentCardSpriteMask = GetComponentInParent<SpriteMask>();
        if (currentCardSpriteMask != null) StartCoroutine(DisableSpriteMask(currentCardSpriteMask));
        parent.GetComponent<SpriteMask>().enabled = true;
        
        player.SetParent(parent, true);
    }

    private static IEnumerator DisableSpriteMask(SpriteMask spriteMask)
    {
        yield return new WaitForSeconds(.5f);
        spriteMask.enabled = false;
        yield return null;
    }

    public IEnumerator Death()
    {
        _playerMovement.DisableSimulation();
        deathSoundPlayer.PlayRandom();
        yield return StartCoroutine(DeathAnimation());
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator DeathAnimation()
    {
        _renderer.maskInteraction = SpriteMaskInteraction.None;
        
        transform.position = new Vector3(transform.position.x, transform.position.y, -9);
        
        var up = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        while (transform.position != up)
        {
            transform.position = Vector3.MoveTowards(transform.position, up, Time.deltaTime * 3);
            yield return null;
        }
        var down = new Vector3(transform.position.x, transform.position.y - 5, transform.position.z);
        while (transform.position != down)
        {
            transform.position = Vector3.MoveTowards(transform.position, down, Time.deltaTime * 7);
            yield return null;
        }
    }
    public IEnumerator PortalAnimation(Transform portal)
    {
        while (transform.position != portal.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, portal.transform.position, Time.deltaTime);
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, Time.deltaTime);
            yield return null;
        }
    }
    
}
