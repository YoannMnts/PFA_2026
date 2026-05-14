using Helteix.Tools.UI;
using Naussilus.Core;
using TMPro;
using UnityEngine;

public class BehaviorUI : UIItem<Behavior>
{
    [SerializeField]
    private TMP_Text behaviorText;
        
    [SerializeField]
    private TMP_Text valueText;
    protected override void SyncUI(Behavior current)
    {
        behaviorText.text = current.Name;
        valueText.text = current.Amount.ToString();
    }

    protected override void ClearUI()
    {
        behaviorText.text = string.Empty;
        valueText.text = string.Empty;
    }
}