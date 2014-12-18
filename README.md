# Transactional File Manager #

## Project Description ##

Transactional File Manager is a .NET API that supports including file system operations such as file copy, move, delete, append, etc. in a transaction. It's an implementation of System.Transaction.IEnlistmentNotification (works with System.Transactions.TransactionScope).

This library allows you to wrap file system operations in transactions like this: 

``` csharp
// Wrap a file copy and a database insert in the same transaction
TxFileManager fileMgr = new TxFileManager();
using (TransactionScope scope1 = new TransactionScope())
{
	// Copy a file
	fileMgr.Copy(srcFileName, destFileName);

	// Insert a database record
	dbMgr.ExecuteNonQuery(insertSql);

	scope1.Complete();
}
```


## Current Features ##

- Support the following file operations in transactions:
	- AppendAllText
	- Copy
	- CreateDirectory
	- DeleteDirectory
	- DeleteFile
	- Move
	- Snapshot
	- WriteAllText
	- WriteAllBytes

This library supports any file system and is not a wrapper over Transactional NTFS (see [AlphaFS](http://alphafs.codeplex.com/)).

## Examples ##
``` csharp
// Completely unrealistic example showing how various file operations, including operations done 
// by library/3rd party code, can participate in transactions.
IFileManager fileManager = new TxFileManager();
using (TransactionScope scope1 = new TransactionScope())
{
    fileManager.WriteAllText(inFileName, xml);

    // Snapshot allows any file operation to be part of our transaction.
    // All we need to know is the file name.
    //The statement below tells the TxFileManager to remember the state of this file.
    // So even though XslCompiledTransform has no knowledge of our TxFileManager, the file it creates (outFileName)
    // will still be restored to this state in the event of a rollback.
    fileManager.Snapshot(outFileName);
    XslCompiledTransform xsl = new XslCompiledTransform(true);
    xsl.Load(uri);
    xsl.Transform(inFileName, outFileName);

    // write to database 1. This database op will get committed/rolled back along with the file operations we are doing in this transaction.
    myDb1.ExecuteNonQuery(sql1);

    // write to database 2. The transaction is promoted to a distributed transaction here.
    myDb2.ExecuteNonQuery(sql2);

    // let's delete some files
    for (string fileName in filesToDelete)
    {
        fileManager.Delete(fileName);
    }

    // Just for kicks, let's start a new nested  transaction. Since we specify RequiresNew here, this nested transaction
    // will be committed/rolled back separately from the main transaction.
    // Note that we can still use the same fileManager instance. It knows how to sort things out correctly.
    using (TransactionScope scope2 = new TransactionScope(TransactionScopeOptions.RequiresNew))
    {
        fileManager.MoveFile(anotherFile, anotherFileDest);
    }

    // move some files
    for (string fileName in filesToMove)
    {
        fileManager.Move(fileName, GetNewFileName(fileName));
    }

    // Finally, let's create a few temporary files...
    // disk space has to be used for something.
    // The nice thing about FileManager.GetTempFileName is that
    // The temp file will be cleaned up automatically for you when the TransactionScope completes.
    // No more worries about temp files that get left behind.
    for (int i=0; i<10; i++)
    {
        fileManager.WriteAllText(fileManager.GetTempFileName(), "testing 1 2");
    }

    scope1.Complete();
    // In the event an exception occurs, everything done here will be rolled back including the output xsl file.
}
```


This is an open source project. The original project web site is [transactionalfilemgr](https://transactionalfilemgr.codeplex.com).

Copyright (c) 2008-2013 Chinh Do

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
