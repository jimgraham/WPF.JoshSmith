using System;
using System.ComponentModel;

namespace Test.ValueConverterGroup
{
	public enum ProcessingState
	{
		[Description("The task is being performed.")]
		Active,

		[Description( "The task is finished." )]
		Complete,

		[Description( "The task is yet to be performed." )]
		Pending,

		[Description( "" )]
		Unknown
	}
}