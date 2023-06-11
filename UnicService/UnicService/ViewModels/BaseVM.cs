using Newtonsoft.Json;

namespace UnicService.ViewModels
{
    public class BaseVM<T>
    {
        [JsonProperty]
        public T Id { get; set; }

        protected BaseVM() { }

        public BaseVM(T id) : this() { Id = id; }
    }
}
