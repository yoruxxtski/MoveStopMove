using UnityEngine;

public class PlayerDance : MonoBehaviour
{
    private Player_Stats player_Stats;
    private PlayerAnimation playerAnimation;
    void Awake()
    {
        player_Stats = GetComponent<Player_Stats>();
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
    }

    void OnEnable()
    {
        GameStateManager.potentialGameEnd += PlayerWin;
    }

    void OnDisable()
    {
        GameStateManager.potentialGameEnd -= PlayerWin;
    }

    public void PlayerWin() {
        if (player_Stats.GetAliveState()) {
            playerAnimation.SetWinAnimation(true);
        }
    }
}