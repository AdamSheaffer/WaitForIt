﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.BDDfy;

namespace TestWaitForIt
{
    [TestClass]
    public class AddingCountdownsTest : TestHelper
    {
        // As a user
        // In order to see how long until an event I'm looking forward to
        // I want to create a new countdown
        // * Click "+" to get the add new event form
        // * Event name must contain at least one character
        // * Event date can be in the past or future, but is required
        // * Event date is auto-populated to 30 days from now
        // * Click "Add" to add the event
        // * The event shows up in the main event list and the form goes away
        // * Click "x" to close the form

        [TestMethod]
        public void ScenarioHappyPathEventCreation()
        {
            GivenThereAreNoEvents();
            ThenIShouldSeeACountdownFor("New Year's", "12/31/15");
            WhenIClick("+");
            ThenIShouldSeeTheEventForm();
            AndIShouldNotSeeTheHelperText();
            AndTheEventDateShouldBe30DaysFromNow();
            AndTheButtonShouldBeDisabled("+");
            WhenIFillInEventTitleWith("Valentine's");
            AndIChooseTheEventDate("02/14/15");
            AndIClick("Add");
            ThenIShouldNotSeeTheEventForm();
            AndIShouldSeeXEvents(1);
            AndIShouldSeeACountdownFor("Valentine's", "02/14/15");
            AndIShouldNotSeeTheHelperText();
            AndTheButtonShouldBeEnabled("+");
        }

        [TestMethod]
        public void ScenarioDataValidationForEventCreation()
        {
            GivenThereAreNoEvents();
            WhenIClick("+");
            AndIClick("Add");
            ThenIShouldSeeTheEventForm();
            AndIShouldSeeAnErrorMessage("Event title must be filled in.");
            WhenIFillInEventTitleWith("Valentine's");
            AndIChooseTheEventDate("02/14/15");
            AndIClick("Add");
            ThenIShouldNotSeeTheEventForm();
            AndIShouldSeeXEvents(1);
            AndIShouldSeeACountdownFor("Valentine's", "02/14/15");
        }

        [TestMethod]
        public void ScenarioCancelingOutOfEventCreation()
        {
            GivenThereAreNoEvents();
            WhenIClick("+");
            WhenIFillInEventTitleWith("Valentine's");
            AndIChooseTheEventDate("02/14/15");
            AndIClick("Cancel");
            ThenIShouldNotSeeTheEventForm();
            AndIShouldSeeXEvents(0);
            AndIShouldSeeTheHelperText();
            AndTheButtonShouldBeEnabled("+");
            WhenIClick("+");
            ThenIShouldSeeTheEventForm();
            AndTheEventDateShouldBe30DaysFromNow();
            AndTheButtonShouldBeDisabled("+");
        }
    }
}
