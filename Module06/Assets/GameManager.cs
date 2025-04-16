using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _keyCountText;
    [SerializeField] private GameObject _exitDoor;

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
}
