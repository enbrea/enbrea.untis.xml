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

using System.Collections.Generic;

namespace Enbrea.Untis.Xml
{
    /// <summary>
    /// Represents together woith <see cref="UntisTimeGridSlot"/> the Untis XML type "XmlTimegridSlot" 
    /// </summary>
    public class UntisTimeGrid
    {
        public string Name { get; set; }
        public List<UntisTimeGridSlot> Slots { get; set; } = new List<UntisTimeGridSlot>();
    }

}
