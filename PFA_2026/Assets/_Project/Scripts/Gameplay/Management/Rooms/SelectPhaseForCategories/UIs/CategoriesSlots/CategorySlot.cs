using Naussilus.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rooms
{
    public class CategorySlot : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image iconImage;
        [SerializeField] private TMP_Text nameText;

        private CategoryUI categoryUI;
        private Category category;
        private int currentIndex;
        
        private void OnEnable()
        {
            button = GetComponent<Button>();
            categoryUI = GetComponentInParent<CategoryUI>();
            category = categoryUI.Current; 
        }
        

        public void SyncUI(int current)
        {
            ClearUI();
            currentIndex = current;
            iconImage.sprite = category.CurrentNpcs[current]?.CategoryIcon;
            nameText.text = category.CurrentNpcs[current]?.Name;
            
            button?.onClick.AddListener(OnClick);
        }

        public void ClearUI()
        {
            iconImage.sprite = null;
            nameText.text = string.Empty;
            button?.onClick.RemoveAllListeners();
        }

        public void OnClick()
        {
            categoryUI.OnClicked(currentIndex);
        }
    }
}