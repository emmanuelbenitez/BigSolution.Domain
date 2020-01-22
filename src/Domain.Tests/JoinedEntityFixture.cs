using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace BigSolution.Infra.Domain
{
    public class JoinedEntityFixture
    {
        [Fact]
        public void CreateSucceeds()
        {
            Action action = () => {
                var unused = new Parent();
            };
            action.Should().NotThrow();
        }

        [Fact]
        public void CreateWithIdSucceeds()
        {
            Action action = () => {
                var unused = new Parent(1);
            };
            action.Should().NotThrow();
        }

        [Fact]
        public void GetCollectionFacadeSucceeds()
        {
            new Parent().Children.Should().NotBeNull();
        }

        [ExcludeFromCodeCoverage]
        public sealed class Parent : JoinedEntity<Parent, int, Child, Link>
        {
            public Parent() { }

            public Parent(int id) : base(id) { }

            public IEnumerable<Child> Children => CollectionFacade;
        }

        [ExcludeFromCodeCoverage]
        [UsedImplicitly]
        public sealed class Child : Entity<int> { }

        [ExcludeFromCodeCoverage]
        public sealed class Link : IJoinEntity<Parent>, IJoinEntity<Child>
        {
            #region IJoinEntity<Child> Members

            Child IJoinEntity<Child>.Navigation
            {
                get => throw new NotImplementedException();
                set => throw new NotImplementedException();
            }

            #endregion

            #region IJoinEntity<Parent> Members

            Parent IJoinEntity<Parent>.Navigation
            {
                get => throw new NotImplementedException();
                set => throw new NotImplementedException();
            }

            #endregion
        }
    }
}
