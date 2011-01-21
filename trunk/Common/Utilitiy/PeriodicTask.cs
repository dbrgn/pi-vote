
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
using System.IO;
using System.Threading;

namespace Pirate.PiVote
{
  public enum PeriodicTaskState
  {
    Ready,
    Running,
    Stopping,
    Stopped,
    Faulted
  }

  public class PeriodicTask
  {
    private Thread thread;

    private Action periodicAction;

    private Action startAction;

    private Action stopAction;

    private Action faultAction;

    public int Delay { get; set; }

    public PeriodicTaskState State { get; private set; }

    public Exception Fault { get; private set; }

    public PeriodicTask(Action periodicAction)
      : this(null, periodicAction, null, null)
    { }

    public PeriodicTask(Action startAction, Action periodicAction, Action stopAction, Action faultAction)
    {
      if (periodicAction == null)
      {
        throw new ArgumentException("periodicAction");
      }

      this.periodicAction = periodicAction;
      this.startAction = startAction;
      this.stopAction = stopAction;
      this.faultAction = faultAction;

      State = PeriodicTaskState.Ready;
      Delay = 1;
    }

    public void Start()
    {
      switch (State)
      {
        case PeriodicTaskState.Ready:
          State = PeriodicTaskState.Running;
          this.thread = new Thread(Work);
          this.thread.Start();
          break;
        default:
          throw new InvalidOperationException("Cannot start in this state.");
      }

    }

    public void Stop()
    {
      switch (State)
      {
        case PeriodicTaskState.Running:
          State = PeriodicTaskState.Stopping;
          this.thread.Join();
          State = PeriodicTaskState.Stopped;
          break;
        case PeriodicTaskState.Stopping:
          this.thread.Join();
          State = PeriodicTaskState.Stopped;
          break;
        case PeriodicTaskState.Stopped:
        case PeriodicTaskState.Faulted:
          break;
        default:
          throw new InvalidOperationException("Cannot stop in this state.");
      }
    }

    private void Work()
    {
      if (this.startAction != null)
      {
        try
        {
          this.startAction();
        }
        catch (Exception exception)
        {
          Fault = exception;
          State = PeriodicTaskState.Faulted;
        }
      }

      while (State == PeriodicTaskState.Running)
      {
        try
        {
          this.periodicAction();

          Thread.Sleep(Delay);
        }
        catch (Exception exception)
        {
          Fault = exception;

          if (this.faultAction != null)
          {
            this.faultAction();
          }

          State = PeriodicTaskState.Faulted;
        }
      }

      if (this.stopAction != null)
      {
        try
        {
          this.stopAction();
        }
        catch (Exception exception)
        {
          Fault = exception;
          State = PeriodicTaskState.Faulted;
        }
      }
    }
  }
}
