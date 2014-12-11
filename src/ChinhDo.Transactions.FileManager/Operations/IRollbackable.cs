namespace ChinhDo.Transactions.FileManager.Operations
{
    /// <summary>
    /// Represents an object that can rollback something
    /// </summary>
    interface IRollbackable
    {
        /// <summary>
        /// Rolls backs, restores the original state.
        /// </summary>
        void Rollback();
    }
}
