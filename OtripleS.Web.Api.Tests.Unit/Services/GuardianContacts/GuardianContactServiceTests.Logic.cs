﻿//---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
//----------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using OtripleS.Web.Api.Models.GuardianContacts;
using Xunit;

namespace OtripleS.Web.Api.Tests.Unit.Services.GuardianContacts
{
    public partial class GuardianContactServiceTests
    {
        [Fact]
        public async Task ShouldAddGuardianContactAsync()
        {
            // given
            GuardianContact randomGuardianContact = CreateRandomGuardianContact();
            GuardianContact inputGuardianContact = randomGuardianContact;
            GuardianContact storageGuardianContact = randomGuardianContact;
            GuardianContact expectedGuardianContact = storageGuardianContact;

            this.storageBrokerMock.Setup(broker =>
                broker.InsertGuardianContactAsync(inputGuardianContact))
                    .ReturnsAsync(storageGuardianContact);

            // when
            GuardianContact actualGuardianContact =
                await this.guardianContactService.AddGuardianContactAsync(inputGuardianContact);

            // then
            actualGuardianContact.Should().BeEquivalentTo(expectedGuardianContact);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGuardianContactAsync(inputGuardianContact),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveGuardianContactById()
        {
            // given
            GuardianContact randomGuardianContact = CreateRandomGuardianContact();
            GuardianContact storageGuardianContact = randomGuardianContact;
            GuardianContact expectedGuardianContact = storageGuardianContact;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectGuardianContactByIdAsync(randomGuardianContact.GuardianId, randomGuardianContact.ContactId))
                    .Returns(new ValueTask<GuardianContact>(randomGuardianContact));

            // when
            GuardianContact actualGuardianContact = await
                this.guardianContactService.RetrieveGuardianContactByIdAsync(
                    randomGuardianContact.GuardianId, randomGuardianContact.ContactId);

            // then
            actualGuardianContact.Should().BeEquivalentTo(expectedGuardianContact);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectGuardianContactByIdAsync(randomGuardianContact.GuardianId, randomGuardianContact.ContactId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}