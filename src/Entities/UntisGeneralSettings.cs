#region ENBREA UNTIS.XML - Copyright (C) 2023 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA UNTIS.XML
 *    
 *    Copyright (C) 2023 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System;

namespace Enbrea.Untis.Xml
{
    /// <summary>
    /// Represents the Untis XML type "XmlGeneralSettings"
    /// <summary>
    public class UntisGeneralSettings
    {
        public string SchoolName { get; set; }
        public uint SchoolNo { get; set; }
        public DateOnly SchoolYearBeginDate { get; set; }
        public DateOnly SchoolYearEndDate { get; set; }
        public DateOnly TermBeginDate { get; set; }
        public DateOnly TermEndDate { get; set; }
        public string TermName { get; set; }
    }
}


