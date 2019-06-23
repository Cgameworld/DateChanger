using ColossalFramework.IO;

namespace DateChanger
{
    public static class SavedValues
    {
        public static long savedOffset = 0;
        
    public class Data : IDataContainer
        {

            public void Serialize(DataSerializer s)
            {
                    s.WriteLong64(savedOffset);
            }

            public void Deserialize(DataSerializer s)
            {
              
                    savedOffset = s.ReadLong64();
            }
            public void AfterDeserialize(DataSerializer s)
            {
            }

        }


        
    }
}
