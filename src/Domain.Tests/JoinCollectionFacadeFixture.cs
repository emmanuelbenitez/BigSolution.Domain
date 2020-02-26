using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace BigSolution.Infra.Domain
{
    public class JoinCollectionFacadeFixture
    {
        [Fact]
        public void AddSucceeds()
        {
            var collectionFacade = new JoinCollectionFacade<Child, Parent, Link>(new Parent(), new List<Link>());
            var child = new Child();
            collectionFacade.Add(child);
            collectionFacade.Entities.Should().ContainSingle(child1 => child1 == child);
        }

        [Fact]
        public void ClearSucceeds()
        {
            var collectionFacade =
                new JoinCollectionFacade<Child, Parent, Link>(new Parent(), new List<Link> { new Link() });
            Action action = () => collectionFacade.Clear();
            action.Should().NotThrow();
            collectionFacade.Should().BeEmpty();
        }

        [Fact]
        public void ContainsFailed()
        {
            var child = new Child();
            var collectionFacade =
                new JoinCollectionFacade<Child, Parent, Link>(new Parent(), new List<Link>()) { child };
            collectionFacade.Contains(new Child()).Should().BeFalse();
        }

        [Fact]
        public void ContainsSucceeds()
        {
            var child = new Child();
            var collectionFacade =
                new JoinCollectionFacade<Child, Parent, Link>(new Parent(), new List<Link>()) { child };
            collectionFacade.Contains(child).Should().BeTrue();
        }

        [Fact]
        public void CopyToSucceeds()
        {
            var item = new Child();
            var collectionFacade = new JoinCollectionFacade<Child, Parent, Link>(new Parent(), new List<Link>()) { item };
            var items = new Child[1];
            collectionFacade.CopyTo(items, 0);
            items.SingleOrDefault().Should().Be(item);
        }

        [Fact]
        public void CountSucceeds()
        {
            var collectionFacade = new JoinCollectionFacade<Child, Parent, Link>(new Parent(), new List<Link>())
                { new Child() };
            collectionFacade.Count.Should().Be(1);
        }

        [Theory]
        [MemberData(nameof(InvalidArgumentsOfConstructor))]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
        public void CreateFailed(Parent parent, ICollection<Link> links)
        {
            Action action = () => new JoinCollectionFacade<Child, Parent, Link>(parent, links);
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
        public void CreateSucceeds()
        {
            Action action = () => new JoinCollectionFacade<Child, Parent, Link>(new Parent(), new List<Link>());
            action.Should().NotThrow();
        }

        [Fact]
        public void EnumerableGetEnumeratorSucceeds()
        {
            IEnumerable collection = new JoinCollectionFacade<Child, Parent, Link>(new Parent(), new List<Link>());
            collection.GetEnumerator().Should().NotBeNull();
        }

        [Fact]
        public void GetEnumeratorSucceeds()
        {
            ((IEnumerable)new JoinCollectionFacade<Child, Parent, Link>(new Parent(), new List<Link>()))
                .GetEnumerator()
                .Should().NotBeNull();
        }

        [Fact]
        public void IsReadonlySucceeds()
        {
            new JoinCollectionFacade<Child, Parent, Link>(new Parent(), new List<Link>()).IsReadOnly.Should().BeFalse();
        }

        [Fact]
        public void RemoveSucceeds()
        {
            var item = new Child();
            var collectionFacade = new JoinCollectionFacade<Child, Parent, Link>(new Parent(), new List<Link>()) { item };
            Action action = () => collectionFacade.Remove(item);
            action.Should().NotThrow();
            collectionFacade.Should().BeEmpty();
        }

        [ExcludeFromCodeCoverage]
        public class Parent : JoinedEntity<Parent, int, Child, Link> { }

        [ExcludeFromCodeCoverage]
        public class Link : IJoinEntity<Child>, IJoinEntity<Parent>
        {
            #region IJoinEntity<Child> Members

            Child IJoinEntity<Child>.Navigation
            {
                get => Child;
                set => Child = value;
            }

            #endregion

            #region IJoinEntity<Parent> Members

            Parent IJoinEntity<Parent>.Navigation
            {
                get => Parent;
                set => Parent = value;
            }

            #endregion

            [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
            public Child Child { get; set; }

            [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
            public Parent Parent { get; set; }
        }

        [ExcludeFromCodeCoverage]
        public class Child : IEntity { }

        public static IEnumerable<object[]> InvalidArgumentsOfConstructor
        {
            [UsedImplicitly]
            get
            {
                yield return new object[] { null, new List<Link>() };
                yield return new object[] { new Parent(), null };
            }
        }
    }
}
