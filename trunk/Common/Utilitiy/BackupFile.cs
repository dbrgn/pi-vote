/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Threading;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote
{
  public class BackupFile
  {
    private string dataPath;
    private string backupFile;
    private Thread thread;

    public Exception Exception { get; private set; }
    public bool Complete { get; private set; }
    public double Progress { get; private set; }

    public BackupFile(string dataPath, string backupFile)
    {
      this.dataPath = dataPath;
      this.backupFile = backupFile;
    }

    public void BeginCreate()
    {
      Complete = false;
      Exception = null;

      this.thread = new Thread(RunCreate);
      this.thread.Start();
    }

    public void BeginExtract()
    {
      Complete = false;
      Exception = null;

      this.thread = new Thread(RunExtract);
      this.thread.Start();
    }

    private void RunCreate()
    {
      try
      {
        Progress = 0;

        DirectoryInfo dataDirectory = new DirectoryInfo(this.dataPath);
        var files = dataDirectory.GetFiles("*", SearchOption.AllDirectories);
        long totalSize = files.Sum(file => file.Length);
        long doneSize = 0;

        FileStream fileStream = new FileStream(this.backupFile, FileMode.Create, FileAccess.Write);
        GZipStream gzipStream = new GZipStream(fileStream, CompressionMode.Compress);
        SerializeContext context = new SerializeContext(gzipStream);
        context.Write(files.Count());
        context.Write(totalSize);

        foreach (var file in files)
        {
          string fileName = file.FullName.Substring(this.dataPath.Length);
          if (fileName.StartsWith("\\"))
            fileName = fileName.Substring(1);

          context.Write(fileName);
          context.Write(File.ReadAllBytes(file.FullName));
          doneSize += file.Length;

          Progress = 1d / (double)totalSize * (double)doneSize;
        }

        context.Close();
        gzipStream.Close();
        fileStream.Close();
      }
      catch (Exception exception)
      {
        Exception = exception;
      }

      Complete = true;
    }

    private void RunExtract()
    {
      try
      {
        Progress = 0;

        FileStream fileStream = new FileStream(this.backupFile, FileMode.Open, FileAccess.Read);
        GZipStream gzipStream = new GZipStream(fileStream, CompressionMode.Decompress);
        DeserializeContext context = new DeserializeContext(gzipStream);

        int fileCount = context.ReadInt32();
        long totalSize = context.ReadInt64();
        long doneSize = 0;

        for (int index = 0; index < fileCount; index++)
        {
          string fileName = context.ReadString();
          byte[] data = context.ReadBytes();
          File.WriteAllBytes(Path.Combine(this.dataPath, fileName), data);
          doneSize += data.Length;

          Progress = 1d / (double)totalSize * (double)doneSize;
        }

        context.Close();
        gzipStream.Close();
        fileStream.Close();
      }
      catch (Exception exception)
      {
        Exception = exception;
      }

      Complete = true;
    }
  }
}
