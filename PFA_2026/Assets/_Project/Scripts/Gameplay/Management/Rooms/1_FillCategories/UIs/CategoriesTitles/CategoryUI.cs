using Helteix.Tools.UI;
using Naussilus.Core;
using Naussilus.Gameplay.CategoriesSlots;
using TMPro;
using UnityEngine;

namespace Naussilus.Gameplay.CategoriesTitles
{
    public class CategoryUI : UIItem<Category>
    {
        private FillCategoriesUI fillCategoriesUI;
        
        [SerializeField] private TMP_Text categoryName;
        [SerializeField] private CategorySlotUIList categorySlotUIList;
        
        private void Start()
        {
            fillCategoriesUI = GetComponentInParent<FillCategoriesUI>();
        }

        protected override void SyncUI(Category current)
        {
            if (current == null)
                return;
            
            categoryName.text = current.Name;
            //Debug.Log($"Category name: {current.Name}, current npcs : {Current.CurrentNpcs.Length}");

            MakeSlots(current);
            current.OnCategoryChanged += MakeSlots;
        }
        

        protected override void ClearUI()
        {
            if (Current == null)
                return;
                
            categoryName.text = string.Empty;
            ClearSlots();
            Current.OnCategoryChanged -= MakeSlots;
        }

        private void ClearSlots()
        {
            categorySlotUIList.Disconnect();
        }

        private void MakeSlots(Category category)
        {
            if (category == null || category != Current)
                return;
            
            ClearSlots();
            
            var categorySlot = Current.CategoryNpcSlots;
            Debug.Log($"Category: {category.Name} has {categorySlot.Length} Slots");
            categorySlotUIList.Connect(categorySlot);
        }

        public void OnClicked(Npc npc)
        {
            fillCategoriesUI.RemoveNpcInCategory(npc);
        }
    }
}