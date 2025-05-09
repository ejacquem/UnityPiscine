using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public List<Player> players;
    public Player currentPlayer;
    public int playerIndex;

    public Transform mainCamera;
    private Vector3 _prevCameraPos;
    private Vector2 _input;

    public float timeToswitch;
    public float switchTime;

    public Transform overviewPosition;
    public bool overviewEnable = false;

    void Start()
    {
        SelectPlayer(playerIndex);
        _prevCameraPos = mainCamera.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (overviewEnable)
            FollowPosition(overviewPosition.position);
        else if (!currentPlayer.IsDead())
            FollowPosition(currentPlayer.transform.position);
        
        if (Input.GetKeyDown(KeyCode.E))
            SelectPlayer(++playerIndex);
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectPlayer(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectPlayer(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SelectPlayer(2);
        if (Input.GetKeyDown(KeyCode.V)){
            if (overviewEnable)
                SelectPlayer(playerIndex);
            else {
                overviewEnable = true;
                _prevCameraPos = mainCamera.position;
                switchTime = timeToswitch;
            }
        }
    }

    void SelectPlayer(int index)
    {
        overviewEnable = false;
        _prevCameraPos = mainCamera.position;
        switchTime = timeToswitch;
        if (currentPlayer != null){
            currentPlayer.SetInput(new Vector2(0, 0));
        }
        playerIndex = index % players.Count;
        currentPlayer = players[playerIndex];
        currentPlayer.SetInput(_input);
    }

    Player GetPlayerAt(int index)
    {
        return players[(index + players.Count * 100) % players.Count];
    }

    void FollowPosition(Vector3 pos)
    {
        switchTime = Mathf.Max(switchTime - Time.deltaTime, 0f);
        mainCamera.position = Vector3.Lerp(pos, _prevCameraPos,  Mathf.SmoothStep(0f, 1f, switchTime / timeToswitch));
    }

    private void OnMove(InputValue value)
    {
        // Debug.Log("Onmove called");
        _input = value.Get<Vector2>();
        currentPlayer.SetInput(_input);
    }

    private void OnJump()
    {
        currentPlayer.Jump();
    }

    public bool IsAPlayerDead()
    {
        foreach (var player in players)
        {
            if (player.IsDead())
                return true;
        }
        return false;
    }
}
