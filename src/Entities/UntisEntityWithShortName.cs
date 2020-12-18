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
    /// An abstract base class with additional property
    /// </summary>
    public abstract class UntisEntityWithShortName : UntisEntity
    {
        public string ShortName 
        { 
            get { return Id.Remove(0, 3);  } 
        }
    }
}
