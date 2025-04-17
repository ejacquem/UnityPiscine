using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _keyCountText;
    [SerializeField] private GameObject _exitDoor;
    [SerializeField] private Animator _UIAnimator;
    [SerializeField] private GameObject _confettiEffect1;
    [SerializeField] private GameObject _confettiEffect2;

    private int _keyCount = 0;
    private int _keyRequiredAmount = 3;

    public static GameManager Instance;

    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        
        _UIAnimator.SetTrigger("Start");
        AudioManager.Instance.Play("Ambience");
        // DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        
    }

    public void KeyCollected()
    {
        _keyCount++;
        _keyCountText.SetText($"Keys: {_keyCount}/{_keyRequiredAmount}");
        if (_keyCount == _keyRequiredAmount)
            _exitDoor.GetComponent<Door>().enabled = true;
    }

    public int GetKeyCount()
    {
        return _keyCount;
    }

    public void PlayerLose()
    {
        _UIAnimator.SetTrigger("Lose");
        AudioManager.Instance.Play("Lose");
    }

    public void PlayerWin()
    {
        _UIAnimator.SetTrigger("Win");
        AudioManager.Instance.Play("Win");
        _confettiEffect1.SetActive(true);
        _confettiEffect2.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
