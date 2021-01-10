# Assignment submitted by Ranjeet Singh
This implementation is inspired from clean architecture and Using Mediator to implement CQRS pattern.
As there were no commands involved in the requirements so this solution looks like overkill for the requirements.

Real benefits of this pattern and demonstrated in the Domain driven architecture utilizing the Domain Events streaming and Notification Handlers.



### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

In this example we do not have much logic in Domain layer as it will be required when the application contains code for the behavior of the different business entities. In our example the project mostly the pass through to the woolworth APIs which actually encapsulate the business logic to calculate the Cart Total.

If I would have to write the logic to evaluate card total using quantities and Specials list that logic should ideally belong in the Domain layer.

### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

### Infrastructure

This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

### Web

This layer represent the Web API which allows you to invoke different queries and commands from the Application layer.