using System;
using Helteix.Tools.UI;
using Naussilus.Core.Managements.RoomDatas.ActionDatas.Categorys;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Rooms
{
    public class ActionCategoryUI : UIItem<Category>
    {
        private SelectCategoryForActionUI selectCategoryForActionUI;
        
        [SerializeField] private string categoryName;
        [SerializeField] private Transform slotPrefab;
        
        private void Start()
        {
            selectCategoryForActionUI = GetComponentInParent<SelectCategoryForActionUI>();
        }

        protected override void SyncUI(Category current)
        {
            categoryName = current.Name;
        }

        protected override void ClearUI()
        {
            categoryName = string.Empty;
        }
    }
}