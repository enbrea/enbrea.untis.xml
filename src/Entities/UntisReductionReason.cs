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
    /// Represents the Untis XML type "XmlReductionReason" 
    /// </summary>
    public class UntisReductionReason : UntisEntityWithShortName
    {
        public string BackgroundColor { get; set; }
        public string Description { get; set; }
        public string ForegroundColor { get; set; }
        public string LongName { get; set; }
        public string Text { get; set; }
    }
}
