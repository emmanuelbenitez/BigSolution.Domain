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

            public IEnumerable<Child> Children => CollectionFacade.Entities;
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
