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
    /// <summary>
    /// Represents the Untis XML type "XmlGeneralSettings"
    /// <summary>
    public class UntisGeneralSettings
    {
        public string SchoolName { get; set; }
        public uint SchoolNo { get; set; }
        public DateTime SchoolYearBeginDate { get; set; }
        public DateTime SchoolYearEndDate { get; set; }
        public DateTime TermBeginDate { get; set; }
        public DateTime TermEndDate { get; set; }
        public string TermName { get; set; }
    }
}


