using Helteix.Tools.Phases;
using Helteix.Tools.Phases.Listeners;
using Naussilus.Core;
using Naussilus.Core.Managers.Npcs;
using Naussilus.Core.NpcDatas;
using UnityEngine;
using UnityEngine.EventSystems;

public class ManagementNpc : MonoPhaseListener<ManagementPhase>, IPointerClickHandler
{
    [SerializeField] private NpcData npcData;
    
    private Vector2 lastPosition;
    
    public Npc Npc => NpcManager.TryGetNpc(npcData?.GUID);
    
    public void OnPointerClick(PointerEventData eventData)
    {
        CheckNpcState checkNpcPhase = new CheckNpcState(this);
        checkNpcPhase.RunAndForget();
    }

    protected override void OnPhaseBegin(ManagementPhase phase)
    {
        Npc.OnNewPosition += SetNewPosition;
        Npc.OnReturnLastPosition += ReturnLastPosition;
        base.OnPhaseBegin(phase);
    }

    protected override void OnPhaseEnd(ManagementPhase phase)
    {
        Npc.OnNewPosition -= SetNewPosition;
        Npc.OnReturnLastPosition -= ReturnLastPosition;
        base.OnPhaseEnd(phase);
    }

    private void SetNewPosition(Vector2 position)
    {
        lastPosition = gameObject.transform.position;
        gameObject.transform.position = position;
    }

    private void ReturnLastPosition()
    {
        gameObject.transform.position = lastPosition;
    }
}