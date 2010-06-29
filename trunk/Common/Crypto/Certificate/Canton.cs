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
using System.Security.Cryptography;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  public enum Canton
  {
    None = 0,
    ZH = 1,
    BE = 2,
    LU = 3,
    UR = 4,
    SZ = 5,
    OW = 6,
    NW = 7,
    GL = 8,
    ZG = 9,
    FR = 10,
    SO = 11,
    BS = 12,
    BL = 13,
    SH = 14,
    AR = 15,
    AI = 16,
    SG = 17,
    GR = 18,
    AG = 19,
    TG = 20,
    TI = 21,
    VD = 22,
    VS = 23,
    NE = 24,
    GE = 25,
    JU = 26
  }

  public static class CantonExtensions
  {
    public static string Text(this Canton canton)
    {
      switch (canton)
      { 
        case Canton.ZH:
          return LibraryResources.cantonZH;
        case Canton.BE:
          return LibraryResources.cantonBE;
        case Canton.LU:
          return LibraryResources.cantonLU;
        case Canton.UR:
          return LibraryResources.cantonUR;
        case Canton.SZ:
          return LibraryResources.cantonSZ;
        case Canton.OW:
          return LibraryResources.cantonOW;
        case Canton.NW:
          return LibraryResources.cantonNW;
        case Canton.GL:
          return LibraryResources.cantonGL;
        case Canton.ZG:
          return LibraryResources.cantonZG;
        case Canton.FR:
          return LibraryResources.cantonFR;
        case Canton.SO:
          return LibraryResources.cantonSO;
        case Canton.BS:
          return LibraryResources.cantonBS;
        case Canton.BL:
          return LibraryResources.cantonBL;
        case Canton.SH:
          return LibraryResources.cantonSH;
        case Canton.AR:
          return LibraryResources.cantonAR;
        case Canton.AI:
          return LibraryResources.cantonAI;
        case Canton.SG:
          return LibraryResources.cantonSG;
        case Canton.GR:
          return LibraryResources.cantonGR;
        case Canton.AG:
          return LibraryResources.cantonAG;
        case Canton.TG:
          return LibraryResources.cantonTG;
        case Canton.TI:
          return LibraryResources.cantonTI;
        case Canton.VD:
          return LibraryResources.cantonVD;
        case Canton.VS:
          return LibraryResources.cantonVS;
        case Canton.NE:
          return LibraryResources.cantonNE;
        case Canton.GE:
          return LibraryResources.cantonGE;
        case Canton.JU:
          return LibraryResources.cantonJU;
        default:
          return LibraryResources.cantonNone;
      }
    }
  }
}
