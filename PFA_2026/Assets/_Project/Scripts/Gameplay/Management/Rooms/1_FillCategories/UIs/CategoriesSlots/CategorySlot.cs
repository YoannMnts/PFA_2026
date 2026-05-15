using System.Linq;
using Naussilus.Core;
using Naussilus.Gameplay.CategoriesTitles;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Naussilus.Gameplay.CategoriesSlots
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
            Npc categoryCurrentNpc = category.CurrentNpcs[current];
            iconImage.sprite = categoryCurrentNpc?.CategoryIcon;
            nameText.text = categoryCurrentNpc?.Name;

            if (category.ObligateNpcs.Contains(categoryCurrentNpc))
            {
                button.interactable = false;
                return;
            }
            
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