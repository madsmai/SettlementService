Settlements API

*Endpoints*

Post to "/Booking" with name and bookingTime

Swagger doc at https://localhost:7246/swagger/index.html

*Future work*

The service has a potential issue with denial of service.
A user could request 4 bookings foreach open hour and effectively shut down the service for the day.

This could be solved by adding an authentication layer unfortunately that was out of scope.
With an authentication layer the API could limit users to one booking at a time.
With that it would atleast require 4 users to preform a denial of service.

As we currently have no information on the users, repudiation is also an issue.
With an authentication layer the API could also track which users make a lot of bookings (and possibly don't show up).
