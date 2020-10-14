﻿//---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
//----------------------------------------------------------------

using System;
using System.Threading.Tasks;
using OtripleS.Web.Api.Brokers.Loggings;
using OtripleS.Web.Api.Brokers.Storage;
using OtripleS.Web.Api.Models.TeacherContacts;

namespace OtripleS.Web.Api.Services.TeacherContacts
{
    public partial class TeacherContactService : ITeacherContactService
	{
		private readonly IStorageBroker storageBroker;
		private readonly ILoggingBroker loggingBroker;

		public TeacherContactService(
			IStorageBroker storageBroker,
			ILoggingBroker loggingBroker)
		{
			this.storageBroker = storageBroker;
			this.loggingBroker = loggingBroker;
		}

        public async ValueTask<TeacherContact> RemoveTeacherContactByIdAsync(Guid teacherId, Guid contactId)
        {
			TeacherContact mayBeTeacherContact =
				await this.storageBroker.SelectTeacherContactByIdAsync(teacherId, contactId);

			return await this.storageBroker.DeleteTeacherContactAsync(mayBeTeacherContact);
		}
    }
}