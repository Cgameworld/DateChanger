using ICities;

namespace DateChanger
{
    public class ModListing : IUserMod
    {
        public string Name
        {
            get { return "Date Changer"; }
        }

        public string Description
        {
            get { return "Change the displayed ingame date"; }
        }


    }
}