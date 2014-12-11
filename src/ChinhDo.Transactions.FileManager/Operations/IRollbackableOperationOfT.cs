namespace ChinhDo.Transactions.FileManager.Operations
{
    /// <summary>
    /// Represents a transactional operation that returns a value.
    /// </summary>
    interface IRollbackableOperation<T> : IRollbackable, IOperation<T> { }
}
