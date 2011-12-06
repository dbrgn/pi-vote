/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;

namespace System
{
    /// <summary>
    /// Description of SystemExtensions.
    /// </summary>
    public static class SystemExtensions
    {
        public static bool IsNull(this object extendetObject) {
            return extendetObject == null;
        }
        
        public static bool IsNullOrEmpty(this string extendetString) {
            return String.IsNullOrEmpty(extendetString);
        }
    }
}
