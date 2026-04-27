using System;
using Helteix.Tools.UI;
using Naussilus.Core.Managements.RoomDatas.ActionDatas.Categorys;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Rooms
{
    public class ActionCategoryUI : UIItem<Category>
    {
        private SelectCategoryForActionUI selectCategoryForActionUI;
        
        [SerializeField] private TMP_Text categoryName;
        [SerializeField] private Transform slotRoot;
        [SerializeField] private CategorySlot slotPrefab;
        
        private void Start()
        {
            selectCategoryForActionUI = GetComponentInParent<SelectCategoryForActionUI>();
        }

        protected override void SyncUI(Category current)
        {
            categoryName.text = current.Name;
            for (int i = 0; i < current.Quantity; i++)
            {
                var slot = Instantiate(slotPrefab, slotRoot);
                slot.Button.onClick.AddListener(OnClicked);
            }
        }

        protected override void ClearUI()
        {
            categoryName.text = string.Empty;
            foreach (Transform slot in slotRoot)
            {
                Destroy(slot.gameObject);
            }
        }

        private void OnClicked()
        {
            selectCategoryForActionUI.ChooseCategory(Current);
        }
    }
}