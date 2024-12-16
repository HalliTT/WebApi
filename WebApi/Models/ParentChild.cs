namespace WebApi.Models
{
    public class ParentChild
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int ChildId { get; set; }

        public virtual User Parent { get; set; }
        public virtual User Child { get; set; }
    }
}
