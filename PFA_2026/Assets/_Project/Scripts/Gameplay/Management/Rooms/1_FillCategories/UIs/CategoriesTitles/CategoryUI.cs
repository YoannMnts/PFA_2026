using Helteix.Tools.UI;
using Naussilus.Core;
using Naussilus.Core.Managers.Rooms;
using Naussilus.Gameplay.CategoriesSlots;
using TMPro;
using UnityEngine;

namespace Naussilus.Gameplay.CategoriesTitles
{
    public class CategoryUI : UIItem<Category>
    {
        private FillCategoriesUI fillCategoriesUI;
        
        [SerializeField] private TMP_Text categoryName;
        [SerializeField] private CategorySlot categorySlotPrefab;
        [SerializeField] private Transform categorySlotRoot;
        
        private void Start()
        {
            fillCategoriesUI = GetComponentInParent<FillCategoriesUI>();
        }

        private void OnDisable()
        {
            if (Current == null)
                return;
            
            RoomCategoryManager.OnNpcAdded -= MakeSlots;
            RoomCategoryManager.OnNpcRemove -= MakeSlots;
        }

        protected override void SyncUI(Category current)
        {
            categoryName.text = current.Name;
            Debug.Log($"Category name: {current.Name}, current npcs : {Current.CurrentNpcs.Length}");

            MakeSlots(current);
            RoomCategoryManager.OnNpcAdded += MakeSlots;
            RoomCategoryManager.OnNpcRemove += MakeSlots;
        }
        

        protected override void ClearUI()
        {
            categoryName.text = string.Empty;
            ClearSlots();
        }

        private void ClearSlots()
        {
            foreach (Transform slot in categorySlotRoot)
            {
                Destroy(slot.gameObject);
            }
        }

        private void MakeSlots(Category category)
        {
            if (category == null || category != Current)
                return;
            
            ClearSlots();
            for (int i = 0; i < category.CurrentNpcs.Length; i++)
            {
                var categorySlot = Instantiate(categorySlotPrefab, categorySlotRoot);
                categorySlot.SyncUI(i);
            }
        }

        public void OnClicked(int index)
        {
            fillCategoriesUI.RemoveNpcInCategory(Current, index);
        }
    }
}