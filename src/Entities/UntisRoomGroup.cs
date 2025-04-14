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

using System.Collections.Generic;

namespace Enbrea.Untis.Xml
{
    /// <summary>
    /// Represents the Untis XML type "XmlRoomGroup" 
    /// </summary>
    public class UntisRoomGroup : UntisEntityWithShortName
    {
        public string BackgroundColor { get; set; }
        public string ForegroundColor { get; set; }
        public string ForeignData { get; set; }
        public string ForeignKey { get; set; }
        public string LongName { get; set; }
        public List<string> RoomIds { get; set; }
    }
}
