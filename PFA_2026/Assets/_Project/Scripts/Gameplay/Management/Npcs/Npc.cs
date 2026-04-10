using _Project.Scripts.NpcMenus.CheckNpcStats;
using Helteix.Tools.Phases;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.NpcMenus
{
    public class Npc : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            CheckNpcScore checkNpcPhase = new CheckNpcScore();
            checkNpcPhase.RunAndForget();
        }
    }
}