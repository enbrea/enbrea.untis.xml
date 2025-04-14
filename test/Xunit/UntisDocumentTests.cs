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
using Xunit;

namespace Enbrea.Untis.Xml.Tests
{
    /// <summary>
    /// Unit tests for <see cref="UntisDocument"/>.
    /// </summary>
    public class UntisDocumentTests : IClassFixture<UntisFixture>
    {
        private readonly UntisDocument _untisDocument;

        public UntisDocumentTests(UntisFixture untisFixture)
        {
            _untisDocument = untisFixture.UntisDocument;
        }

        [Fact]
        public void Test_GeneralSettings()
        {
            Assert.Equal("12345", _untisDocument.GeneralSettings.SchoolNo);
            Assert.Equal("Test Schule", _untisDocument.GeneralSettings.SchoolName);
            Assert.Equal(new DateOnly(2009, 8, 24), _untisDocument.GeneralSettings.TermBeginDate);
            Assert.Equal(new DateOnly(2010, 7, 16), _untisDocument.GeneralSettings.TermEndDate);
        }

        [Fact]
        public void Test_Lessons()
        {
            Assert.Equal("LS_141800", _untisDocument.Lessons[0].Id);
            Assert.Equal("141800", _untisDocument.Lessons[0].ShortName);
            Assert.Equal("SU_D  G4", _untisDocument.Lessons[0].SubjectId);
            Assert.Equal("TR_CoeJM", _untisDocument.Lessons[0].TeacherId);
            Assert.Equal("CL_FOT 11B", _untisDocument.Lessons[0].ClassIds[0]);
            Assert.Equal("CL_11", _untisDocument.Lessons[0].ClassIds[1]);
            Assert.Equal("CL_12 1", _untisDocument.Lessons[0].ClassIds[2]);
            Assert.Equal("CL_13", _untisDocument.Lessons[0].ClassIds[3]);
            Assert.Equal("SG_DG4_12", _untisDocument.Lessons[0].StudentGroupIds[0]);
            Assert.Equal("ST_FicT", _untisDocument.Lessons[0].StudentIds[0]);
            Assert.Equal("ST_BraM", _untisDocument.Lessons[0].StudentIds[17]);
            Assert.Equal((uint)7, _untisDocument.Lessons[0].Times[0].Slot);
            Assert.Equal(new TimeSpan(14, 15, 0), _untisDocument.Lessons[0].Times[0].StartTime);
            Assert.Equal(new TimeSpan(15, 00, 0), _untisDocument.Lessons[0].Times[0].EndTime);
            Assert.Null(_untisDocument.Lessons[0].Times[0].SlotGroupFirstSlot);
            Assert.Null(_untisDocument.Lessons[0].Times[0].SlotGroupStartTime);
            Assert.Equal((uint)8, _untisDocument.Lessons[0].Times[0].SlotGroupLastSlot);
            Assert.Equal(new TimeSpan(15, 40, 0), _untisDocument.Lessons[0].Times[0].SlotGroupEndTime);
            Assert.Equal((uint)8, _untisDocument.Lessons[0].Times[1].Slot);
            Assert.Equal(new TimeSpan(15, 00, 0), _untisDocument.Lessons[0].Times[1].StartTime);
            Assert.Equal(new TimeSpan(15, 40, 0), _untisDocument.Lessons[0].Times[1].EndTime);
            Assert.Equal((uint)7, _untisDocument.Lessons[0].Times[1].SlotGroupFirstSlot);
            Assert.Equal(new TimeSpan(14, 15, 0), _untisDocument.Lessons[0].Times[1].SlotGroupStartTime);
            Assert.Null(_untisDocument.Lessons[0].Times[1].SlotGroupLastSlot);
            Assert.Null(_untisDocument.Lessons[0].Times[1].SlotGroupEndTime);
        }

        [Fact]
        public void Test_ReductionReasons()
        {
            Assert.Single(_untisDocument.ReductionReasons);
            Assert.Equal("RR_Brand", _untisDocument.ReductionReasons[0].Id);
            Assert.Equal("Brandschutzbeauftragter", _untisDocument.ReductionReasons[0].LongName);
        }

        [Fact]
        public void Test_Reductions()
        {
            Assert.Equal(2, _untisDocument.Reductions.Count);
            Assert.Equal("RD_1", _untisDocument.Reductions[0].Id);
            Assert.Equal("TR_AnnKo", _untisDocument.Reductions[0].TeacherId);
            Assert.Equal("RR_Brand", _untisDocument.Reductions[0].ReasonId);
            Assert.Equal(6.000, _untisDocument.Reductions[0].Value);
        }

        [Fact]
        public void Test_Teachers()
        {
            Assert.Equal("TR_AnnKo", _untisDocument.Teachers[0].Id);
            Assert.Equal("Kofi", _untisDocument.Teachers[0].FirstName);
            Assert.Equal("Annan", _untisDocument.Teachers[0].LastName);
            Assert.Equal(UntisGender.Male, _untisDocument.Teachers[0].Gender);
            Assert.Equal("18.000", _untisDocument.Teachers[0].WeekTarget);
            Assert.Single(_untisDocument.Teachers[0].Qualifications);
            Assert.Equal("SU_BK", _untisDocument.Teachers[0].Qualifications[0].SubjectId);
        }

        [Fact]
        public void Test_TimeGrids()
        {
            Assert.Single(_untisDocument.TimeGrids);
            Assert.Null(_untisDocument.TimeGrids[0].Name);
            Assert.Equal(DayOfWeek.Monday, _untisDocument.TimeGrids[0].Slots[0].Day);
            Assert.Equal((uint)1, _untisDocument.TimeGrids[0].Slots[0].Period);
            Assert.Equal(new TimeSpan(7, 50, 0), _untisDocument.TimeGrids[0].Slots[0].StartTime);
            Assert.Equal(new TimeSpan(8, 35, 0), _untisDocument.TimeGrids[0].Slots[0].EndTime);
        }
    }
}
