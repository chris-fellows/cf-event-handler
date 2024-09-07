# cf-event-handler

Handles events based on event handling rules. Event is logged via REST API and then passed to the
relevant event handler(s).

Event Handlers
--------------
Events can be handled by any of the following:
- Log to console.
- Log to CSV file.
- Log as Datadog event.
- Log as Datadog metric.
- Send email.
- Send to HTTP endpoint. E.g. Pass event to REST API.
- Create Jira issue.
- SignalR client. E.g. CFEventHandler.Notifications.
- Pass to SQL. E.g. Call stored procedure.
- Post to MS Teams channel.

Health Checks
-------------
The API health check is available from the /health endpoint
