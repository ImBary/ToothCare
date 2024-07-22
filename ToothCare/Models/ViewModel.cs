using ToothCare.Models;

namespace ToothCare.Models
{
    public class ViewModel<T> where T : class
    {
        public List<T> lists { get; set; }
        public Pagination pagination { get; set; }

    }
}
