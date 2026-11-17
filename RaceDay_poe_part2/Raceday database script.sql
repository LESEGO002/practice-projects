Create database racedaydb;

use racedaydb;

create table users(
UserId int primary key identity(1,2),
FirstName varchar(100)not null,
LastName varchar(100)not null,
age int,
Email varchar(100)not null Unique,
passwordHashed varchar(max) not null,
createdAt varchar(50) not null,
UserRole varchar(100)
);
 

insert into users(FirstName,LastName,age,Email,passwordHashed,createdAt,UserRole)
values
('Lesego','Stonner',18,'gou@gmail.com','11adytdw','june 21','Organizer'),
('Junior','Besturre',20,'clu@gmail.com','2ffr065','may 20','Participant'),
('Kgotatso','Flook',32,'llu@gmail.com','222tue','4 Jan','001'),
('Joe','Manamela',25,'vto@gmail.com','66fghhj','3 july','003');


Create table Catergories(
CatergoryId int primary key identity(1,4),
CatergoryName varchar(100)not null,
CategoryDescrition varchar(100)not null,
MinAge int not null,
MaxAge int not null,
organizerID int foreign key(organizerID)references Organizers(OrganizerId)
);

 
INSERT INTO Catergories (CatergoryName, CategoryDescrition, MinAge, MaxAge, organizerID)
VALUES
('Athletics', 'for runners and gymnasts', 18, 45,
 (SELECT o.OrganizerId FROM Organizers o WHERE o.User_id = 11)),
('Jogging', 'For exercisers', 20, 75,
 (SELECT o.OrganizerId FROM Organizers o WHERE o.User_id = 11));


Create Table Organizers(
OrganizerId int primary key Identity(1,1),
User_id int foreign key(User_id)references Users(UserId)
);

create table Events(
EventId int primary key identity(1,3),
EventName varchar(100),
Eventdescription varchar(100) not null,
EventDate DateTime2 not null Default GetDate(),
EventLocation varchar(100)not null,
MaxParticipants int not null,
Eventstatus char(100)not null,
createdAt varchar(50) not null,
Organiser_id int FOREIGN KEY (Organiser_id) REFERENCES Organizers(OrganizerId),
Category_id int FOREIGN KEY (Category_id) REFERENCES Catergories(CatergoryId)
);

DELETE FROM Events WHERE EventName = 'Test Run';

INSERT INTO Events
(EventName, Eventdescription, EventDate, EventLocation, MaxParticipants, Eventstatus, createdAt, Organiser_id, Category_id)
VALUES
('Test Run', 'Testing', '2026-11-01', 'Johannesburg', 100, 'Open', GETDATE(),
 (SELECT o.OrganizerId FROM Organizers o JOIN Users u ON u.UserId = o.User_id WHERE u.Email = 'mbalulafikile@gmail.com'),
 (SELECT CatergoryId FROM Catergories WHERE CatergoryName = 'Athletics'));

SELECT * FROM Events;
SELECT * FROM Events;

Create table Enrollment(
EnrollmentId int primary key identity(1,5),
BibNumber int not null,
EnrollmentDate DateTime2 not null Default GetDate(),
Enrollmentstatus varchar(50) not null,
event_id int foreign key(event_id)references Events(EventId),
Participant_id int FOREIGN KEY (Participant_Id) REFERENCES Participants(ParticipantID)
);

insert into Enrollment(BibNumber,EnrollmentDate,Enrollmentstatus)
Values
('10','22 may 2025','Pending'),
('18','03 March 2016','Approved');


Create table Participants(
ParticipantId int primary key Identity(1,7),
event_id int foreign key(event_id)references Events(EventId),
User_id int foreign key(User_id)references Users(UserId)
);



create table Results(
ResultId int primary key identity(1,6),
finishTime char(8) not null,
position int not null,
ResultStatus Varchar(50)not null,
RecordedAt char(50),
Enrollment_id int foreign key(Enrollment_id)references Enrollment(EnrollmentId)
);

insert into Results(finishTime,position,ResultStatus,RecordedAt)
Values
('10:25 am','12','Lost','22/08/2026'),
('13:00 pm','5','Won','22/08/2026');


select * from Organizers;
Select * from Participants;
select * from users;
select * from Events;
select * from Catergories;
select * from Enrollment;
select * from Results;

DELETE FROM Participants WHERE ParticipantId IN (22, 36, 50, 71);