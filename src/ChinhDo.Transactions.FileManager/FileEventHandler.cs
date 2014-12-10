namespace ChinhDo.Transactions.FileManager
{
    /// <summary>Delegate to call when a new found is found.</summary>
    public delegate void FileEventHandler(string fileName, ref bool cancel);
}
