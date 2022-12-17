using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField]
    public string nextLevel;

    private SoundPlayer _soundPlayer;
    
    private void Start()
    {
        EventManager.OnCardBeginDrag.AddListener(PauseAnimation);
        EventManager.OnCardEndDrag.AddListener(ProceedAnimation);
        EventManager.OnGameStarted.AddListener(ProceedAnimation);

        _soundPlayer = GetComponentInChildren<SoundPlayer>();
        
        PauseAnimation();
    }

    private void PauseAnimation()
    {
        GetComponent<Animator>().speed = 0;
    }
    
    private void ProceedAnimation()
    {
        GetComponent<Animator>().speed = 1;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (int.TryParse(SceneManager.GetActiveScene().name.Split().Last(), out var currentLevel))
            {
                Debug.Log(currentLevel);
                SaveManager.SaveCompletedLevelsCount(currentLevel);
            }
            col.GetComponent<Animator>().SetTrigger("Teleport");
            StartCoroutine(ChangeLevel(col.GetComponent<PlayerTriggerLogic>()));
        }
    }

    private IEnumerator ChangeLevel(PlayerTriggerLogic player)
    {
        _soundPlayer.PlayRandom();
        yield return player.StartCoroutine(player.PortalAnimation(transform));
        SceneManager.LoadScene(nextLevel);
    }
}
