using NUnit.Framework;
using System;
using System.Linq;

namespace Collections.Unit.Tests
{
    public class CollectionUnitTests
    {


        [Test]
        public void Test_Collection_EmptyConstructor()
        {
            //Arrange
            var nums = new Collection<int>();

            //Act

            //Assert
            Assert.That(nums.ToString(), Is.EqualTo("[]"));

        }

        [Test]
        public void Test_Collection_ConstructorSingleItem()
        {
            //Arrange
            var nums = new Collection<int>(5);
            //Act

            //Assert
            Assert.That(nums.ToString(), Is.EqualTo("[5]"));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));

        }
        [Test]
        public void Test_Collection_ConstructorMultipleItems()
        {
            //Arrange
            var nums = new Collection<int>(10, 20, 30);

            //Act

            //Assert
            Assert.That(nums.ToString(), Is.EqualTo("[10, 20, 30]"));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }
        [Test]
        public void Test_Collection_Add()
        {
            //Arrange
            var nums = new Collection<int>(10, 20, 30);
            nums.Add(40);

            //Act

            //Assert
            Assert.That(nums.ToString(), Is.EqualTo("[10, 20, 30, 40]"));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }

        [Test]
        public void Test_Collection_AddWithGrow()
        {
            //Arrange
            var nums = new Collection<int>(10, 20, 30);
            int oldCapacity = nums.Capacity;

            //Act
            for (int i = 0; i < 50; i++)
                nums.Add(i);

            //Assert
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            string expectedNums =
                "[10, 20, 30, " +
                string.Join(", ", Enumerable.Range(0, 50)) +
                "]";
            Assert.That(nums.ToString(), Is.EqualTo(expectedNums));
        }
        [Test]
        public void Test_AddRange()
        {
            //Arrange
            var names = new Collection<string>("Ivan", "Dragan");


            //Act
            names.AddRange("Petkan", "Nikolay", "Dimitar");

            //Assert
            Assert.That(names.ToString(), Is.EqualTo("[Ivan, Dragan, Petkan, Nikolay, Dimitar]"));
            Assert.That(names.Capacity, Is.GreaterThanOrEqualTo(names.Count));
        }

        [Test]
        public void Test_Collection_GetByIndex()
        {
            //Arrange
            var names = new Collection<string>("Ivan", "Dragan");

            //Act
            var item0 = names[0];
            var item1 = names[1];

            //Assert
            Assert.That(item0, Is.EqualTo("Ivan"));
            Assert.That(item1, Is.EqualTo("Dragan"));
            Assert.That(item0 != item1);
        }
        [Test]
        public void Test_Collection_GetByInvalidIndex()
        {
            //Arrange
            var names = new Collection<string>("Ivan", "Dragan");

            //Act

            //Assert
            Assert.That(() => { var name = names[-1]; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());

            Assert.That(() => { var name = names[2]; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());

            Assert.That(() => { var name = names[500]; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());

            Assert.That(names.ToString(), Is.EqualTo("[Ivan, Dragan]"));


        }

        [Test]
        public void Test_Collection_SetByIndex()
        {
            //Arrange
            var names = new Collection<string>("Ivan", "Dragan");

            //Act
            names[0] = "Ivan";
            names[1] = "Dragan";

            //Assert
            Assert.That(names.ToString(), Is.EqualTo("[Ivan, Dragan]"));



        }
        [Test]
        public void Test_Collection_SetByInvalidIndex()
        {
            //Arrange
            var names = new Collection<string>("Ivan", "Dragan");

            //Act

            //Assert
            Assert.That(() => { names[-1] = "new item"; },
               Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { names[-2] = "new item"; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { names[500] = "new item"; },
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[Ivan, Dragan]"));


        }
        [Test]
        public void Test_Collection_AddRangeWithGrow()
        {
            //Arrange
            var nums = new Collection<int>(1, 2);

            //Act
            int oldCapacity = nums.Capacity;
            nums.AddRange(Enumerable.Range(1000, 2000).ToArray());
            string expectedNums = "[1, 2, " + string.Join(", ", Enumerable.Range(1000, 2000)) + "]";

            //Assert
            Assert.That(nums.ToString(), Is.EqualTo(expectedNums));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));

        }
        [Test]
        public void Test_Collection_InsertAtStart()
        {
            //Arrange
            var names = new Collection<string>("Ivan", "Dragan");

            //Act
            names.InsertAt(0, "Petkan");

            //Assert
            Assert.That(names.ToString(), Is.EqualTo("[Petkan, Ivan, Dragan]"));
            Assert.That(names.Capacity, Is.GreaterThanOrEqualTo(names.Count));

        }
        [Test]
        public void Test_Collection_InsertAtMiddle()
        {
            //Arrange
            var names = new Collection<string>("Ivan", "Dragan");
            //Act
            names.InsertAt(1, "Petkan");
            //Assert
            Assert.That(names.ToString(), Is.EqualTo("[Ivan, Petkan, Dragan]"));
            Assert.That(names.Capacity, Is.GreaterThanOrEqualTo(names.Count));

        }
        [Test]
        public void Test_Collection_InsertAtEnd()
        {
            //Arrange
            var names = new Collection<string>("Ivan", "Dragan");

            //Act
            names.InsertAt(2, "Petkan");
            //Assert
            Assert.That(names.ToString(), Is.EqualTo("[Ivan, Dragan, Petkan]"));
            Assert.That(names.Capacity, Is.GreaterThanOrEqualTo(names.Count));
        }
        [Test]
        public void Test_Collection_InsertAtWithGrow()
        {
            //Arrange
            var names = new Collection<string>("Ivan", "Dragan");

            //Act
            int oldCapacity = names.Capacity;
            names.InsertAt(0, "Petkan");
            names.InsertAt(3, "Nikolay");
            names.InsertAt(4, "Rusev");
            for (int i = names.Count; i >= 0; i--)
                names.InsertAt(i, "Item" + i);

            //Assert
            Assert.That(names.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(names.ToString(), Is.EqualTo(
                "[Item0, Petkan, Item1, Ivan, Item2, Dragan, Item3, Nikolay, Item4, Rusev, Item5]"));
            Assert.That(names.Capacity, Is.GreaterThanOrEqualTo(names.Count));
        }
        [Test]
        public void Test_Collection_InsertAtInvalidIndex()
        {
            //Arrange
            var names = new Collection<string>("Ivan", "Dragan");

            //Act

            //Assert
            Assert.That(() => names.InsertAt(-1, "Petkan"), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => names.InsertAt(-3, "Nikolay"), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => names.InsertAt(500, "Rusev"), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(),Is.EqualTo("[Ivan, Dragan]"));



        }
        [Test]
        public void Test_Collection_ExchangeFirstLast()
        {
            //Arrange
            var names = new Collection<string>("Ivan", "Dragan", "Nikolay", "Rusev");

            //Act
            names.Exchange(0, 3);

            //Assert
            Assert.That(names.ToString(), Is.EqualTo("[Rusev, Dragan, Nikolay, Ivan]"));

        }
        [Test]
        public void Test_Collection_ExchangeMiddle()
        {
            //Arrange
            var names = new Collection<string>("Ivan", "Dragan", "Petkan", "Nikolay");
            //Act
            names.Exchange(1, 2);

            //Assert
            Assert.That(names.ToString(), Is.EqualTo("[Ivan, Petkan, Dragan, Nikolay]"));

        }
        [Test]
        public void Test_Collection_ExchangeInvalidIndexes()
        {
            //Arrange
            var names = new Collection<string>("Ivan", "Dragan");

            //Act

            //Assert
            Assert.That(() => names.Exchange(-1,1), Throws.InstanceOf<ArgumentOutOfRangeException>()); 
            Assert.That(() => names.Exchange(1,-1), Throws.InstanceOf<ArgumentOutOfRangeException>()); 
            Assert.That(() => names.Exchange(2,1), Throws.InstanceOf<ArgumentOutOfRangeException>()); 
            Assert.That(() => names.Exchange(1,2), Throws.InstanceOf<ArgumentOutOfRangeException>()); 
            Assert.That(() => names.Exchange(-500,500), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[Ivan, Dragan]"));

        }
        [Test]
        public void Test_Collection_RemovedAtStart()
        {
            //Arrange
            var names = new Collection<string>("Ivan", "Dragan", "Petkan", "Nikolay");
            var removed = names.RemoveAt(0);

            //Act

            //Assert
            Assert.That(removed, Is.EqualTo("Ivan"));
            Assert.That(names.ToString(), Is.EqualTo("[Dragan, Petkan, Nikolay]"));

        }
        [Test]
        public void Test_Collection_RemovedAtEnd()
        {
            //Arrange
            var names = new Collection<string>("Ivan", "Dragan", "Petkan", "Nikolay");
            var removed = names.RemoveAt(3);
            //Act

            //Assert
            Assert.That(removed, Is.EqualTo("Nikolay"));
            Assert.That(names.ToString, Is.EqualTo("[Ivan, Dragan, Petkan]"));

        }
        [Test]
        public void Test_Collection_RemoveAtMiddle()
        {
            //Arrange
            var names = new Collection<string>("Ivan", "Dragan", "Nikolay", "Petkan");
            var removed = names.RemoveAt(1);
            //Act

            //Assert
            Assert.That(removed.ToString(), Is.EqualTo("Dragan"))  ;

        }
        [Test]
        public void Test_Collection_RemoveAtInvalidIndex()
        {
            //Arrange
            var names = new Collection<string>("Ivan", "Dragan");

            //Act

            //Assert
            Assert.That(() => names.RemoveAt(-1), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => names.RemoveAt(2), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => names.RemoveAt(400), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[Ivan, Dragan]"));

        }
        [Test]
        public void Test_Collection_RemoveAll()
        {
            //Arrange
            var nums = new Collection<int>();
            const int itemsCount = 1000;

            //Act
            nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());
            for (int i = 1; i <= itemsCount; i++)
            {
                var removed = nums.RemoveAt(0);

                //Assert
                Assert.That(removed, Is.EqualTo(i));
            }
            Assert.That(nums.ToString(), Is.EqualTo("[]"));
            Assert.That(nums.Count, Is.EqualTo(0));

            


        }
        [Test]
        public void Test_Collection_Clear()
        {
            //Arrange
            var names = new Collection<string>("Ivan", "Dragan", "Petkan", "Nikolay");

            //Act
            names.Clear();

            //Assert
            Assert.That(names.Count, Is.EqualTo(0));
            Assert.That(names.ToString(), Is.EqualTo("[]"));


        }
        [Test]
        public void Test_Collection_CountAndCapacity()
        {
            //Arrange
            var nums = new Collection<int>();

            //Act
            const int itemCount = 1000;

            //Assert
            for (int i = 1; i <= itemCount; i++)
            {
                nums.Add(i);
                Assert.That(nums.Count, Is.EqualTo(i));
                Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
            }
            for (int i = itemCount; i >= 1; i--)
            { 
                nums.RemoveAt(i -1);
                Assert.That(nums.Count, Is.EqualTo(i-1));
                Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
            }


        }
        [Test]
        [Timeout(1000)]
        public void Test_Collection_1MilionItems()
        {
            //Arrange
            const int itemsCount= 1000000;
            var nums = new Collection<int>();

            //Act
            nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());

            //Assert
            Assert.That(nums.Count, Is.EqualTo(itemsCount));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));

            for (int i = itemsCount - 1; i >= 0; i--)
                nums.RemoveAt(i);
            Assert.That(nums.ToString(), Is.EqualTo("[]"));
            Assert.That(nums.Capacity,Is.GreaterThanOrEqualTo(nums.Count));


        }
        [Test]
        public void Test_Collection_ToStringCollectionOfCollections()
        {
            //Arrange
            var names = new Collection<string>("Ivan", "Dragan");
            var nums = new Collection<int>(10, 20);
            var dates = new Collection<DateTime>();
            var nested = new Collection<object>(names, nums, dates);

            //Act

            //Assert
            Assert.That(nested.ToString(),Is.EqualTo("[[Ivan, Dragan], [10, 20], []]"));


        }
        [Test]
        public void Test_Collection_ToStringMultiple()
        {
            //Arrange
            var obj = new Collection<object>("Ivan", "Dragan", 15);

            //Act

            //Assert
            Assert.That(obj.ToString(), Is.EqualTo("[Ivan, Dragan, 15]"));


        }
        [Test]
        public void Test_Collection_ToStringSingle()
        {
            //Arrange
            var names = new Collection<string>("Ivan");

            //Act

            //Assert
            Assert.That(names.ToString(), Is.EqualTo("[Ivan]"));

        }
        [Test]
        public void Test_Collection_ToStringEmpty()
        {
            //Arrange
            var names = new Collection<string>();

            //Act

            //Assert
           // Assert.That(names.ToString(), Is.Empty);
            Assert.That(names.ToString,Is.EqualTo("[]"));


        }


    }
}

