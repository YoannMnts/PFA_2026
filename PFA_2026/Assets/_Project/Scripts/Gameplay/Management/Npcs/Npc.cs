using Helteix.Tools.Phases;
using Naussilus.Core.NpcDatas;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts
{
    public class Npc : MonoBehaviour, IPointerClickHandler
    {
        [field: SerializeField] 
        public NpcData NpcData { get; private set; }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            CheckNpcState checkNpcPhase = new CheckNpcState(this);
            checkNpcPhase.RunAndForget();
        }
    }
}