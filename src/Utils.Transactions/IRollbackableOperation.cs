namespace Utils.Transactions
{
    /// <summary>
    /// Represents a transactional operation.
    /// </summary>
    public interface IRollbackableOperation : IRollbackable, IOperation { }
}