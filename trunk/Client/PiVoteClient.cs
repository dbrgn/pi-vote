/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Thomas Bruderer <apophis@apophis.ch>
 *  <BSD Like license>
 */

using Gtk;
using System;
using System.Collections.Generic;

namespace Pirate.PiVote.Client
{
    /// <summary>
    /// Description of MyClass.
    /// </summary>
    public class ClientMain
    {
        static void Main()
        {
          new Pirate.PiVote.Crypto.Test().Do();
          return;

            Application.Init ();
            
            // Set up a button object.
            Button btn = new Button ("Hello World");
            // when this button is clicked, it'll run hello()
            btn.Clicked += new EventHandler (hello);
            
            Window window = new Window ("helloworld");
            // when this window is deleted, it'll run delete_event()
            window.DeleteEvent += delete_event;
            
            // Add the button to the window and display everything
            window.Add (btn);
            window.ShowAll ();
            
            Application.Run ();
        }
        
        
        // runs when the user deletes the window using the "close
        // window" widget in the window frame.
        static void delete_event (object obj, DeleteEventArgs args)
        {
            Application.Quit ();
        }
        
        // runs when the button is clicked.
        static void hello (object obj, EventArgs args)
        {
            Console.WriteLine("Hello World");
            Application.Quit ();
        }
    }
}