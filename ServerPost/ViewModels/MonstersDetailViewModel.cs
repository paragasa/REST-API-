using ServerPost.Models;

namespace ServerPost.ViewModels
{
    public class MonstersDetailViewModel : BaseViewModel
    {
        public Monster Data { get; set; }
        public MonstersDetailViewModel(Monster data = null)
        {
            Title = data?.Name;
            Data = data;
        }
    }
}
