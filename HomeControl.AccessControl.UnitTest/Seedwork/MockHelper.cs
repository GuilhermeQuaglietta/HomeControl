using HomeControl.AccessControl.Domain.Users;
using HomeControl.Core.Infrastructure.Contract;
using HomeControl.Identity.Jwt;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeControl.AccessControl.UnitTest.Seedwork
{
    public static class MockHelper
    {
        public static IUserQueries GenerateIUserQueries(User generalUser)
        {
            Mock<IUserQueries> mock = GenerateIUserQueriesMock(generalUser, generalUser, generalUser);
            return mock.Object;
        }
        public static IUserQueries GenerateIUserQueries(User loginUser, User findByEmailUser, User findByRecoveryKeyUser)
        {
            Mock<IUserQueries> mock = GenerateIUserQueriesMock(loginUser, findByEmailUser, findByRecoveryKeyUser);
            return mock.Object;
        }
        public static Mock<IUserQueries> GenerateIUserQueriesMock(User generalUser)
        {
            return GenerateIUserQueriesMock(generalUser, generalUser, generalUser);
        }
        public static Mock<IUserQueries> GenerateIUserQueriesMock(User loginUser, User findByEmailUser, User findByRecoveryKeyUser)
        {
            Mock<IUserQueries> mock = new Mock<IUserQueries>();
            mock.Setup(x => x.FindByEmail(It.IsAny<string>())).Returns(findByEmailUser);
            mock.Setup(x => x.FindByRecoveryKey(It.IsAny<string>())).Returns(findByRecoveryKeyUser);
            mock.Setup(x => x.LoginUser(It.IsAny<string>(), It.IsAny<string>())).Returns(loginUser);
            return mock;
        }


        public static Mock<IUserRepository> GenerateIUserRepositoryMock(User entity)
        {
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            return GenerateIRepositoryMock(mock, entity);
        }

        private static Mock<TRepository> GenerateIRepositoryMock<TRepository, TEntity>(Mock<TRepository> mock, TEntity entity)
            where TEntity : class
            where TRepository : class, IRepository<TEntity>
        {
            return GenerateIRepositoryMock(mock, entity, new List<TEntity>() { entity });
        }
        private static Mock<TRepository> GenerateIRepositoryMock<TRepository, TEntity>(Mock<TRepository> mock, TEntity entity, List<TEntity> entities)
            where TEntity : class
            where TRepository : class, IRepository<TEntity>
        {
            return GenerateIRepositoryMock(mock, entity, entities, null);
        }
        private static Mock<TRepository> GenerateIRepositoryMock<TRepository, TEntity>(Mock<TRepository> mock, TEntity entity, List<TEntity> entities, List<TEntity> duplicateEntities)
            where TEntity : class
            where TRepository : class, IRepository<TEntity>
        {
            mock.Setup(x => x.Get(It.IsAny<int>())).Returns(entity);
            mock.Setup(x => x.GetAll()).Returns(entities);
            mock.Setup(x => x.GetDuplicates(It.IsAny<TEntity>())).Returns(duplicateEntities);
            mock.Setup(x => x.Find(It.IsAny<int>())).Returns(entity);
            mock.Setup(x => x.Insert(It.IsAny<TEntity>()));
            mock.Setup(x => x.Insert((It.IsAny<IEnumerable<TEntity>>())));
            mock.Setup(x => x.Update(It.IsAny<TEntity>()));
            mock.Setup(x => x.Update((It.IsAny<IEnumerable<TEntity>>())));
            mock.Setup(x => x.Delete(It.IsAny<TEntity>()));
            mock.Setup(x => x.Delete((It.IsAny<IEnumerable<TEntity>>())));
            mock.Setup(x => x.Delete(It.IsAny<int>()));
            return mock;
        }

    }
}
