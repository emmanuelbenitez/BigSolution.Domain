namespace BigSolution.Infra.Domain
{
    public interface IJoinEntity<TEntity>
        where TEntity : class
    {
        TEntity Navigation { get; set; }
    }
}
