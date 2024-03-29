﻿/*
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
using System.Data;
using System.Threading;

namespace MySql.Data.MySqlClient
{
  public static class MySqlExtensions
  {
    public static byte[] GetBlob(this MySqlDataReader reader, int i)
    {
      int count = (int)reader.GetBytes(i, 0, null, 0, int.MaxValue);
      byte[] buffer = new byte[count];
      reader.GetBytes(i, 0, buffer, 0, count);
      return buffer;
    }

    public static Guid GetGuid(this MySqlDataReader reader, int i)
    {
      byte[] buffer = new byte[16];
      reader.GetBytes(i, 0, buffer, 0, buffer.Length);
      return new Guid(buffer);
    }

    public static void Add(this MySqlCommand command, string parameterName, object value)
    {
      command.Parameters.AddWithValue(parameterName, value);
    }

    public static bool ExecuteHasRows(this MySqlCommand command)
    {
      return ((long)command.ExecuteScalar()) > 0;
    }

    public static MySqlDataReader ExecuteReader(this MySqlConnection dbConnection, string commandString)
    {
      MySqlCommand command = new MySqlCommand(commandString, dbConnection);
      return command.ExecuteReader();
    }

    public static MySqlDataReader ExecuteReader(this MySqlConnection dbConnection, string commandString, string parameterName, object parameterValue)
    {
      MySqlCommand command = new MySqlCommand(commandString, dbConnection);
      command.Parameters.AddWithValue(parameterName, parameterValue);
      return command.ExecuteReader();
    }

    public static MySqlDataReader ExecuteReader(this MySqlConnection dbConnection, string commandString, string parameterName1, object parameterValue1, string parameterName2, object parameterValue2)
    {
      MySqlCommand command = new MySqlCommand(commandString, dbConnection);
      command.Parameters.AddWithValue(parameterName1, parameterValue1);
      command.Parameters.AddWithValue(parameterName2, parameterValue2);
      return command.ExecuteReader();
    }

    public static object ExecuteScalar(this MySqlConnection dbConnection, string commandString)
    {
      MySqlCommand command = new MySqlCommand(commandString, dbConnection);
      return command.ExecuteScalar();
    }

    public static object ExecuteScalar(this MySqlConnection dbConnection, string commandString, string parameterName, object parameterValue)
    {
      MySqlCommand command = new MySqlCommand(commandString, dbConnection);
      command.Parameters.AddWithValue(parameterName, parameterValue);
      return command.ExecuteScalar();
    }

    public static object ExecuteScalar(this MySqlConnection dbConnection, string commandString, string parameterName1, object parameterValue1, string parameterName2, object parameterValue2)
    {
      MySqlCommand command = new MySqlCommand(commandString, dbConnection);
      command.Parameters.AddWithValue(parameterName1, parameterValue1);
      command.Parameters.AddWithValue(parameterName2, parameterValue2);
      return command.ExecuteScalar();
    }

    public static void ExecuteNonQuery(this MySqlConnection dbConnection, string commandString)
    {
      MySqlCommand command = new MySqlCommand(commandString, dbConnection);
      command.ExecuteNonQuery();
    }

    public static void ExecuteNonQuery(this MySqlConnection dbConnection, string commandString, string parameterName, object parameterValue)
    {
      MySqlCommand command = new MySqlCommand(commandString, dbConnection);
      command.Parameters.AddWithValue(parameterName, parameterValue);
      command.ExecuteNonQuery();
    }

    public static void ExecuteNonQuery(this MySqlConnection dbConnection, string commandString, string parameterName1, object parameterValue1, string parameterName2, object parameterValue2)
    {
      MySqlCommand command = new MySqlCommand(commandString, dbConnection);
      command.Parameters.AddWithValue(parameterName1, parameterValue1);
      command.Parameters.AddWithValue(parameterName2, parameterValue2);
      command.ExecuteNonQuery();
    }

    public static bool ExecuteHasRows(this MySqlConnection dbConnection, string commandString)
    {
      MySqlCommand command = new MySqlCommand(commandString, dbConnection);
      return command.ExecuteHasRows();
    }

    public static bool ExecuteHasRows(this MySqlConnection dbConnection, string commandString, string parameterName, object parameterValue)
    {
      MySqlCommand command = new MySqlCommand(commandString, dbConnection);
      command.Parameters.AddWithValue(parameterName, parameterValue);
      return command.ExecuteHasRows();
    }

    public static bool ExecuteHasRows(this MySqlConnection dbConnection, string commandString, string parameterName1, object parameterValue1, string parameterName2, object parameterValue2)
    {
      MySqlCommand command = new MySqlCommand(commandString, dbConnection);
      command.Parameters.AddWithValue(parameterName1, parameterValue1);
      command.Parameters.AddWithValue(parameterName2, parameterValue2);
      return command.ExecuteHasRows();
    }
  }
}
