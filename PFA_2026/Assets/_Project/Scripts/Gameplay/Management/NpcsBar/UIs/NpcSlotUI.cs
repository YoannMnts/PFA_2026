using Helteix.Tools.UI;
using Naussilus.Core;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class NpcSlotUI : UIItem<Npc>
{
    [SerializeField] private Image icon;
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text npcName;
        
    protected override void SyncUI(Npc current)
    {
        icon.sprite = current.CategoryIcon;
        npcName.text = current.Name;
        button.onClick.AddListener(OnClick);
    }

    protected override void ClearUI()
    {
        icon.sprite = null;
        npcName.text = string.Empty;
        button.onClick.RemoveAllListeners();
    }

    public void OnClick()
    {
        Current.Selected();
    }
}