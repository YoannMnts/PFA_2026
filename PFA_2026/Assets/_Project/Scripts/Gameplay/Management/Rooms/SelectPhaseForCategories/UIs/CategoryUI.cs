using Helteix.Tools.UI;
using Naussilus.Core;
using Naussilus.Core.Managements.ActionDatas;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Rooms
{
    public class CategoryUI : UIItem<Category>
    {
        private SelectNpcsForActionUI selectNpcsForActionUI;
        
        [SerializeField] private TMP_Text categoryName;
        [SerializeField] private Transform slotRoot;
        [SerializeField] private CategorySlot slotPrefab;
        
        private void Start()
        {
            selectNpcsForActionUI = GetComponentInParent<SelectNpcsForActionUI>();
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
            selectNpcsForActionUI.ChooseCategory(Current);
        }
    }
}