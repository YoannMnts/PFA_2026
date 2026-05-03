using System;
using Helteix.Tools.UI;
using Naussilus.Core;
using TMPro;
using UnityEngine;

namespace Rooms
{
    public class CategoryUI : UIItem<Category>
    {
        private SelectNpcsForActionUI selectNpcsForActionUI;
        
        [SerializeField] private TMP_Text categoryName;
        [SerializeField] private CategorySlot categorySlotPrefab;
        [SerializeField] private Transform categorySlotRoot;
        
        private void Start()
        {
            selectNpcsForActionUI = GetComponentInParent<SelectNpcsForActionUI>();
        }

        private void OnDisable()
        {
            if (Current == null)
                return;
            
            Current.OnNpcAdded -= MakeSlots;
            Current.OnNpcClear -= MakeSlots;
        }

        protected override void SyncUI(Category current)
        {
            categoryName.text = current.Name;
            Debug.Log($"Category name: {current.Name}, current npcs : {Current.CurrentNpcs.Count}");

            MakeSlots(current);
            Current.OnNpcAdded += MakeSlots;
            Current.OnNpcClear += MakeSlots;
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

        private void MakeSlots(Category current)
        {
            if (current == null)
                return;
            
            ClearSlots();
            for (int i = 0; i < current.CurrentNpcs.Count; i++)
            {
                var categorySlot = Instantiate(categorySlotPrefab, categorySlotRoot);
                categorySlot.SyncUI(i);
            }
        }

        public void OnClicked(int index)
        {
            selectNpcsForActionUI.ChooseCategory(Current, index);
        }
    }
}