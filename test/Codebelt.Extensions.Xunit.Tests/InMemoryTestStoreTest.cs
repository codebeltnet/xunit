using System.Linq;
using Xunit;

namespace Codebelt.Extensions.Xunit
{
    /// <summary>
    /// Tests for the <see cref="InMemoryTestStore{T}"/> class.
    /// </summary>
    public class InMemoryTestStoreTest : Test
    {
        public InMemoryTestStoreTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Constructor_ShouldInitializeEmptyStore()
        {
            var sut = new InMemoryTestStore<string>();

            Assert.Equal(0, sut.Count);
        }

        [Fact]
        public void Add_ShouldAddItemToStore()
        {
            var sut = new InMemoryTestStore<string>();

            sut.Add("test");

            Assert.Equal(1, sut.Count);
        }

        [Fact]
        public void Add_ShouldAddMultipleItems()
        {
            var sut = new InMemoryTestStore<string>();

            sut.Add("first");
            sut.Add("second");
            sut.Add("third");

            Assert.Equal(3, sut.Count);
        }

        [Fact]
        public void Add_ShouldAllowDuplicateItems()
        {
            var sut = new InMemoryTestStore<string>();

            sut.Add("duplicate");
            sut.Add("duplicate");

            Assert.Equal(2, sut.Count);
        }

        [Fact]
        public void Add_ShouldAllowNullValues()
        {
            var sut = new InMemoryTestStore<string>();

            sut.Add(null);

            Assert.Equal(1, sut.Count);
        }

        [Fact]
        public void Count_ShouldReturnZero_WhenStoreIsEmpty()
        {
            var sut = new InMemoryTestStore<int>();

            Assert.Equal(0, sut.Count);
        }

        [Fact]
        public void Count_ShouldReturnCorrectNumber_AfterAddingItems()
        {
            var sut = new InMemoryTestStore<int>();

            sut.Add(1);
            sut.Add(2);
            sut.Add(3);

            Assert.Equal(3, sut.Count);
        }

        [Fact]
        public void Query_ShouldReturnAllItems_WhenPredicateIsNull()
        {
            var sut = new InMemoryTestStore<string>();
            sut.Add("alpha");
            sut.Add("beta");
            sut.Add("gamma");

            var result = sut.Query(null);

            Assert.Equal(3, result.Count());
            Assert.Contains("alpha", result);
            Assert.Contains("beta", result);
            Assert.Contains("gamma", result);
        }

        [Fact]
        public void Query_ShouldReturnAllItems_WhenNoPredicateProvided()
        {
            var sut = new InMemoryTestStore<string>();
            sut.Add("one");
            sut.Add("two");

            var result = sut.Query();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void Query_ShouldReturnFilteredItems_WhenPredicateProvided()
        {
            var sut = new InMemoryTestStore<int>();
            sut.Add(1);
            sut.Add(2);
            sut.Add(3);
            sut.Add(4);
            sut.Add(5);

            var result = sut.Query(x => x > 3);

            Assert.Equal(2, result.Count());
            Assert.Contains(4, result);
            Assert.Contains(5, result);
        }

        [Fact]
        public void Query_ShouldReturnEmptySequence_WhenNoItemsMatchPredicate()
        {
            var sut = new InMemoryTestStore<string>();
            sut.Add("cat");
            sut.Add("dog");

            var result = sut.Query(x => x.StartsWith("z"));

            Assert.Empty(result);
        }

        [Fact]
        public void Query_ShouldReturnEmptySequence_WhenStoreIsEmpty()
        {
            var sut = new InMemoryTestStore<string>();

            var result = sut.Query(x => x.Length > 5);

            Assert.Empty(result);
        }

        [Fact]
        public void Query_ShouldSupportComplexPredicates()
        {
            var sut = new InMemoryTestStore<string>();
            sut.Add("apple");
            sut.Add("apricot");
            sut.Add("banana");
            sut.Add("avocado");

            var result = sut.Query(x => x.StartsWith("a") && x.Length > 5);

            Assert.Equal(2, result.Count());
            Assert.Contains("apricot", result);
            Assert.Contains("avocado", result);
        }

        [Fact]
        public void QueryFor_ShouldReturnOnlyMatchingType()
        {
            var sut = new InMemoryTestStore<Animal>();
            sut.Add(new Dog { Name = "Buddy" });
            sut.Add(new Cat { Name = "Whiskers" });
            sut.Add(new Dog { Name = "Max" });

            var dogs = sut.QueryFor<Dog>();

            Assert.Equal(2, dogs.Count());
            Assert.All(dogs, dog => Assert.IsType<Dog>(dog));
        }

        [Fact]
        public void QueryFor_ShouldReturnEmptySequence_WhenNoMatchingType()
        {
            var sut = new InMemoryTestStore<Animal>();
            sut.Add(new Dog { Name = "Buddy" });
            sut.Add(new Dog { Name = "Max" });

            var cats = sut.QueryFor<Cat>();

            Assert.Empty(cats);
        }

        [Fact]
        public void QueryFor_ShouldReturnEmptySequence_WhenStoreIsEmpty()
        {
            var sut = new InMemoryTestStore<Animal>();

            var dogs = sut.QueryFor<Dog>();

            Assert.Empty(dogs);
        }

        [Fact]
        public void QueryFor_ShouldNotReturnDerivedTypes()
        {
            var sut = new InMemoryTestStore<Animal>();
            sut.Add(new Dog { Name = "Buddy" });
            sut.Add(new ServiceDog { Name = "Guide", Task = "Assistance" });

            var dogs = sut.QueryFor<Dog>();

            Assert.Single(dogs);
            Assert.Equal("Buddy", dogs.First().Name);
        }

        [Fact]
        public void QueryFor_ShouldWorkWithMultipleTypes()
        {
            var sut = new InMemoryTestStore<Animal>();
            sut.Add(new Dog { Name = "Buddy" });
            sut.Add(new Cat { Name = "Whiskers" });
            sut.Add(new Dog { Name = "Max" });
            sut.Add(new Cat { Name = "Fluffy" });

            var dogs = sut.QueryFor<Dog>();
            var cats = sut.QueryFor<Cat>();

            Assert.Equal(2, dogs.Count());
            Assert.Equal(2, cats.Count());
        }

        [Fact]
        public void InMemoryTestStore_ShouldWorkWithReferenceTypes()
        {
            var sut = new InMemoryTestStore<TestEntity>();
            var entity1 = new TestEntity { Id = 1, Name = "First" };
            var entity2 = new TestEntity { Id = 2, Name = "Second" };

            sut.Add(entity1);
            sut.Add(entity2);

            var result = sut.Query(e => e.Id == 1);

            Assert.Single(result);
            Assert.Equal("First", result.First().Name);
        }

        [Fact]
        public void InMemoryTestStore_ShouldWorkWithValueTypes()
        {
            var sut = new InMemoryTestStore<int>();

            sut.Add(10);
            sut.Add(20);
            sut.Add(30);

            var result = sut.Query(x => x >= 20);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void InMemoryTestStore_ShouldMaintainInsertionOrder()
        {
            var sut = new InMemoryTestStore<string>();

            sut.Add("first");
            sut.Add("second");
            sut.Add("third");

            var result = sut.Query().ToList();

            Assert.Equal("first", result[0]);
            Assert.Equal("second", result[1]);
            Assert.Equal("third", result[2]);
        }

        // Test helper classes
        private abstract class Animal
        {
            public string Name { get; set; }
        }

        private class Dog : Animal
        {
        }

        private class ServiceDog : Dog
        {
            public string Task { get; set; }
        }

        private class Cat : Animal
        {
        }

        private class TestEntity
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
