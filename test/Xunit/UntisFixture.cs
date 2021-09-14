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

using System.IO;

namespace Enbrea.Untis.Xml.Tests
{
    /// <summary>
    /// Class Fixtures for <see cref="UntisDocumentTests"/>.
    /// </summary>
    public class UntisFixture
    {
        public UntisFixture()
        {
                var xmlFile = Path.Combine(GetOutputFolder(), "Assets", "untis.xml");
                UntisDocument = UntisDocument.Load(xmlFile);
        }

        public UntisDocument UntisDocument { get; set; }

        private string GetOutputFolder()
        {
            // Get the full location of the assembly
            string assemblyPath = System.Reflection.Assembly.GetAssembly(typeof(UntisFixture)).Location;

            // Get the folder that's in
            return Path.GetDirectoryName(assemblyPath);
        }
    }
}
