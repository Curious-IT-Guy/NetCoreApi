using System;

namespace NetCoreApi.Data.Models
{
    public partial class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public TodoState TodoState { get; set; }
    }
}
