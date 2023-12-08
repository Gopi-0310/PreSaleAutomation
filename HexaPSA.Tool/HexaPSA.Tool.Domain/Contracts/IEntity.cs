namespace HexaPSA.Tool.Domain.Contracts
{
    public interface IEntity<T>:IBaseEntity
    {
        public T Id { get; set; }
    }
   
}
