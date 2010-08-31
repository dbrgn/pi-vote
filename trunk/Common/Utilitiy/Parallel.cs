/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace System
{
  public static class Parallel
  {
    public static List<TOutput> Work<TInput, TOutput>(Func<TInput, ProgressHandler, TOutput> function, IEnumerable<TInput> inputs, ProgressHandler progressHandler)
    {
      if (function == null)
        throw new ArgumentNullException("function");
      if (inputs == null)
        throw new ArgumentNullException("inputs");

      ParallelWorker<TInput, TOutput> parallel = new ParallelWorker<TInput, TOutput>(function, inputs);
      parallel.Start();

      while (!parallel.Complete)
      {
        if (progressHandler != null)
        {
          progressHandler(parallel.Progress);
        }

        Thread.Sleep(10);
      }

      return parallel.Results;
    }

    /// <summary>
    /// Execute some uniform work in parallel.
    /// </summary>
    /// <typeparam name="TInput">Type of the task input.</typeparam>
    /// <typeparam name="TOutput">Type of the task output.</typeparam>
    private class ParallelWorker<TInput, TOutput>
    {
      private List<Thread> workers;
      private Func<TInput, ProgressHandler, TOutput> function;
      private Queue<Tuple<int, TInput>> inputs;
      private SortedList<int, TOutput> outputs;
      private Dictionary<int, double> progress;
      private int workCount;
      private int workDone;

      /// <summary>
      /// Results of the work.
      /// </summary>
      public List<TOutput> Results
      {
        get { return new List<TOutput>(this.outputs.Values); }
      }

      /// <summary>
      /// Creates a new parallel execution.
      /// </summary>
      /// <param name="function">Function to apply to all input.</param>
      /// <param name="inputs">List of all input.</param>
      public ParallelWorker(Func<TInput, ProgressHandler, TOutput> function, IEnumerable<TInput> inputs)
      {
        this.function = function;

        this.inputs = new Queue<Tuple<int, TInput>>();
        for (int index = 0; index < inputs.Count(); index++)
        {
          this.inputs.Enqueue(new Tuple<int, TInput>(index, inputs.ElementAt(index)));
        }

        this.workCount = this.inputs.Count;
        this.workDone = 0;
        this.outputs = new SortedList<int, TOutput>();
        this.workers = new List<Thread>();
        this.progress = new Dictionary<int, double>();

        int threadCount = Math.Min(Environment.ProcessorCount, this.inputs.Count);
        threadCount.Times(() => this.workers.Add(new Thread(Worker)));
      }

      /// <summary>
      /// Start the execution.
      /// </summary>
      public void Start()
      {
        this.workers.ForEach(worker => worker.Priority = ThreadPriority.Lowest);
        this.workers.ForEach(worker => worker.Start());
      }

      /// <summary>
      /// Is the execution complete?
      /// </summary>
      public bool Complete
      {
        get { return this.workers.All(worker => worker.ThreadState == ThreadState.Stopped); }
      }

      /// <summary>
      /// Progress made in execution.
      /// </summary>
      public double Progress
      {
        get
        {
          lock (this.progress)
          {
            return 1d / (double)this.workCount * ((double)this.workDone + this.progress.Values.Sum());
          }
        }
      }

      /// <summary>
      /// Does the actual work.
      /// Runs in its own thread.
      /// </summary>
      private void Worker()
      {
        lock (this.progress)
        {
          this.progress.Add(Thread.CurrentThread.ManagedThreadId, 0d);
        }

        Tuple<int, TInput> input = null;
        bool done = false;

        while (!done)
        {
          lock (this.inputs)
          {
            if (this.inputs.Count > 0)
            {
              input = this.inputs.Dequeue();
            }
            else
            {
              done = true;
            }
          }

          if (input != null)
          {
            TOutput output = this.function(input.Second, WorkerProgressHandler);

            lock (this.progress)
            {
              this.progress[Thread.CurrentThread.ManagedThreadId] = 0d;
              this.workDone++;
            }

            lock (this.outputs)
            {
              this.outputs.Add(input.First, output);
            }

            input = null;
          }
        }
      }

      /// <summary>
      /// Handles the process made by a worker.
      /// </summary>
      /// <param name="value">Value of the progress made.</param>
      private void WorkerProgressHandler(double value)
      {
        lock (this.progress)
        {
          this.progress[Thread.CurrentThread.ManagedThreadId] = value;
        }
      }
    }
  }
}
