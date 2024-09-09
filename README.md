# cf-event-handler

Handles events based on event handling rules. An event client logs an event via the REST API and the event may
be passed to zero or more event handlers depending on rules defined. The rule typically examine event properties
to determine how to handle the event.

E.g. Event with event type 1234 (Error) and event parameter CompanyId=1 sends an email to X and posts to a MS Teams channel.
E.g. Event with event type 1234 (Error) and event parameter CompanyId=2 sends an email to Y and posts to a MS Teams channel.
E.g. Event with event type 4567 (Warning) logs to console.

Event
-----
The event contains the following information:
- Unique Id.
- Event Client Id. Client that logged the event.
- Event Type Id.
- Parameters (List of name & value). E.g. CompanyId=1, ErrorMessage="Unexpected error XYZ"

Event Type
----------
An event type is a single type of event.

Event Client
------------
An event client is a unique client that logs events. The event records which event client logged the event.

Event Handler
-------------
An event handler handles an event in one of the following ways:
- Log to console.
- Log to CSV file.
- Datadog event.
- Datadog metric.
- Send email.
- Send to HTTP endpoint. E.g. Pass event to REST API.
- Create Jira issue. E.g. An event that requires user action.
- SignalR client. E.g. CFEventHandler.Notifications console app.
- SMS message.
- Pass to SQL. E.g. Call stored procedure.
- Post to MS Teams channel.

Event Settings
--------------
Event settings contain settings for handling the event. The settings depend on the particular event handler
that is handling the event. E.g. For an email then the settings contain email server credentials, email recipient(s) 
and instructions for formatting the email body.

Event Handler Rule
------------------
An event handler rule defines a rule for passing the event to a specific event handler. For a single event then
there may be zero or more rules that are valid.

The rule defines the following:
- Event handler to handle the event. E.g. Send email.
- Event settings, dependent on particular event handler. E.g. Email server credentials.
- Set of conditions for using the rule based on any of the following event properties:
	- Event type. E.g. Event Type Id = 3456
	- Event client. E.g. Event Client Id = 1234
	- Event parameters. E.g. CompanyId in (1,2,3) AND RegionId = 15
	
System Configuration
--------------------
Instructions for configuring the system:
- Configure list of event clients via REST API.
- Configure list of event types via REST API.
- Configure list of event handler rules via REST API.
- Configure list of event settings to use when handling events via REST API.

CFEventHandler.Notifications (Console App)
------------------------------------------
This console application is an example for receiving SignalR notifications from the system.

The following notifications can be received:
- Diagnostics from event processing.
- Events where there is an event rule for the SignalR client.

Log Event (Script)
------------------
The script (Scripts\Log Event\*.cmd) will log an event stored in a .json file.

Health Checks
-------------
The API health check is available from the /health endpoint

