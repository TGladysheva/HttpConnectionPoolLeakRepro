using System;
using System.Diagnostics.Tracing;
using System.Text;
using Vostok.Logging.Abstractions;

namespace Client
{
	internal sealed class HttpEventListener : EventListener
	{
		private readonly ILog log;

		public HttpEventListener(ILog log)
		{
			this.log = log;
		}
		protected override void OnEventSourceCreated(EventSource eventSource)
		{
			if(eventSource.Name.StartsWith("Private.InternalDiagnostics.System.Net.Http", StringComparison.OrdinalIgnoreCase) || eventSource.Name.StartsWith("System.Net", StringComparison.OrdinalIgnoreCase)) EnableEvents(eventSource, EventLevel.LogAlways);
		}

		protected override void OnEventWritten(EventWrittenEventArgs eventData)
		{
			var sb = new StringBuilder().Append($"{eventData.TimeStamp:HH:mm:ss.fffffff}  {eventData.ActivityId}.{eventData.RelatedActivityId}  {eventData.EventSource.Name}.{eventData.EventName}[{eventData.Message}](");
			for(int i = 0; i < eventData.Payload?.Count; i++)
			{
				sb.Append(eventData.PayloadNames?[i]).Append(": ").Append(eventData.Payload[i]);
				if(i < eventData.Payload?.Count - 1)
				{
					sb.Append(", ");
				}
			}

			sb.Append(")");
			log.Info(sb.ToString());
		}
	}
}