#region Copyright & License

// Copyright © 2020 - 2020 Emmanuel Benitez
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
            items.Should().ContainSingle(child => child == item);
        }

        [Fact]
        public void CountSucceeds()
        {
            var collectionFacade = new JoinCollectionFacade<Child, Parent, Link>(new Parent(), new List<Link>())
                { new Child() };
            collectionFacade.Should().ContainSingle();
        }

        [Theory]
        [MemberData(nameof(InvalidArgumentsOfConstructor))]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
        public void CreateFailed(Parent parent, ICollection<Link> links)
        {
            Action act = () => new JoinCollectionFacade<Child, Parent, Link>(parent, links);
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
        public void CreateSucceeds()
        {
            Action act = () => new JoinCollectionFacade<Child, Parent, Link>(new Parent(), new List<Link>());
            act.Should().NotThrow();
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
            ((IEnumerable) new JoinCollectionFacade<Child, Parent, Link>(new Parent(), new List<Link>()))
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
            Action act = () => collectionFacade.Remove(item);
            act.Should().NotThrow();
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
