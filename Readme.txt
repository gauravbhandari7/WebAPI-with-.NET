Brief intro to the project :  The project solution is divided into four sub-division namely 
•	Domain (Class Library) : 
	Consisting of the entities

•	Infrastructure (Class Library) :  
	Consisting of the entity configuration through IEntityTypeConfiguration allowing configuration to be separated in a separate class. 

	It also consists Migrations, IRepositories( data access layer abstraction) and  ApplicationDbContext inherting from DbContext to save and retrieve the instances of the entities

	SaveChanges method of DbContext is overridden to apply changes to the entity before saving to the database. In case of add, createdby and date are assigned while in case of modification, modified by and date are assigned.

•	Application (Class Library):
	It consists of the implementation of IRepository interface(includes the CRUD method) from infrastructure project to achieve repository pattern.

	For Student command and queries, I have used CQRS pattern with MediatR but for User and Teacher entity, implementation is done on controller itself to avoid lengthy codes.

•	TestApi (Asp.Net Core Web API)

	As mentioned in the task details, ADO.NET with stored procedures is implemented for every HttpGet request for teacher and student APIs(Services for TeacherCrudService and StudentCrudService is injected to the controllers to achieve loose coupling and data access abstraction).

	For POST and other request, entity framework core is used( this too with the injection of Crud Services).

	JWT authentication is used to authenticate the APIs. 

	Services registered for ApplicationDbContext, ICrudRepositories to the respective student and teacher CrudService implementation in Startup. Similarly, Swagger configuration is also done to check the developed Web APIs efficiently.
