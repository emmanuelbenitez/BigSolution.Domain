using Xunit;

namespace BigSolution.Infra.Domain
{
    public class EntityTests
    {
        private class FakeEntity : Entity<int>
        {
            public FakeEntity()
            {
            }

            public FakeEntity(int id) : base(id)
            {
            }
        }

        [Fact]
        public void Equals_EntitiesAreNew_ReturnsFalse()
        {
            var entity = new FakeEntity();
            var entityToCompare = new FakeEntity();

            Assert.False(entity.Equals(entityToCompare));
        }

        [Fact]
        public void Equals_EntitiesDoesNotHaveSameId_ReturnsFalse()
        {
            var entity = new FakeEntity(1);
            var entityToCompare = new FakeEntity(2);

            Assert.False(entity.Equals(entityToCompare));
        }

        [Fact]
        public void Equals_EntitiesHaveSameId_ReturnsTrue()
        {
            var entity = new FakeEntity(1);
            var entityToCompare = new FakeEntity(1);

            Assert.True(entity.Equals(entityToCompare));
        }

        [Fact]
        public void Equals_ObjectIsSameReference_ReturnsTrue()
        {
            var entity = new FakeEntity();
            var entityToCompare = entity;

            Assert.True(entity.Equals(entityToCompare));
        }

        [Fact]
        public void OperatorNotEqual_EntitiesAreNew_ReturnsTrue()
        {
            var entity = new FakeEntity();
            var entityToCompare = new FakeEntity();

            Assert.True(entity != entityToCompare);
        }

        [Fact]
        public void OperatorNotEqual_EntitiesDoesNotHaveSameId_ReturnsTrue()
        {
            var entity = new FakeEntity(1);
            var entityToCompare = new FakeEntity(2);

            Assert.True(entity != entityToCompare);
        }

        [Fact]
        public void OperatorNotEqual_EntitiesHaveSameId_ReturnsFalse()
        {
            var entity = new FakeEntity(1);
            var entityToCompare = new FakeEntity(1);

            Assert.False(entity != entityToCompare);
        }

        [Fact]
        public void OperatorNotEqual_ObjectIsSameReference_ReturnsFalse()
        {
            var entity = new FakeEntity();
            var entityToCompare = entity;

            Assert.False(entity != entityToCompare);
        }
    }
}