namespace ChinhDo.Transactions.FileManager.Operations
{
    /// <summary>
    /// Represents an operation that returns a value
    /// </summary>
    interface IOperation<T>
    {
        /// <summary>
        /// Executes the operation and returns a value
        /// </summary>
        /// <returns>Returns the output of the operation</returns>
        T Execute();
    }
}
