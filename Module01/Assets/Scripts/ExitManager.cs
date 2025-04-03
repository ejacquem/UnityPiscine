using System.Collections.Generic;
using UnityEngine;

public class ExitManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _exits;
    [SerializeField]
    private List<GameObject> _players;

    [SerializeField]
    private float _tolerance;
    [SerializeField]
    private float _kickSpeed;

    private bool _allPlayerOnExit = false;
    private const float _exitTime = 2f;
    public float _exitTimer = _exitTime;

    Vector3 AbsVector3(Vector3 v)
    {
        return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
    }

    void Start()
    {
        _exitTimer = _exitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(_allPlayerOnExit){
            _exitTimer -= Time.deltaTime;
            KickPlayerOut();
            return;
        }
        _allPlayerOnExit = true;
        for (int i = 0; i < _exits.Count; i++)
        {
            Vector3 playerPos = _players[i].transform.position;
            playerPos.y -= _players[i].transform.localScale.y * 0.5f;
            Vector3 dist = AbsVector3(_exits[i].transform.position - playerPos);

            // if(i == 0) Debug.Log($"dist.x = {dist.x}, dist.y = {dist.y}");
            if (dist.x + dist.y < _tolerance){
                _exits[i].GetComponent<SpriteRenderer>().color = _players[i].GetComponent<MeshRenderer>().material.color;
            }
            else{
                _exits[i].GetComponent<SpriteRenderer>().color = Color.black;
                _allPlayerOnExit = false;
            }
        }
    }

    private void KickPlayerOut()
    {
        foreach (var player in _players)
        {
            player.transform.position += Time.deltaTime * _kickSpeed * Vector3.back;
        }
    }

}
