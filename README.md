# UniversityService
# Contents

[1. Project Overview [2](#project-overview)](#project-overview)

[2. Technologies Used [2](#technologies-used)](#technologies-used)

[3. Database Schema [2](#database-schema)](#database-schema)

[4. API Endpoints [3](#api-endpoints)](#api-endpoints)

[5. Entity and Repository Design
[3](#entity-and-repository-design)](#entity-and-repository-design)

[6. JWT Token and User Authentication
[3](#jwt-token-and-user-authentication)](#jwt-token-and-user-authentication)

[7. Role-based Access Control
[3](#role-based-access-control)](#role-based-access-control)

[8. Sorting and Paging [4](#sorting-and-paging)](#sorting-and-paging)

[9. Error Handling [4](#error-handling)](#error-handling)

[10. Installation and Running the Project
[4](#installation-and-running-the-project)](#installation-and-running-the-project)

[11. Testing [5](#testing)](#testing)

[Appendix [6](#appendix)](#appendix)

[Appendix A – Database schema script
[6](#appendix-a-database-schema-script)](#appendix-a-database-schema-script)

[Appendix B– Database data script
[10](#appendix-b-database-data-script)](#appendix-b-database-data-script)

# 1. Project Overview

This project is an implementation of a RESTful API for a university
management system. The API allows users (students and lecturers) to
manage and access information related to classes, students, and
lecturers based on their roles. It implements features like role-based
access control, JWT authentication, pagination, and sorting.

# 2. Technologies Used

ASP.NET Core: The project is built using ASP.NET Core which is a free,
open-source, cross-platform framework for building modern, cloud-based,
and internet-connected applications.

Entity Framework Core: It is used as an Object-Relational Mapper (ORM)
to facilitate data operations.

LINQ: Language Integrated Query (LINQ) is used for data manipulation
operations over the data from the database.

SQL Server: Used as the database management system.

JSON Web Tokens (JWT): Used for user authentication and authorization.

# 3. Database Schema

The database schema consists of four main tables: Users, Courses,
Sections, AcademicPeriods, and SectionUsers. The diagram of database
schema is as follows:

![alt text](https://github.com/pmousarris/UniversityService/blob/main/Database%20Diagram.png)

# 4. API Endpoints

The API consists of the following endpoints:

POST /Login/GetToken - Authenticates the user and generates a JWT (JSON
Web Token) for authenticated users. This token should be included in the
Authorization header (Type: Bearer Token) for subsequent requests to
access protected resources. The body of the request should contain the
user's credentials (username and password).

POST /Student/GetStudents - Retrieves a list of students, including authenticated student and displays
user details based on user role.

POST /Class/GetClasses - Retrieves a list of classes with the option to
filter by academic period and/or taken classes.

POST /Class/GetClassesWithAssignedLecturers - Retrieves a list of all
classes. For lecturers, it also includes the number of students.

Details of request parameters, expected responses and HTTP methods can
be viewed by running the project in debug mode and browsing the swagger
web interface.

# 5. Entity and Repository Design

The project implements Entity classes representing the database tables:
User, Class, AcademicPeriod, and SectionUser. It includes a repository
interface, IUnicBaseRepository, with the database operations, and
concrete implementation repository class.

# 6. JWT Token and User Authentication

The project uses JSON Web Tokens (JWT) for user authentication and
authorization. User passwords are hashed using the BCrypt.Net-Next
library before being stored in the database. JWTs are generated upon
successful login and must be included in the Authorization header for
accessing protected endpoints. The token is set to expire after a
certain interval which is configurable in the *appsettings.json* file of
the project solution.

# 7. Role-based Access Control

Access to the controller actions is controlled based on the user's role,
using an AccessActionFilter. The filter checks the role of the
authenticated user against a dictionary of roles and their accessible
controllers/actions.

# 8. Sorting and Paging

The /Student/GetStudents endpoint returns list of resources with sorting
and pagination. The client can specify the sort column, sort order, page
number and page size in the request parameters.

# 9. Error Handling

For the purpose of error handling and logging a custom *IUniLogger*
interface has been implemented that provides a standard contract for all
logger operations within the system.

An IUniLoggerBase base class has been set up to provide the general
logging functionality, and an IUniLoggerConsole implementation class is
provided that logs data to the console for the development environment.

In a production environment, it's common to direct logging output to
more robust and persistent storage systems or logging platforms. For
example, logs might be written to a database or sent via email to system
administrators. This is an area that can be further developed in future
iterations of this system based on specific production requirements.

# 10. Installation and Running the Project

Clone the project repository.

Install the necessary packages using dotnet restore.

Set up the database: You will find a UnicBase.mdf file in the root
directory of the project. You need to attach this database file to your
SQL Server 2019 instance. Follow the steps below to attach the
UnicBase.mdf file:

-   Open SQL Server Management Studio and connect to your server.

-   Right-click on "Databases" in the Object Explorer and select
    "Attach...".

-   Click on the "Add" button in the "Attach Databases" dialog box.

-   In the "Locate Database Files" dialog box, navigate to the directory
    where the *UnicBase.mdf* file is located, select it, and click "OK".

-   Click "OK" in the "Attach Databases" dialog box to finish attaching
    the database.

Update the connection string: In the Program.cs file, update the sql
server connection string to point to your SQL Server instance.

Build and run the project in debugging mode.

Testing the endpoints with Postman: In the project, you'll find a
Postman requests file named *UnicBase.postman_collection*. To load this
into Postman:

-   Open Postman.

-   Click on the "Import" button at the top left.

-   Select "Upload Files", then locate and select the
    *UnicBase.postman_collection* file.

-   Click "Import" to load the requests into Postman.

-   You can now use these pre-configured requests to test the
    application's endpoints.

Login Credentials:

Here are some test credentials for different users and roles that you
can use for testing:

| **User Role** | **Email**                            | **Password** |
|---------------|--------------------------------------|--------------|
| Lecturer      | charalambos.christodoulou@unic.ac.cy | password1    |
| Student       | giannos.koutsides@gmail.com          | password2    |
| Lecturer      | artemis.giannakou@unic.ac.cy         | password3    |
| Student       | alice.georgiou@outlook.com           | password4    |

Use these credentials to log in and test the application functionality
according to the user role.

# 11. Testing

Future versions may implement unit and integration tests to ensure
functionality of individual components and the system as a whole for
improving reliability and maintainability of the application.

# Appendix

# Appendix A – Database schema script

USE \[UnicBase\]

GO

/\*\*\*\*\*\* Object: Table \[dbo\].\[AcademicPeriods\] \*\*\*\*\*\*/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

CREATE TABLE \[dbo\].\[AcademicPeriods\](

\[Id\] \[int\] IDENTITY(1,1) NOT NULL,

\[Name\] \[nvarchar\](50) NOT NULL,

\[StartDate\] \[date\] NOT NULL,

\[EndDate\] \[date\] NOT NULL,

PRIMARY KEY CLUSTERED

(

\[Id\] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON,
OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON \[PRIMARY\]

) ON \[PRIMARY\]

GO

/\*\*\*\*\*\* Object: Table \[dbo\].\[Courses\] \*\*\*\*\*\*/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

CREATE TABLE \[dbo\].\[Courses\](

\[Id\] \[int\] IDENTITY(1,1) NOT NULL,

\[Code\] \[nvarchar\](50) NOT NULL,

\[Title\] \[nvarchar\](100) NOT NULL,

PRIMARY KEY CLUSTERED

(

\[Id\] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON,
OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON \[PRIMARY\]

) ON \[PRIMARY\]

GO

/\*\*\*\*\*\* Object: Table \[dbo\].\[Sections\] \*\*\*\*\*\*/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

CREATE TABLE \[dbo\].\[Sections\](

\[Id\] \[int\] IDENTITY(1,1) NOT NULL,

\[CourseId\] \[int\] NULL,

\[AcademicPeriodId\] \[int\] NULL,

\[SectionNumber\] \[nvarchar\](50) NOT NULL,

PRIMARY KEY CLUSTERED

(

\[Id\] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON,
OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON \[PRIMARY\]

) ON \[PRIMARY\]

GO

/\*\*\*\*\*\* Object: Table \[dbo\].\[SectionUsers\] \*\*\*\*\*\*/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

CREATE TABLE \[dbo\].\[SectionUsers\](

\[SectionId\] \[int\] NOT NULL,

\[UserId\] \[uniqueidentifier\] NOT NULL,

CONSTRAINT \[PK_SectionUsers\] PRIMARY KEY CLUSTERED

(

\[SectionId\] ASC,

\[UserId\] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON,
OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON \[PRIMARY\]

) ON \[PRIMARY\]

GO

/\*\*\*\*\*\* Object: Table \[dbo\].\[Users\] \*\*\*\*\*\*/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

CREATE TABLE \[dbo\].\[Users\](

\[Id\] \[uniqueidentifier\] NOT NULL,

\[FirstName\] \[nvarchar\](50) NOT NULL,

\[LastName\] \[nvarchar\](50) NOT NULL,

\[Role\] \[int\] NOT NULL,

\[Phone1\] \[nvarchar\](20) NOT NULL,

\[Email\] \[nvarchar\](50) NOT NULL,

\[SocialInsuranceNumber\] \[nvarchar\](20) NULL,

\[PasswordHash\] \[nvarchar\](128) NOT NULL,

CONSTRAINT \[PK\_\_Users\] PRIMARY KEY CLUSTERED

(

\[Id\] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON,
OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON \[PRIMARY\],

CONSTRAINT \[UQ\_\_Users_FirstName_LastName\] UNIQUE NONCLUSTERED

(

\[FirstName\] ASC,

\[LastName\] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON,
OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON \[PRIMARY\],

CONSTRAINT \[UQ_Users_Email\] UNIQUE NONCLUSTERED

(

\[Email\] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY =
OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON,
OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON \[PRIMARY\]

) ON \[PRIMARY\]

GO

ALTER TABLE \[dbo\].\[Users\] ADD DEFAULT (newid()) FOR \[Id\]

GO

ALTER TABLE \[dbo\].\[Sections\] WITH CHECK ADD FOREIGN
KEY(\[AcademicPeriodId\])

REFERENCES \[dbo\].\[AcademicPeriods\] (\[Id\])

GO

ALTER TABLE \[dbo\].\[Sections\] WITH CHECK ADD FOREIGN
KEY(\[CourseId\])

REFERENCES \[dbo\].\[Courses\] (\[Id\])

GO

ALTER TABLE \[dbo\].\[SectionUsers\] WITH CHECK ADD CONSTRAINT
\[FK_SectionUser_Section\] FOREIGN KEY(\[SectionId\])

REFERENCES \[dbo\].\[Sections\] (\[Id\])

GO

ALTER TABLE \[dbo\].\[SectionUsers\] CHECK CONSTRAINT
\[FK_SectionUser_Section\]

GO

ALTER TABLE \[dbo\].\[SectionUsers\] WITH CHECK ADD CONSTRAINT
\[FK_SectionUser_User\] FOREIGN KEY(\[UserId\])

REFERENCES \[dbo\].\[Users\] (\[Id\])

GO

ALTER TABLE \[dbo\].\[SectionUsers\] CHECK CONSTRAINT
\[FK_SectionUser_User\]

GO

# Appendix B– Database data script

USE \[UnicBase\]

GO

SET IDENTITY_INSERT \[dbo\].\[AcademicPeriods\] ON

GO

INSERT \[dbo\].\[AcademicPeriods\] (\[Id\], \[Name\], \[StartDate\],
\[EndDate\]) VALUES (1, N'Spring 2023', CAST(N'2023-01-01' AS Date),
CAST(N'2023-04-30' AS Date))

GO

INSERT \[dbo\].\[AcademicPeriods\] (\[Id\], \[Name\], \[StartDate\],
\[EndDate\]) VALUES (2, N'Summer 2023', CAST(N'2023-05-01' AS Date),
CAST(N'2023-08-31' AS Date))

GO

INSERT \[dbo\].\[AcademicPeriods\] (\[Id\], \[Name\], \[StartDate\],
\[EndDate\]) VALUES (3, N'Fall 2023', CAST(N'2023-09-01' AS Date),
CAST(N'2023-12-31' AS Date))

GO

SET IDENTITY_INSERT \[dbo\].\[AcademicPeriods\] OFF

GO

SET IDENTITY_INSERT \[dbo\].\[Courses\] ON

GO

INSERT \[dbo\].\[Courses\] (\[Id\], \[Code\], \[Title\]) VALUES (1,
N'COMP-123', N'Introduction to Programming')

GO

INSERT \[dbo\].\[Courses\] (\[Id\], \[Code\], \[Title\]) VALUES (2,
N'ENGL-324', N'English Level 2')

GO

INSERT \[dbo\].\[Courses\] (\[Id\], \[Code\], \[Title\]) VALUES (3,
N'COMP-454', N'Data Structures')

GO

SET IDENTITY_INSERT \[dbo\].\[Courses\] OFF

GO

SET IDENTITY_INSERT \[dbo\].\[Sections\] ON

GO

INSERT \[dbo\].\[Sections\] (\[Id\], \[CourseId\], \[AcademicPeriodId\],
\[SectionNumber\]) VALUES (1, 1, 1, N'Section 1')

GO

INSERT \[dbo\].\[Sections\] (\[Id\], \[CourseId\], \[AcademicPeriodId\],
\[SectionNumber\]) VALUES (2, 1, 1, N'Section 2')

GO

INSERT \[dbo\].\[Sections\] (\[Id\], \[CourseId\], \[AcademicPeriodId\],
\[SectionNumber\]) VALUES (3, 1, 1, N'Section 3')

GO

INSERT \[dbo\].\[Sections\] (\[Id\], \[CourseId\], \[AcademicPeriodId\],
\[SectionNumber\]) VALUES (4, 1, 2, N'Section 1')

GO

INSERT \[dbo\].\[Sections\] (\[Id\], \[CourseId\], \[AcademicPeriodId\],
\[SectionNumber\]) VALUES (5, 1, 2, N'Section 2')

GO

INSERT \[dbo\].\[Sections\] (\[Id\], \[CourseId\], \[AcademicPeriodId\],
\[SectionNumber\]) VALUES (6, 2, 1, N'Section 1')

GO

INSERT \[dbo\].\[Sections\] (\[Id\], \[CourseId\], \[AcademicPeriodId\],
\[SectionNumber\]) VALUES (7, 2, 2, N'Section 1')

GO

INSERT \[dbo\].\[Sections\] (\[Id\], \[CourseId\], \[AcademicPeriodId\],
\[SectionNumber\]) VALUES (8, 3, 3, N'Section 1')

GO

SET IDENTITY_INSERT \[dbo\].\[Sections\] OFF

GO

INSERT \[dbo\].\[Users\] (\[Id\], \[FirstName\], \[LastName\], \[Role\],
\[Phone1\], \[Email\], \[SocialInsuranceNumber\], \[PasswordHash\])
VALUES (N'07d7057a-3283-4838-86e3-027715aa35a4', N'Charalambos',
N'Christodoulou', 2, N'99345445',
N'charalambos.christodoulou@unic.ac.cy', N'3245567',
N'$2a$11$ydQDgHMIaRv.TPBGEgUvZeam3YCiHS3l4vDcxmE5S4Jm2h4emV6Me')

GO

INSERT \[dbo\].\[Users\] (\[Id\], \[FirstName\], \[LastName\], \[Role\],
\[Phone1\], \[Email\], \[SocialInsuranceNumber\], \[PasswordHash\])
VALUES (N'cf562c24-8249-4930-81f2-6db5a20ee892', N'Giannos',
N'Koutsides', 1, N'99332567', N'giannos.koutsides@gmail.com', NULL,
N'$2a$11$UQHSqGwVqVJLU90G0/oFIu/k7Qwwjqr/K/DTnBgXlOOD60iciBjl2')

GO

INSERT \[dbo\].\[Users\] (\[Id\], \[FirstName\], \[LastName\], \[Role\],
\[Phone1\], \[Email\], \[SocialInsuranceNumber\], \[PasswordHash\])
VALUES (N'8cedd47e-f8b6-4352-8eba-7957ad4d9a9a', N'Artemis',
N'Giannakou', 2, N'99434211', N'artemis.giannakou@unic.ac.cy',
N'1123546',
N'$2a$11$UJccgmYG6j/FrJvdtbj2/eZ0.bzQrxObtpG/5CCjq37.DfXL7xHHW')

GO

INSERT \[dbo\].\[Users\] (\[Id\], \[FirstName\], \[LastName\], \[Role\],
\[Phone1\], \[Email\], \[SocialInsuranceNumber\], \[PasswordHash\])
VALUES (N'9e762518-d2f3-4842-9058-d91d62e12187', N'Alice', N'Georgiou',
1, N'99443489', N'alice.georgiou@outlook.com', NULL,
N'$2a$11$rGr1u3Wmfg4cPNu8c/ywT.3fodtKR.TUylhSdp5uoLX1ZOfwtdkje')

GO

INSERT \[dbo\].\[SectionUsers\] (\[SectionId\], \[UserId\]) VALUES (1,
N'07d7057a-3283-4838-86e3-027715aa35a4')

GO

INSERT \[dbo\].\[SectionUsers\] (\[SectionId\], \[UserId\]) VALUES (1,
N'cf562c24-8249-4930-81f2-6db5a20ee892')

GO

INSERT \[dbo\].\[SectionUsers\] (\[SectionId\], \[UserId\]) VALUES (1,
N'9e762518-d2f3-4842-9058-d91d62e12187')

GO

INSERT \[dbo\].\[SectionUsers\] (\[SectionId\], \[UserId\]) VALUES (2,
N'07d7057a-3283-4838-86e3-027715aa35a4')

GO

INSERT \[dbo\].\[SectionUsers\] (\[SectionId\], \[UserId\]) VALUES (2,
N'cf562c24-8249-4930-81f2-6db5a20ee892')

GO

INSERT \[dbo\].\[SectionUsers\] (\[SectionId\], \[UserId\]) VALUES (3,
N'07d7057a-3283-4838-86e3-027715aa35a4')

GO

INSERT \[dbo\].\[SectionUsers\] (\[SectionId\], \[UserId\]) VALUES (3,
N'cf562c24-8249-4930-81f2-6db5a20ee892')

GO

INSERT \[dbo\].\[SectionUsers\] (\[SectionId\], \[UserId\]) VALUES (4,
N'07d7057a-3283-4838-86e3-027715aa35a4')

GO

INSERT \[dbo\].\[SectionUsers\] (\[SectionId\], \[UserId\]) VALUES (4,
N'cf562c24-8249-4930-81f2-6db5a20ee892')

GO

INSERT \[dbo\].\[SectionUsers\] (\[SectionId\], \[UserId\]) VALUES (5,
N'07d7057a-3283-4838-86e3-027715aa35a4')

GO

INSERT \[dbo\].\[SectionUsers\] (\[SectionId\], \[UserId\]) VALUES (5,
N'cf562c24-8249-4930-81f2-6db5a20ee892')

GO

INSERT \[dbo\].\[SectionUsers\] (\[SectionId\], \[UserId\]) VALUES (6,
N'07d7057a-3283-4838-86e3-027715aa35a4')

GO

INSERT \[dbo\].\[SectionUsers\] (\[SectionId\], \[UserId\]) VALUES (6,
N'cf562c24-8249-4930-81f2-6db5a20ee892')

GO

INSERT \[dbo\].\[SectionUsers\] (\[SectionId\], \[UserId\]) VALUES (6,
N'8cedd47e-f8b6-4352-8eba-7957ad4d9a9a')

GO

INSERT \[dbo\].\[SectionUsers\] (\[SectionId\], \[UserId\]) VALUES (7,
N'8cedd47e-f8b6-4352-8eba-7957ad4d9a9a')

GO

INSERT \[dbo\].\[SectionUsers\] (\[SectionId\], \[UserId\]) VALUES (7,
N'9e762518-d2f3-4842-9058-d91d62e12187')

GO

INSERT \[dbo\].\[SectionUsers\] (\[SectionId\], \[UserId\]) VALUES (8,
N'8cedd47e-f8b6-4352-8eba-7957ad4d9a9a')

GO

INSERT \[dbo\].\[SectionUsers\] (\[SectionId\], \[UserId\]) VALUES (8,
N'9e762518-d2f3-4842-9058-d91d62e12187')

GO
