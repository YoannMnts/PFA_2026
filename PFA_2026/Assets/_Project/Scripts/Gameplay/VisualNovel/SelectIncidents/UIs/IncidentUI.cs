using Helteix.Tools.UI;
using Naussilus.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Naussilus.Gameplay.VisualNovel.SelectIncidents.UIs
{
    public class IncidentUI : UIItem<Incident>
    {
        private SelectIncidentUI selectIncidentUI;

        [SerializeField]
        private TMP_Text nameText;
        
        [SerializeField]
        private Image iconImage;
        
        [SerializeField]
        private Button selectButton;
        
        private void Start()
        {
            selectIncidentUI = GetComponentInParent<SelectIncidentUI>();
        }

        protected override void SyncUI(Incident current)
        {
            nameText.text = current.Name;
            iconImage.sprite = current.Npcs[0].CategoryIcon;
            
            selectButton.onClick.AddListener(OnClick);
        }

        protected override void ClearUI()
        {
            nameText.text = string.Empty;
            iconImage.sprite = null;
            selectButton.onClick.RemoveAllListeners();
        }

        private void OnClick()
        {
            selectIncidentUI.OnIncidentSelected(Current);
        }
    }
}