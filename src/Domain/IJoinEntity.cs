namespace BigSolution.Infra.Domain
{
    public interface IJoinEntity<TEntity>
        //where TEntity : class//, IEntity
    {
        TEntity Navigation { get; set; }
    }
}