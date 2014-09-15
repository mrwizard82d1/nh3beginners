namespace ch02
{
    /// <summary>
    /// A model for a product category.
    /// </summary>
    public class Category
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }
}
