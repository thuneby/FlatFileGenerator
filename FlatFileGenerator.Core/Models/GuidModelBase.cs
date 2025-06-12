namespace FlatFileGenerator.Core.Models
{
    public abstract class GuidModelBase : Entity<Guid>
    {
        public GuidModelBase()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }
    }
}
