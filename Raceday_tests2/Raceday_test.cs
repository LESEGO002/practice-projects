
using System.Data.SqlClient;
using RaceDay_poe_part2.backend;
using RaceDay_poe_part2.Controllers;


namespace Raceday_tests2
{//start of namespace
    public class RaceDayTests: IDisposable
    {//start of class

        private const string ConnectionString = @"Data source=(localdb)\raceday_api;database=racedaydb;Integrated Security=true;TrustServerCertificate=true;";
        private const string OrgEmail = "xunit_org@raceday.test";
        private const string Org2Email = "xunit_org2@raceday.test";
        private const string PartEmail = "xunit_part@raceday.test";
        private const string EventName = "XUNIT_Run";
        private const string CategoryName = "XUNIT_Cat";

        //clean the test data before every test
        public RaceDayTests()
        {//start of constructor
            Cleanup();
        }//end of constructor

        //clean the test data after every test
        public void Dispose()
        {//start of method
            Cleanup();
        }//end of method Dispose


        //---------------- setup and helpers ----------------

        //deletes only test data, children first
        private void Cleanup()
        {//start of method
            Run(@"
            DELETE FROM Results WHERE Enrollment_id IN (SELECT EnrollmentId FROM Enrollment WHERE event_id IN (SELECT EventId FROM Events WHERE EventName LIKE 'XUNIT[_]%'));
            DELETE FROM Enrollment WHERE event_id IN (SELECT EventId FROM Events WHERE EventName LIKE 'XUNIT[_]%');
            DELETE FROM Participants WHERE event_id IN (SELECT EventId FROM Events WHERE EventName LIKE 'XUNIT[_]%')
                OR User_id IN (SELECT UserId FROM users WHERE Email LIKE 'xunit[_]%@raceday.test');
            DELETE FROM Events WHERE EventName LIKE 'XUNIT[_]%';
            DELETE FROM Catergories WHERE CatergoryName LIKE 'XUNIT[_]%';
            DELETE FROM Organizers WHERE User_id IN (SELECT UserId FROM users WHERE Email LIKE 'xunit[_]%@raceday.test');
            DELETE FROM users WHERE Email LIKE 'xunit[_]%@raceday.test';");
        }//end of method Cleanup

        //runs a sql command that returns nothing
        private void Run(string sql)
        {//start of method
            using (SqlConnection connect = new SqlConnection(ConnectionString))
            {
                connect.Open();
                new SqlCommand(sql, connect).ExecuteNonQuery();
            }
        }//end of method Run

        //runs a sql command and returns the first value
        private object Scalar(string sql)
        {//start of method
            using (SqlConnection connect = new SqlConnection(ConnectionString))
            {
                connect.Open();
                return new SqlCommand(sql, connect).ExecuteScalar();
            }
        }//end of method Scalar

        //runs a count query and returns the number
        private int Count(string sql) => (int)Scalar(sql);

        //seed helpers insert straight into the tables and return the new id
        private int SeedUser(string email, string role, string hash = "hash") =>
            (int)Scalar($"INSERT INTO users (FirstName, LastName, age, Email, passwordHashed, createdAt, UserRole) OUTPUT INSERTED.UserId VALUES ('Test','User',25,'{email}','{hash}','2026-10-16','{role}')");

        private int SeedOrganizer(string email)
        {//start of method
            int userId = SeedUser(email, "Organizer");
            return (int)Scalar($"INSERT INTO Organizers (User_id) OUTPUT INSERTED.OrganizerId VALUES ({userId})");
        }//end of method SeedOrganizer

        private int SeedCategory(string name, int organizerId) =>
            (int)Scalar($"INSERT INTO Catergories (CatergoryName, CategoryDescrition, MinAge, MaxAge, organizerID) OUTPUT INSERTED.CatergoryId VALUES ('{name}','desc',18,45,{organizerId})");

        private int SeedEvent(string name, int organizerId, int categoryId) =>
            (int)Scalar($"INSERT INTO Events (EventName, Eventdescription, EventDate, EventLocation, MaxParticipants, Eventstatus, createdAt, Organiser_id, Category_id) OUTPUT INSERTED.EventId VALUES ('{name}','desc','2026-12-25','JHB',100,'Open','2026-10-16',{organizerId},{categoryId})");

        private int SeedParticipant(int eventId, int userId) =>
            (int)Scalar($"INSERT INTO Participants (event_id, User_id) OUTPUT INSERTED.ParticipantId VALUES ({eventId},{userId})");

        private int SeedEnrollment(int bib, int eventId, int participantId) =>
            (int)Scalar($"INSERT INTO Enrollment (BibNumber, Enrollmentstatus, event_id, Participant_id) OUTPUT INSERTED.EnrollmentId VALUES ({bib},'Enrolled',{eventId},{participantId})");

        private int SeedResult(int enrollmentId) =>
            (int)Scalar($"INSERT INTO Results (finishTime, position, ResultStatus, RecordedAt, Enrollment_id) OUTPUT INSERTED.ResultId VALUES ('01:45:30',1,'Won','2026-10-16 10:00:00',{enrollmentId})");

        //organizer + category + event
        private (int OrganizerId, int CategoryId, int EventId) SeedEventSetup()
        {//start of method
            int organizerId = SeedOrganizer(OrgEmail);
            int categoryId = SeedCategory(CategoryName, organizerId);
            int eventId = SeedEvent(EventName, organizerId, categoryId);
            return (organizerId, categoryId, eventId);
        }//end of method SeedEventSetup

        //everything above plus a participant who joined and enrolled
        private int SeedEnrollmentSetup(int bib = 10)
        {//start of method
            var setup = SeedEventSetup();
            int userId = SeedUser(PartEmail, "Participant");
            int participantId = SeedParticipant(setup.EventId, userId);
            return SeedEnrollment(bib, setup.EventId, participantId);
        }//end of method SeedEnrollmentSetup


        //---------------- password rules ----------------

        //test that weak passwords are rejected
        //using the PasswordCheck() method
        [Theory]
        [InlineData("Ab1@", "at least 8")]
        [InlineData(null, "at least 8")]
        [InlineData("Pass @1234", "spaces")]
        [InlineData("pass@1234", "uppercase")]
        [InlineData("PASS@1234", "lowercase")]
        [InlineData("Pass@word", "number")]
        [InlineData("Pass1234", "special character")]
        public void test_weakPassword(string password, string expected_results)
        {//start of method

            //creating an instance of the class
            //with an object check_password
            Authentication check_password = new Authentication();

            //test result from the method
            string test_results = check_password.PasswordCheck(password);

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

        }//end of method test_weakPassword


        //test that strong passwords are accepted
        //using the PasswordCheck() method
        [Theory]
        [InlineData("Pass@1234")]
        [InlineData("Str0ng#Passw0rd")]
        public void test_strongPassword(string password)
        {//start of method

            //creating an instance of the class
            //with an object check_password
            Authentication check_password = new Authentication();

            //test result and expected results
            string test_results = check_password.PasswordCheck(password);
            string expected_results = "";

            //then use assert.equal()
            Assert.Equal(expected_results, test_results);

        }//end of method test_strongPassword


        //---------------- register and login ----------------

        //test that a participant is saved as a user
        //using the RegisterUserInfo() method
        [Fact]
        public void test_registerParticipant()
        {//start of method

            //creating an instance of the class
            //with an object register_user
            Authentication register_user = new Authentication();

            //test result and expected results
            string test_results = register_user.RegisterUserInfo("Xunit", "Part", 22, PartEmail, "hash", "2026-10-16", "Participant");
            string expected_results = "registered successfully";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

            //check the user was saved
            int test_count = Count($"SELECT COUNT(*) FROM users WHERE Email='{PartEmail}'");
            int expected_count = 1;
            Assert.Equal(expected_count, test_count);

        }//end of method test_registerParticipant


        //test that no participant row is created on register
        //participant rows are created when the user joins an event
        [Fact]
        public void test_registerParticipantNoParticipantRow()
        {//start of method

            //creating an instance of the class
            //with an object register_user
            Authentication register_user = new Authentication();

            //register the participant
            register_user.RegisterUserInfo("Xunit", "Part", 22, PartEmail, "hash", "2026-10-16", "Participant");

            //test result and expected results
            int test_count = Count($"SELECT COUNT(*) FROM Participants p JOIN users u ON u.UserId = p.User_id WHERE u.Email='{PartEmail}'");
            int expected_count = 0;

            //then use assert.equal()
            Assert.Equal(expected_count, test_count);

        }//end of method test_registerParticipantNoParticipantRow


        //test that an organizer also gets an organizer row
        //using the RegisterUserInfo() method
        [Fact]
        public void test_registerOrganizer()
        {//start of method

            //creating an instance of the class
            //with an object register_user
            Authentication register_user = new Authentication();

            //test result and expected results
            string test_results = register_user.RegisterUserInfo( "Org", "30", OrgEmail, "hash", "2026-10-16", "Organizer");
            string expected_results = "registered successfully";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

            //check the organizer row was created
            int test_count = Count($"SELECT COUNT(*) FROM Organizers o JOIN users u ON u.UserId = o.User_id WHERE u.Email='{OrgEmail}'");
            int expected_count = 1;
            Assert.Equal(expected_count, test_count);

        }//end of method test_registerOrganizer


        //test that a duplicate email is rejected
        //using the RegisterUserInfo() method
        [Fact]
        public void test_registerDuplicateEmail()
        {//start of method

            //creating an instance of the class
            //with an object register_user
            Authentication register_user = new Authentication();

            //register the same email twice
            register_user.RegisterUserInfo("Xunit", "Part", 22, PartEmail, "hash", "2026-10-16", "Participant");
            string test_results = register_user.RegisterUserInfo("Xunit", "Again", 22, PartEmail, "hash", "2026-10-16", "Participant");
            string expected_results = "UNIQUE KEY";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

            //check only one user was saved
            int test_count = Count($"SELECT COUNT(*) FROM users WHERE Email='{PartEmail}'");
            int expected_count = 1;
            Assert.Equal(expected_count, test_count);

        }//end of method test_registerDuplicateEmail


        //test that the correct password logs in
        //using the LogInInfo() method
        [Fact]
        public void test_loginCorrectPassword()
        {//start of method

            //seed a user with a hashed password
            string hash = BCrypt.Net.BCrypt.HashPassword("Pass@1234");
            SeedUser(PartEmail, "Participant", hash);

            //creating an instance of the class
            //with an object login_user
            AuthController login_user = new AuthController();

            //test result and expected results
            string test_results = login_user.LoginUser(PartEmail, "Pass@1234");
            string expected_results = "Logged in successfully";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

        }//end of method test_loginCorrectPassword


        //test that a wrong password is rejected
        //using the LogInInfo() method
        [Fact]
        public void test_loginWrongPassword()
        {//start of method

            //seed a user with a hashed password
            string hash = BCrypt.Net.BCrypt.HashPassword("Pass@1234");
            SeedUser(PartEmail, "Participant", hash);

            //creating an instance of the class
            //with an object login_user
            AuthController login_user = new AuthController();

            //test result and expected results
            string test_results = login_user.LoginUser(PartEmail, "Wrong@1234");
            string expected_results = "Failed to Login";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

        }//end of method test_loginWrongPassword


        //---------------- events ----------------

        //test that an organizer can create an event
        //using the CreateEvents() method
        [Fact]
        public void test_createEventByOrganizer()
        {//start of method

            //seed the organizer and category
            int orgId = SeedOrganizer(OrgEmail);
            SeedCategory(CategoryName, orgId);

            //creating an instance of the class
            //with an object create_event
            Events create_event = new Events();

            //test result and expected results
            string test_results = create_event.CreateEvents(EventName, "desc", new DateTime(2026, 12, 25), "JHB", 100, "Open", OrgEmail, CategoryName);
            string expected_results = "created successfully";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

            //check the organizer id and category id were saved
            int test_count = Count($"SELECT COUNT(*) FROM Events WHERE EventName='{EventName}' AND Organiser_id IS NOT NULL AND Category_id IS NOT NULL");
            int expected_count = 1;
            Assert.Equal(expected_count, test_count);

        }//end of method test_createEventByOrganizer


        //test that creating an event does not create a participant row
        //using the CreateEvents() method
        [Fact]
        public void test_createEventNoParticipantRow()
        {//start of method

            //seed the organizer and category
            int orgId = SeedOrganizer(OrgEmail);
            SeedCategory(CategoryName, orgId);

            //creating an instance of the class
            //with an object create_event
            Events create_event = new Events();

            //create the event
            create_event.CreateEvents(EventName, "desc", new DateTime(2026, 12, 25), "JHB", 100, "Open", OrgEmail, CategoryName);

            //test result and expected results
            int test_count = Count($"SELECT COUNT(*) FROM Participants WHERE event_id IN (SELECT EventId FROM Events WHERE EventName='{EventName}')");
            int expected_count = 0;

            //then use assert.equal()
            Assert.Equal(expected_count, test_count);

        }//end of method test_createEventNoParticipantRow


        //test that a participant cannot create an event
        //using the CreateEvents() method
        [Fact]
        public void test_createEventByParticipant()
        {//start of method

            //seed the organizer, category and a participant
            int orgId = SeedOrganizer(OrgEmail);
            SeedCategory(CategoryName, orgId);
            SeedUser(PartEmail, "Participant");

            //creating an instance of the class
            //with an object create_event
            Events create_event = new Events();

            //test result and expected results
            string test_results = create_event.CreateEvents(EventName, "desc", new DateTime(2026, 12, 25), "JHB", 100, "Open", PartEmail, CategoryName);
            string expected_results = "Organizer not found";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

            //check no event was saved
            int test_count = Count($"SELECT COUNT(*) FROM Events WHERE EventName='{EventName}'");
            int expected_count = 0;
            Assert.Equal(expected_count, test_count);

        }//end of method test_createEventByParticipant


        //test that an unknown category is rejected
        //using the CreateEvents() method
        [Fact]
        public void test_createEventUnknownCategory()
        {//start of method

            //seed the organizer only
            SeedOrganizer(OrgEmail);

            //creating an instance of the class
            //with an object create_event
            Events create_event = new Events();

            //test result and expected results
            string test_results = create_event.CreateEvents(EventName, "desc", new DateTime(2026, 12, 25), "JHB", 100, "Open", OrgEmail, "XUNIT_Missing");
            string expected_results = "Category not found";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

            //check no event was saved
            int test_count = Count($"SELECT COUNT(*) FROM Events WHERE EventName='{EventName}'");
            int expected_count = 0;
            Assert.Equal(expected_count, test_count);

        }//end of method test_createEventUnknownCategory


        //test that an event can be found by its id
        //using the EventbyID() method
        [Fact]
        public void test_eventByIdFound()
        {//start of method

            //seed the event
            var setup = SeedEventSetup();

            //creating an instance of the class
            //with an object get_event
            Events get_event = new Events();

            //test result and expected results
            string test_results = get_event.EventbyID(setup.EventId);
            string expected_results = EventName;

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

        }//end of method test_eventByIdFound


        //test that an unknown event id returns not found
        //using the EventbyID() method
        [Fact]
        public void test_eventByIdNotFound()
        {//start of method

            //creating an instance of the class
            //with an object get_event
            Events get_event = new Events();

            //test result and expected results
            string test_results = get_event.EventbyID(-1);
            string expected_results = "Event not found";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

        }//end of method test_eventByIdNotFound


        //test that another organizer cannot update the event
        //using the UpdateEvent() method
        [Fact]
        public void test_updateEventNotOwner()
        {//start of method

            //seed the event and a second organizer
            var setup = SeedEventSetup();
            SeedOrganizer(Org2Email);

            //creating an instance of the class
            //with an object update_event
            Events update_event = new Events();

            //test result and expected results
            string test_results = update_event.UpdateEvent(setup.EventId, Org2Email, "XUNIT_Renamed", "desc", new DateTime(2026, 12, 25), "JHB", 100, "Open");
            string expected_results = "not the organizer";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

            //check the name did not change
            int test_count = Count($"SELECT COUNT(*) FROM Events WHERE EventId={setup.EventId} AND EventName='{EventName}'");
            int expected_count = 1;
            Assert.Equal(expected_count, test_count);

        }//end of method test_updateEventNotOwner


        //test that the owner can update the event
        //using the UpdateEvent() method
        [Fact]
        public void test_updateEventOwner()
        {//start of method

            //seed the event
            var setup = SeedEventSetup();

            //creating an instance of the class
            //with an object update_event
            Events update_event = new Events();

            //test result and expected results
            string test_results = update_event.UpdateEvent(setup.EventId, OrgEmail, "XUNIT_Renamed", "desc", new DateTime(2026, 12, 25), "JHB", 100, "Open");
            string expected_results = "updated successfully";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

            //check the name changed
            int test_count = Count($"SELECT COUNT(*) FROM Events WHERE EventId={setup.EventId} AND EventName='XUNIT_Renamed'");
            int expected_count = 1;
            Assert.Equal(expected_count, test_count);

        }//end of method test_updateEventOwner


        //test that another organizer cannot delete the event
        //using the DeleteEvent() method
        [Fact]
        public void test_deleteEventNotOwner()
        {//start of method

            //seed the event and a second organizer
            var setup = SeedEventSetup();
            SeedOrganizer(Org2Email);

            //creating an instance of the class
            //with an object delete_event
            Events delete_event = new Events();

            //test result and expected results
            string test_results = delete_event.DeleteEvent(setup.EventId, Org2Email);
            string expected_results = "not the organizer";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

            //check the event is still there
            int test_count = Count($"SELECT COUNT(*) FROM Events WHERE EventId={setup.EventId}");
            int expected_count = 1;
            Assert.Equal(expected_count, test_count);

        }//end of method test_deleteEventNotOwner


        //test that the owner can delete the event
        //and the participants go with it
        //using the DeleteEvent() method
        [Fact]
        public void test_deleteEventOwner()
        {//start of method

            //seed the event and a participant who joined
            var setup = SeedEventSetup();
            int userId = SeedUser(PartEmail, "Participant");
            SeedParticipant(setup.EventId, userId);

            //creating an instance of the class
            //with an object delete_event
            Events delete_event = new Events();

            //test result and expected results
            string test_results = delete_event.DeleteEvent(setup.EventId, OrgEmail);
            string expected_results = "deleted successfully";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

            //check the event and its participants are gone
            int test_eventCount = Count($"SELECT COUNT(*) FROM Events WHERE EventId={setup.EventId}");
            int test_participantCount = Count($"SELECT COUNT(*) FROM Participants WHERE event_id={setup.EventId}");
            int expected_count = 0;
            Assert.Equal(expected_count, test_eventCount);
            Assert.Equal(expected_count, test_participantCount);

        }//end of method test_deleteEventOwner


        //test that a participant can join an event
        //using the JoinEvent() method
        [Fact]
        public void test_joinEvent()
        {//start of method

            //seed the event and a participant
            var setup = SeedEventSetup();
            int userId = SeedUser(PartEmail, "Participant");

            //creating an instance of the class
            //with an object join_event
            Events join_event = new Events();

            //test result and expected results
            string test_results = join_event.CreateEvents(PartEmail, EventName);
            string expected_results = "joined";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

            //check both ids were saved
            int test_count = Count($"SELECT COUNT(*) FROM Participants WHERE event_id={setup.EventId} AND User_id={userId}");
            int expected_count = 1;
            Assert.Equal(expected_count, test_count);

        }//end of method test_joinEvent


        //test that a second join is refused
        //using the JoinEvent() method
        [Fact]
        public void test_joinEventTwice()
        {//start of method

            //seed the event and a participant
            var setup = SeedEventSetup();
            int userId = SeedUser(PartEmail, "Participant");

            //creating an instance of the class
            //with an object join_event
            Events join_event = new Events();

            //join the event twice
            join_event.CreateEvents(PartEmail, EventName);
            string test_results = join_event.CreateEvents(PartEmail, EventName);
            string expected_results = "already joined";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

            //check there is still only one participant row
            int test_count = Count($"SELECT COUNT(*) FROM Participants WHERE event_id={setup.EventId} AND User_id={userId}");
            int expected_count = 1;
            Assert.Equal(expected_count, test_count);

        }//end of method test_joinEventTwice


        //test that an organizer cannot join an event
        //using the JoinEvent() method
        [Fact]
        public void test_joinEventByOrganizer()
        {//start of method

            //seed the event
            SeedEventSetup();

            //creating an instance of the class
            //with an object join_event
            Events join_event = new Events();

            //test result and expected results
            string test_results = join_event.CreateEvents(OrgEmail, EventName);
            string expected_results = "Only registered participants";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

        }//end of method test_joinEventByOrganizer


        //test that joining an unknown event is refused
        //using the JoinEvent() method
        [Fact]
        public void test_joinEventUnknownEvent()
        {//start of method

            //seed a participant
            SeedUser(PartEmail, "Participant");

            //creating an instance of the class
            //with an object join_event
            Events join_event = new Events();

            //test result and expected results
            string test_results = join_event.CreateEvents(PartEmail, "XUNIT_Missing");
            string expected_results = "Event not found";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

        }//end of method test_joinEventUnknownEvent


        //---------------- enrollment ----------------

        //test that a participant who joined can enroll
        //using the CreateEnrollment() method
        [Fact]
        public void test_createEnrollmentAfterJoining()
        {//start of method

            //seed the event and a participant who joined
            var setup = SeedEventSetup();
            int userId = SeedUser(PartEmail, "Participant");
            SeedParticipant(setup.EventId, userId);

            //creating an instance of the class
            //with an object create_enrollment
            EnrollmentsController create_enrollment = new EnrollmentsController();

            //test result and expected results
            string test_results = create_enrollment.CreateEnrollment(10, "Enrolled", EventName, PartEmail);
            string expected_results = "created successfully";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

            //check every column was filled
            int test_count = Count($"SELECT COUNT(*) FROM Enrollment WHERE BibNumber=10 AND event_id={setup.EventId} AND Participant_id IS NOT NULL");
            int expected_count = 1;
            Assert.Equal(expected_count, test_count);

        }//end of method test_createEnrollmentAfterJoining


        //test that enrolling without joining is refused
        //using the CreateEnrollment() method
        [Fact]
        public void test_createEnrollmentWithoutJoining()
        {//start of method

            //seed the event and a participant who did not join
            var setup = SeedEventSetup();
            SeedUser(PartEmail, "Participant");

            //creating an instance of the class
            //with an object create_enrollment
            EnrollmentsController create_enrollment = new EnrollmentsController();

            //test result and expected results
            string test_results = create_enrollment.CreateEnrollment(10, "Enrolled", EventName, PartEmail);
            string expected_results = "join the event first";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

            //check no enrollment was saved
            int test_count = Count($"SELECT COUNT(*) FROM Enrollment WHERE event_id={setup.EventId}");
            int expected_count = 0;
            Assert.Equal(expected_count, test_count);

        }//end of method test_createEnrollmentWithoutJoining


        //test that enrolling in an unknown event is refused
        //using the CreateEnrollment() method
        [Fact]
        public void test_createEnrollmentUnknownEvent()
        {//start of method

            //seed a participant
            SeedUser(PartEmail, "Participant");

            //creating an instance of the class
            //with an object create_enrollment
            EnrollmentsController create_enrollment = new EnrollmentsController();

            //test result and expected results
            string test_results = create_enrollment.CreateEnrollment(10, "Enrolled", "XUNIT_Missing", PartEmail);
            string expected_results = "Event not found";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

        }//end of method test_createEnrollmentUnknownEvent


        //test that an enrollment can be found by its id
        //using the EnrollmentbyID() method
        [Fact]
        public void test_enrollmentByIdFound()
        {//start of method

            //seed the enrollment
            int id = SeedEnrollmentSetup(10);

            //creating an instance of the class
            //with an object get_enrollment
            EnrollmentsController get_enrollment = new EnrollmentsController();

            //test result and expected results
            string test_results = get_enrollment.GetEnrollmentById(id);
            string expected_results = "Bib Number: 10";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

        }//end of method test_enrollmentByIdFound


        //test that an unknown enrollment id returns not found
        //using the EnrollmentbyID() method
        [Fact]
        public void test_enrollmentByIdNotFound()
        {//start of method

            //creating an instance of the class
            //with an object get_enrollment
            EnrollmentsController get_enrollment = new EnrollmentsController();

            //test result and expected results
            string test_results = get_enrollment.GetEnrollmentById(-1);
            string expected_results = "Enrollment not found";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

        }//end of method test_enrollmentByIdNotFound


        //test that all enrollments include the event name
        //using the GetAllEnrollments() method
        [Fact]
        public void test_getAllEnrollments()
        {//start of method

            //seed the enrollment
            SeedEnrollmentSetup();

            //creating an instance of the class
            //with an object get_enrollments
            EnrollmentsController get_enrollments = new EnrollmentsController();

            //test result and expected results
            string test_results = get_enrollments.GetAllEnrollments();
            string expected_results = EventName;

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

        }//end of method test_getAllEnrollments


        //test that an enrollment bib and status can be updated
        //using the UpdateEnrollment_byId() method
        [Fact]
        public void test_updateEnrollment()
        {//start of method

            //seed the enrollment
            int id = SeedEnrollmentSetup(10);

            //creating an instance of the class
            //with an object update_enrollment
            EnrollmentsController update_enrollment = new EnrollmentsController();

            //test result and expected results
            string test_results = update_enrollment.UpdateEnrollmentById(id, 77, "Confirmed");
            string expected_results = "updated successfully";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

            //check the bib and status changed
            int test_count = Count($"SELECT COUNT(*) FROM Enrollment WHERE EnrollmentId={id} AND BibNumber=77 AND Enrollmentstatus='Confirmed'");
            int expected_count = 1;
            Assert.Equal(expected_count, test_count);

        }//end of method test_updateEnrollment


        //test that an enrollment can be deleted
        //using the DeleteEnrollment_byId() method
        [Fact]
        public void test_deleteEnrollment()
        {//start of method

            //seed the enrollment
            int id = SeedEnrollmentSetup();

            //creating an instance of the class
            //with an object delete_enrollment
            EnrollmentsController delete_enrollment = new EnrollmentsController();

            //test result and expected results
            string test_results = delete_enrollment.DeleteEnrollmentById(id);
            string expected_results = "deleted successfully";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

            //check the row is gone
            int test_count = Count($"SELECT COUNT(*) FROM Enrollment WHERE EnrollmentId={id}");
            int expected_count = 0;
            Assert.Equal(expected_count, test_count);

        }//end of method test_deleteEnrollment


        //---------------- categories ----------------

        //test that an organizer can add a category
        //using the CategoriesInfo() method
        [Fact]
        public void test_addCategoryByOrganizer()
        {//start of method

            //seed the organizer
            SeedOrganizer(OrgEmail);

            //creating an instance of the class
            //with an object add_category
            Categories add_category = new Categories();

            //test result and expected results
            string test_results = add_category.CategoriesInfo(CategoryName, "desc", 18, 45, OrgEmail);
            string expected_results = "is added";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

            //check the organizer id was saved
            int test_count = Count($"SELECT COUNT(*) FROM Catergories WHERE CatergoryName='{CategoryName}' AND organizerID IS NOT NULL");
            int expected_count = 1;
            Assert.Equal(expected_count, test_count);

        }//end of method test_addCategoryByOrganizer


        //test that a participant cannot add a category
        //using the CategoriesInfo() method
        [Fact]
        public void test_addCategoryByParticipant()
        {//start of method

            //seed the organizer and a participant
            SeedOrganizer(OrgEmail);
            SeedUser(PartEmail, "Participant");

            //creating an instance of the class
            //with an object add_category
            Categories add_category = new Categories();

            //test result and expected results
            string test_results = add_category.CategoriesInfo(CategoryName, "desc", 18, 45, PartEmail);
            string expected_results = "Only organizers";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

            //check no category was saved
            int test_count = Count($"SELECT COUNT(*) FROM Catergories WHERE CatergoryName='{CategoryName}'");
            int expected_count = 0;
            Assert.Equal(expected_count, test_count);

        }//end of method test_addCategoryByParticipant


        //test that a category can be updated
        //using the Catergoryupdate_byId() method
        [Fact]
        public void test_updateCategory()
        {//start of method

            //seed the category
            int orgId = SeedOrganizer(OrgEmail);
            int catId = SeedCategory(CategoryName, orgId);

            //creating an instance of the class
            //with an object update_category
            Categories update_category = new Categories();

            //test result and expected results
            string test_results = update_category.Catergoryupdate_byId(catId, "XUNIT_CatRenamed", "new desc", 20, 50);
            string expected_results = "updated successfully";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

            //check the category changed
            int test_count = Count($"SELECT COUNT(*) FROM Catergories WHERE CatergoryId={catId} AND CatergoryName='XUNIT_CatRenamed' AND MaxAge=50");
            int expected_count = 1;
            Assert.Equal(expected_count, test_count);

        }//end of method test_updateCategory


        //test that an unused category can be deleted
        //using the DeleteCategory_byId() method
        [Fact]
        public void test_deleteUnusedCategory()
        {//start of method

            //seed the category
            int orgId = SeedOrganizer(OrgEmail);
            int catId = SeedCategory(CategoryName, orgId);

            //creating an instance of the class
            //with an object delete_category
            Categories delete_category = new Categories();

            //test result and expected results
            string test_results = delete_category.DeleteCategory_byId(catId);
            string expected_results = "deleted successfully";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

            //check the row is gone
            int test_count = Count($"SELECT COUNT(*) FROM Catergories WHERE CatergoryId={catId}");
            int expected_count = 0;
            Assert.Equal(expected_count, test_count);

        }//end of method test_deleteUnusedCategory


        //test that a category used by an event cannot be deleted
        //using the DeleteCategory_byId() method
        [Fact]
        public void test_deleteUsedCategory()
        {//start of method

            //seed the event with its category
            var setup = SeedEventSetup();

            //creating an instance of the class
            //with an object delete_category
            Categories delete_category = new Categories();

            //test result and expected results
            string test_results = delete_category.DeleteCategory_byId(setup.CategoryId);
            string expected_results = "cannot be deleted";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

            //check the category and the event link are still there
            int test_categoryCount = Count($"SELECT COUNT(*) FROM Catergories WHERE CatergoryId={setup.CategoryId}");
            int test_eventCount = Count($"SELECT COUNT(*) FROM Events WHERE EventId={setup.EventId} AND Category_id IS NOT NULL");
            int expected_count = 1;
            Assert.Equal(expected_count, test_categoryCount);
            Assert.Equal(expected_count, test_eventCount);

        }//end of method test_deleteUsedCategory


        //---------------- results ----------------

        //test that an organizer can create a result
        //using the CreateResult() method
        [Fact]
        public void test_createResultByOrganizer()
        {//start of method

            //seed the enrollment
            int enrollmentId = SeedEnrollmentSetup(10);

            //creating an instance of the class
            //with an object create_result
            Result create_result = new Result();

            //test result and expected results
            string test_results = create_result.CreateResult("01:45:30", 1, "Won", 10, EventName, OrgEmail);
            string expected_results = "created successfully";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

            //check the result is linked to the enrollment
            int test_count = Count($"SELECT COUNT(*) FROM Results WHERE Enrollment_id={enrollmentId} AND RecordedAt IS NOT NULL");
            int expected_count = 1;
            Assert.Equal(expected_count, test_count);

        }//end of method test_createResultByOrganizer


        //test that a participant cannot create a result
        //using the CreateResult() method
        [Fact]
        public void test_createResultByParticipant()
        {//start of method

            //seed the enrollment
            int enrollmentId = SeedEnrollmentSetup(10);

            //creating an instance of the class
            //with an object create_result
            Result create_result = new Result();

            //test result and expected results
            string test_results = create_result.CreateResult("01:45:30", 1, "Won", 10, EventName, PartEmail);
            string expected_results = "Only organizers";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

            //check no result was saved
            int test_count = Count($"SELECT COUNT(*) FROM Results WHERE Enrollment_id={enrollmentId}");
            int expected_count = 0;
            Assert.Equal(expected_count, test_count);

        }//end of method test_createResultByParticipant


        //test that an unknown bib number is refused
        //using the CreateResult() method
        [Fact]
        public void test_createResultUnknownBibNumber()
        {//start of method

            //seed the enrollment
            int enrollmentId = SeedEnrollmentSetup(10);

            //creating an instance of the class
            //with an object create_result
            Result create_result = new Result();

            //test result and expected results
            string test_results = create_result.CreateResult("01:45:30", 1, "Won", 999, EventName, OrgEmail);
            string expected_results = "No enrollment found";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

            //check no result was saved
            int test_count = Count($"SELECT COUNT(*) FROM Results WHERE Enrollment_id={enrollmentId}");
            int expected_count = 0;
            Assert.Equal(expected_count, test_count);

        }//end of method test_createResultUnknownBibNumber


        //test that a result can be found by its id
        //using the ResultbyID() method
        [Fact]
        public void test_resultByIdFound()
        {//start of method

            //seed the result
            int resultId = SeedResult(SeedEnrollmentSetup());

            //creating an instance of the class
            //with an object get_result
            Result get_result = new Result();

            //test result and expected results
            string test_results = get_result.ResultbyID(resultId);
            string expected_results = "Finish Time: 01:45:30";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

        }//end of method test_resultByIdFound


        //test that an unknown result id returns not found
        //using the ResultbyID() method
        [Fact]
        public void test_resultByIdNotFound()
        {//start of method

            //creating an instance of the class
            //with an object get_result
            Result get_result = new Result();

            //test result and expected results
            string test_results = get_result.ResultbyID(-1);
            string expected_results = "Result not found";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

        }//end of method test_resultByIdNotFound


        //test that all results include the event name
        //using the GetAllResults() method
        [Fact]
        public void test_getAllResults()
        {//start of method

            //seed the result
            SeedResult(SeedEnrollmentSetup());

            //creating an instance of the class
            //with an object get_results
            Result get_results = new Result();

            //test result and expected results
            string test_results = get_results.GetAllResults();
            string expected_results = EventName;

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

        }//end of method test_getAllResults


        //test that a result time, position and status can be updated
        //using the UpdateResult_byId() method
        [Fact]
        public void test_updateResult()
        {//start of method

            //seed the result
            int resultId = SeedResult(SeedEnrollmentSetup());

            //creating an instance of the class
            //with an object update_result
            Result update_result = new Result();

            //test result and expected results
            string test_results = update_result.UpdateResult_byId(resultId, "02:00:00", 2, "Lost");
            string expected_results = "updated successfully";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

            //check the position and status changed
            int test_count = Count($"SELECT COUNT(*) FROM Results WHERE ResultId={resultId} AND position=2 AND ResultStatus='Lost'");
            int expected_count = 1;
            Assert.Equal(expected_count, test_count);

        }//end of method test_updateResult


        //test that a result can be deleted
        //using the DeleteResult_byId() method
        [Fact]
        public void test_deleteResult()
        {//start of method

            //seed the result
            int resultId = SeedResult(SeedEnrollmentSetup());

            //creating an instance of the class
            //with an object delete_result
            Result delete_result = new Result();

            //test result and expected results
            string test_results = delete_result.DeleteResult_byId(resultId);
            string expected_results = "deleted successfully";

            //then use assert.contains()
            Assert.Contains(expected_results, test_results);

            //check the row is gone
            int test_count = Count($"SELECT COUNT(*) FROM Results WHERE ResultId={resultId}");
            int expected_count = 0;
            Assert.Equal(expected_count, test_count);

        }//end of method test_deleteResult

    }//end of class

}//end of namespace