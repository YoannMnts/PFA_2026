using System.Linq;
using Helteix.Tools.UI;
using Naussilus.Core;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Rooms
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

        protected override void SyncUI(Category current)
        {
            categoryName.text = current.Name;
            Debug.Log($"Category name: {current.Name}, current npcs : {Current.CurrentNpcs.Length}");

            CreateSlots(current);
            
        }

        private void CreateSlots(Category current)
        {
            for (int i = 0; i < current.CurrentNpcs.Length; i++)
            {
                var categorySlot = Instantiate(categorySlotPrefab, categorySlotRoot);
                categorySlot.SyncUI(i);
            }
        }

        protected override void ClearUI()
        {
            categoryName.text = string.Empty;
            foreach (Transform slot in categorySlotRoot)
            {
                Destroy(slot.gameObject);
            }
        }

        public void OnClicked(int index)
        {
            selectNpcsForActionUI.ChooseCategory(Current, index);
        }
    }
}