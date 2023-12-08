using HexaPSA.Tool.Domain.Contracts;

namespace HexaPSA.Tool.Domain.Entities
{
    public class EntityBase<T> : IEntity<T>
    {
        public T Id { get; set; }
        public EntityBase() { }
        public EntityBase(T id) { }

        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid ModifiedBy { get; set; }
        public bool IsDestroyed { get; set; }
    }
}
