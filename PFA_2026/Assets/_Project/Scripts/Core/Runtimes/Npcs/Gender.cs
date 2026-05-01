using Naussilus.Core.NpcDatas;

namespace Naussilus.Core
{
    public struct Gender : INpcSelector
    {
        public EGender EGender { get; private set; }

        public Gender(GenderData data)
        {
            EGender = data.EGender;
        }
    }
}