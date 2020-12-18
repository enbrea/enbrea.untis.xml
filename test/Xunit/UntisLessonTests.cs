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

using System;
using System.Collections.Generic;
using Xunit;

namespace Enbrea.Untis.Xml.Tests
{
    /// <summary>
    /// Unit tests for <see cref="UntisLesson"/>.
    /// </summary>
    public class UntisLessonTests
    {
        [Fact]
        public void Test_GetDateInstances()
        {
            var lesson = new UntisLesson()
            {
                Occurence = "11111FF00000FF1111FFF00000FF11111FF00000FF11111FFFFFFFFFFFFFFFF00000FF11111FF",
                ValidFrom = new DateTime(2009, 8, 24, 0, 0, 0, 0),
                ValidTo = new DateTime(2010, 7, 16, 0, 0, 0, 0)
            };

            IEnumerator<DateTime> enumerator = lesson.GetDateInstances(new DateTime(2009, 8, 24, 0, 0, 0, 0), DayOfWeek.Tuesday).GetEnumerator();

            Assert.True(enumerator.MoveNext());
            Assert.Equal(new DateTime(2009, 8, 25, 0, 0, 0, 0), enumerator.Current);
            Assert.True(enumerator.MoveNext());
            Assert.Equal(new DateTime(2009, 8, 25, 0, 0, 0, 0).AddDays(14), enumerator.Current);
            Assert.True(enumerator.MoveNext());
            Assert.Equal(new DateTime(2009, 8, 25, 0, 0, 0, 0).AddDays(28), enumerator.Current);
            Assert.True(enumerator.MoveNext());
            Assert.Equal(new DateTime(2009, 8, 25, 0, 0, 0, 0).AddDays(42), enumerator.Current);
            Assert.True(enumerator.MoveNext());
            Assert.Equal(new DateTime(2009, 8, 25, 0, 0, 0, 0).AddDays(70), enumerator.Current);
        }
    }
}
