using System.Linq;
using Helteix.Tools.UI;
using Naussilus.Core;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Rooms
{
    public class CategoryUI : UIItem<Category>
    {
        private SelectNpcsForActionUI selectNpcsForActionUI;
        
        [SerializeField] private TMP_Text categoryName;
        [SerializeField] private CategorySlotUIList categorySlotUIList;
        
        private void Start()
        {
            selectNpcsForActionUI = GetComponentInParent<SelectNpcsForActionUI>();
        }

        protected override void SyncUI(Category current)
        {
            categoryName.text = current.Name;
            var npcs = current.CurrentNpcs ?? new Npc[current.Quantity];
            categorySlotUIList.Connect(npcs);
        }

        protected override void ClearUI()
        {
            categoryName.text = string.Empty;
            categorySlotUIList.Disconnect();
        }

        public void OnClicked(CategorySlotUI categorySlotUI)
        {
            var categorySlotUIs = categorySlotUIList.UIItems.ToArray();
            for (int i = 0; i < categorySlotUIs.Length; i++)
                if (categorySlotUIs[i] == categorySlotUI)
                    selectNpcsForActionUI.ChooseCategory(Current, i);
        }
    }
}