namespace Utils.Transactions
{
    /// <summary>
    /// Represents an operation that returns a value
    /// </summary>
    public interface IExecutable<T>
    {
        /// <summary>
        /// Executes the operation and returns a value
        /// </summary>
        /// <returns>Returns the output of the operation</returns>
        T Execute();
    }
}