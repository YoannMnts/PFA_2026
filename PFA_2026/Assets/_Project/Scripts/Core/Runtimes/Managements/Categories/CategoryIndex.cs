using Naussilus.Core.Managements.ActionDatas;

namespace Naussilus.Core
{
    public struct CategoryIndex : INpcSelector
    {
        public int Index { get; private set; }

        public CategoryIndex(CategoryIndexData data)
        {
            Index = data.Index;
        }
    }
}