#region ENBREA UNTIS.XML - Copyright (C) STÜBER SYSTEMS GmbH
/*    
 *    ENBREA UNTIS.XML
 *    
 *    Copyright (C) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System;

namespace Enbrea.Untis.Xml
{
    /// <summary>
    /// Represents the Untis XML type "XmlReduction" 
    /// </summary>
    public class UntisReduction : UntisEntityWithShortName
    {
        public DateOnly? BeginDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string ReasonId { get; set; }
        public string TeacherId { get; set; }
        public string Text { get; set; }
        public double Value { get; set; }
    }
}
