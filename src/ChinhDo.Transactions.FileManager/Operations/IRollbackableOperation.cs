namespace ChinhDo.Transactions.FileManager.Operations
{
    /// <summary>
    /// Represents a transactional operation.
    /// </summary>
    interface IRollbackableOperation : IRollbackable, IOperation { }
}
