#region ENBREA UNTIS.XML - Copyright (C) 2020 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA UNTIS.XML
 *    
 *    Copyright (C) 2020 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

namespace Enbrea.Untis.Xml
{
    /// <summary>
    /// Represents the Untis XML type "XmlRoom" 
    /// </summary>
    public class UntisRoom : UntisEntityWithShortName
    {
        public string BackgroundColor { get; set; }
        public uint? Capacity { get; set; }
        public string DepartmentId { get; set; }
        public string DescriptionId { get; set; }
        public string ForegroundColor { get; set; }
        public string ForeignKey { get; set; }
        public string LongName { get; set; }
        public string Text { get; set; }
    }
}
