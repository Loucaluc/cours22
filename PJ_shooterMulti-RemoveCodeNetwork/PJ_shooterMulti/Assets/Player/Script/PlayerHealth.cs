using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour, Damagable {
    private PlayerController playerController;
    private ScoreDisplayer scoreDisplayer;
    private TeamManager teamManager;
    void Start() {
        playerController = GetComponent<PlayerController>();
        teamManager = GetComponent<TeamManager>();
    }

    public override void OnStartServer()
    {
        scoreDisplayer = FindObjectOfType<ScoreDisplayer>();
    }

    public void DealDamage(GameObject from, int damage) {

        CmdDealDamage(from, damage);
    }

    [Command]
    public void CmdDealDamage(GameObject from, int damage)
    {
        TeamManager FromTeamManager = from.GetComponent<TeamManager>();

        if (FromTeamManager != null)
        {
            if (scoreDisplayer != null)
            {
                if (FromTeamManager.GetTeam() != teamManager.GetTeam())
                {
                    scoreDisplayer.RpcAddKill(FromTeamManager.GetTeam());
                }
            }
        }
        playerController.RpcDie();
    }
}
