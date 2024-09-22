using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_State_FreeRoam : NPCState
{
    private Vector3 target;

    public NPC_State_FreeRoam(NPC _npc, NPCStateMachine _npcStateMachine) : base(_npc, _npcStateMachine)
    {
    }

    public override void AnimationTriggerEvent(NPC.AnimationTriggerType _triggerType)
    {
        base.AnimationTriggerEvent(_triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        SetNewRandomTarget();
    }

    public override void ExitState()
    {
        base.ExitState();
        npc.agent.ResetPath();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        // Check if NPC has reached the target
        if (!npc.agent.pathPending && npc.agent.remainingDistance <= npc.agent.stoppingDistance)
        {
            // Set a new random target
            SetNewRandomTarget();
        }
    }

    private void SetNewRandomTarget()
    {
        target = GetRandomPositionWithinNavMeshArea();
        npc.MoveTo(target);
    }

    private Vector3 GetRandomPositionWithinNavMeshArea() 
    {

        Vector3 randomDirection = Random.insideUnitSphere * 15;
        randomDirection += npc.transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 15, npc.agent.areaMask))
        {
            return hit.position;
        }

        Debug.LogError("Couldn't find a valid position within the NavMesh area.");
        return npc.transform.position; // Eðer geçerli bir konum bulunamazsa, mevcut konumu döndür
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
