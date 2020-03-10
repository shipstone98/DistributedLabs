using System;

namespace Cryptography.Web.Models
{
	public class ErrorViewModel
	{
		public string RequestId { get; set; }

		public bool ShowRequestId => !String.IsNullOrEmpty(this.RequestId);
	}
}
