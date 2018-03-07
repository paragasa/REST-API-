
using SQLite;

namespace ServerPost.Models
{
    public class Character : Entity<Item>
    {
        public int Level { get; set; }

        public void Update(Character newData)
        {
            if (newData == null)
            {
                return;
            }

            // Update all the fields in the Data, except for the Id
            Name = newData.Name;
            Description = newData.Description;
            Level = newData.Level;
        }

    }
}