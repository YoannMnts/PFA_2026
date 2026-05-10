using Helteix.Tools.UI;
using Naussilus.Core;
using UnityEngine;
using UnityEngine.UI;

public class NpcSlotUI : UIItem<Npc>
{
    [SerializeField] private Image icon;
    [SerializeField] private Button button;
        
    protected override void SyncUI(Npc current)
    {
        icon.sprite = current.CategoryIcon;
        button.onClick.AddListener(OnClick);
    }

    protected override void ClearUI()
    {
        icon.sprite = null;
        button.onClick.RemoveAllListeners();
    }

    public void OnClick()
    {
        Current.Selected();
    }
}