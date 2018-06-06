using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeTracker.Shared;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void Register_registers_user()
        {
            var options = new DbContextOptionsBuilder<TrackerDbContext>().UseInMemoryDatabase(databaseName: "Register_registers_new_user").Options;
            var name = "testName";
            var surname = "testSurname";
            var username = "user493";
            var password = "afij94sf1sf";

            using (var context = new TrackerDbContext(options))
            {
                var repository = new TrackerSqlRepository(context);
                repository.RegisterUser(new User(name, surname, username, password));
            }

            using (var context = new TrackerDbContext(options))
            {
                var repository = new TrackerSqlRepository(context);

                /*Check if only one user is stored in database.*/
                Assert.AreEqual(1, context.Users.Count());

                /*Check if stored user has same data as original.*/
                Assert.AreEqual(name, context.Users.Single().Name);
                Assert.AreEqual(surname, context.Users.Single().Surname);
            }
        }

        [TestMethod]
        public void Free_checks_available_usernames()
        {
            var options = new DbContextOptionsBuilder<TrackerDbContext>().UseInMemoryDatabase(databaseName: "Check_for_free_username").Options;
            var name = "testName";
            var surname = "testSurname";
            var takenUsername = "user493";
            var password = "afij94sf1sf";
            var freeUsername = takenUsername + "k";

            using (var context = new TrackerDbContext(options))
            {
                var repository = new TrackerSqlRepository(context);
                repository.RegisterUser(new User(name, surname, takenUsername, password));
            }

            using (var context = new TrackerDbContext(options))
            {
                var repository = new TrackerSqlRepository(context);

                /*Check if method return false for free username and true for taken username.*/
                Assert.IsTrue(repository.UserExists(takenUsername));
                Assert.IsFalse(repository.UserExists(freeUsername));
            }
        }

        [TestMethod]
        public void Get_returns_registered_user()
        {
            var options = new DbContextOptionsBuilder<TrackerDbContext>().UseInMemoryDatabase(databaseName: "Retrieve_user").Options;
            var name = "testName";
            var surname = "testSurname";
            var takenUsername = "user493";
            var password = "afij94sf1sf";
            var freeUsername = takenUsername + "k";
            var wrongPassword = password + "l";

            using (var context = new TrackerDbContext(options))
            {
                var repository = new TrackerSqlRepository(context);
                repository.RegisterUser(new User(name, surname, takenUsername, password));
            }

            using (var context = new TrackerDbContext(options))
            {
                var repository = new TrackerSqlRepository(context);

                /*Check if method returns stored user when provided right credentials and null when provided wrong credentials.*/
                var user = repository.GetUser(new TimeTracker.Shared.Model.LoginModel(takenUsername, password));
                Assert.IsNotNull(user);
                Assert.AreEqual(name, user.Name);
                Assert.AreEqual(surname, user.Surname);
                Assert.AreEqual(takenUsername, user.Username);
                Assert.AreEqual(password.GetHashCode(), user.PasswordHash);

                Assert.IsNull(repository.GetUser(new TimeTracker.Shared.Model.LoginModel(takenUsername, wrongPassword)));
                Assert.IsNull(repository.GetUser(new TimeTracker.Shared.Model.LoginModel(freeUsername, password)));
            }
        }
    }
}
