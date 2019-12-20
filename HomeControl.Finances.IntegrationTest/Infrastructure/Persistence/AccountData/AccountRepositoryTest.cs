using HomeControl.Finances.Infrastructure.Persistence.AccountData;
using HomeControl.Finances.Infrastructure.Persistence.AccountData.Entity;
using HomeControl.Finances.Infrastructure.Persistence.AccountData.Repository;
using HomeControl.Finances.IntegrationTest.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace HomeControl.Finances.IntegrationTest.Infrastructure.Persistence.Account
{
    [TestClass]
    public class AccountRepositoryTest
    {
        private readonly AccountRepository _repository;

        public AccountRepositoryTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseSqlServer(AppSettings.ConnectionString);
            AccountDbContext dbContext = new AccountDbContext(optionsBuilder.Options);
            _repository = new AccountRepository(dbContext);
        }


        [TestMethod]
        public void AccountRepository_InsertAccount_Verify_AccountIdIsGenerated()
        {
            AccountEntity account = GenerateValidAccountEntity(0);

            _repository.Insert(account);
            Assert.AreNotEqual(0, account.AccountId);
        }

        [TestMethod]
        public void AccountRepository_InsertAccount_Verify_AllFieldsArePersisted()
        {
            AccountEntity account = GenerateValidAccountEntity(0);

            _repository.Insert(account);
            Assert.AreNotEqual(0, account.AccountId);

            var persistedAccount = _repository.Get(account.AccountId);
            Assert.AreEqual(account.Title, persistedAccount.Title);
            Assert.AreEqual(account.HighlightColor, persistedAccount.HighlightColor);
            Assert.AreEqual(account.OwnerId, persistedAccount.OwnerId);
        }

        [TestMethod]
        public void AccountRepository_InsertMultipleAccounts_Verify_AllAccountsPersisted()
        {
            List<AccountEntity> account = new List<AccountEntity>
            {
                GenerateValidAccountEntity(0),
                GenerateValidAccountEntity(0),
                GenerateValidAccountEntity(0)
            };

            _repository.Insert(account);
            account.ForEach(x => Assert.AreNotEqual(0, x.AccountId));
        }

        [TestMethod]
        public void AccountRepository_UpdateAccount_Verify_AllFieldsArePersisted()
        {
            var account = GenerateValidAccountEntity(0);

            _repository.Insert(account);
            Assert.AreNotEqual(0, account.AccountId);

            account.Title = "Test Update";
            _repository.Update(account);

            var persistedAccount = _repository.Get(account.AccountId);
            Assert.AreEqual(account.Title, persistedAccount.Title);
            Assert.AreEqual(account.HighlightColor, persistedAccount.HighlightColor);
            Assert.AreEqual(account.OwnerId, persistedAccount.OwnerId);
        }

        [TestMethod]
        public void AccountRepository_UpdateAccount_Verify_AllAccountsPersisted()
        {
            List<AccountEntity> accountCollection = new List<AccountEntity>
            {
                GenerateValidAccountEntity(0),
                GenerateValidAccountEntity(0),
                GenerateValidAccountEntity(0)
            };
            _repository.Insert(accountCollection);

            foreach (var item in accountCollection)
            {
                var persistedAccount = _repository.Get(item.AccountId);
                Assert.AreEqual(item.Title, persistedAccount.Title);
                Assert.AreEqual(item.HighlightColor, persistedAccount.HighlightColor);
                Assert.AreEqual(item.OwnerId, persistedAccount.OwnerId);
            }
        }

        [TestMethod]
        public void AccountRepository_DeleteAccount_Verify_AccountCantBeQueried()
        {
            var account = GenerateValidAccountEntity(0);
            _repository.Insert(account);

            var persistedAccount = _repository.Get(account.AccountId);
            _repository.Delete(persistedAccount.AccountId);

            var deletedAccount = _repository.Get(account.AccountId);
            Assert.IsNull(deletedAccount);
        }

        [TestMethod]
        public void AccountRepository_DeleteAccount_Verify_AllAccountsPersisted()
        {
            List<AccountEntity> accountCollection = new List<AccountEntity>
            {
                GenerateValidAccountEntity(0),
                GenerateValidAccountEntity(0),
                GenerateValidAccountEntity(0)
            };

            _repository.Insert(accountCollection);
            accountCollection.ForEach(x => Assert.AreNotEqual(0, x.AccountId));

            _repository.Delete(accountCollection);

            foreach (var item in accountCollection)
            {
                var deletedAccount = _repository.Get(item.AccountId);
                Assert.IsNull(deletedAccount);
            }
        }

        public AccountEntity GenerateValidAccountEntity(int id)
        {
            return new AccountEntity()
            {
                AccountId = id,
                HighlightColor = 1,
                OwnerId = 1,
                Title = "Test Account",
            };
        }
    }
}
