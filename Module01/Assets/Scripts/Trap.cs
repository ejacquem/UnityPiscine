using UnityEngine;

public class Trap : MonoBehaviour
{
    public enum Action {KILL, DIE};

    [SerializeField]
    private Action action;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trap hit {other.gameObject.name}");
        Player player = other.gameObject.GetComponent<Player>();
        if(player){
            if (action == Action.KILL)
                player.Kill();
            if (action == Action.DIE)
                player.Die();
        }
    }
}
