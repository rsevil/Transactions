namespace Utils.Transactions
{
    /// <summary>
    /// Represents a transactional operation that returns a value.
    /// </summary>
    public interface IRollbackableOperation<T> : IRollbackable, IOperation<T> { }
}