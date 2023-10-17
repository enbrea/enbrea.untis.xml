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
                ValidFrom = new DateOnly(2009, 8, 24),
                ValidTo = new DateOnly(2010, 7, 16)
            };

            IEnumerator<DateOnly> enumerator = lesson.GetDateInstances(new DateOnly(2009, 8, 24), DayOfWeek.Tuesday).GetEnumerator();

            Assert.True(enumerator.MoveNext());
            Assert.Equal(new DateOnly(2009, 8, 25), enumerator.Current);
            Assert.True(enumerator.MoveNext());
            Assert.Equal(new DateOnly(2009, 8, 25).AddDays(14), enumerator.Current);
            Assert.True(enumerator.MoveNext());
            Assert.Equal(new DateOnly(2009, 8, 25).AddDays(28), enumerator.Current);
            Assert.True(enumerator.MoveNext());
            Assert.Equal(new DateOnly(2009, 8, 25).AddDays(42), enumerator.Current);
            Assert.True(enumerator.MoveNext());
            Assert.Equal(new DateOnly(2009, 8, 25).AddDays(70), enumerator.Current);
        }

        [Fact]
        public void Test_GetFirstDateInstance()
        {
            var lesson = new UntisLesson()
            {
                ValidFrom = new DateOnly(2021, 2, 2),
                ValidTo = new DateOnly(2021, 4, 26)
            };

            var date = lesson.GetFirstDateInstance(DayOfWeek.Thursday);

            Assert.Equal(new DateOnly(2021, 2, 4), date);
        }
    }
}
