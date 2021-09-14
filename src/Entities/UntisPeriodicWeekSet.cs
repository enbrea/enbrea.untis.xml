#region ENBREA UNTIS.XML - Copyright (C) 2021 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA UNTIS.XML
 *    
 *    Copyright (C) 2021 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System;

namespace Enbrea.Untis.Xml
{
    [Flags]
    public enum UntisPeriodicWeekSet
    {
        WeekA = 1 << 1,
        WeekB = 1 << 2,
        WeekC = 1 << 3,
        WeekD = 1 << 4,
        WeekE = 1 << 5,
        WeekF = 1 << 6,
        WeekG = 1 << 7,
        WeekH = 1 << 8,
        WeekI = 1 << 9,
        WeekJ = 1 << 10,
        WeekK = 1 << 11,
        WeekL = 1 << 12,
        WeekM = 1 << 13,
        WeekN = 1 << 14,
        WeekO = 1 << 15,
        WeekP = 1 << 16
    }
}

