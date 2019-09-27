using System;

namespace Assets.Resources.Scripts.Web
{
	public class WebRequestException : Exception
	{
		public WebRequestException(string message)
			: base(message)
		{
			// we don't need anything else here
		}
	}
}