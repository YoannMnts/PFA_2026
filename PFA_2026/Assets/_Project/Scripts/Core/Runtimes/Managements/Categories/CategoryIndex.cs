using Naussilus.Core.Managements.ActionDatas;

namespace Naussilus.Core
{
    public class CategoryIndex : INpcSelector
    {
        public int Index { get; private set; }

        public CategoryIndex(CategoryIndexData data)
        {
            Index = data.Index;
        }
    }
}