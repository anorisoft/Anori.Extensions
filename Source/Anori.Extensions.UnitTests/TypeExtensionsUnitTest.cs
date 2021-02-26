using NUnit.Framework;
using System.Reflection;

namespace Anori.Extensions.UnitTests
{
    public class TypeExtensionsUnitTests
    {
        [Test]
        public void TypeExtensions_IsNullable_int_ReturnFalse()
        {
           Assert.False(typeof(int).IsNullable());
        }

        [Test]
        public void TypeExtensions_IsNullable_nullable_int_ReturnTrue()
        {
            Assert.True(typeof(int?).IsNullable());
        }

        [Test]
        public void TypeExtensions_IsNullableOf_int_ReturnFalse()
        {
            Assert.False(TypeExtensions.IsNullable<int>());
        }

        [Test]
        public void TypeExtensions_IsNullableOf_nullable_int_ReturnTrue()
        {
            Assert.True(TypeExtensions.IsNullable<int?>());
        }

        [Test]
        public void TypeExtensions_IsNullable_GetTypeInfo_int_ReturnFalse()
        {
            Assert.False(typeof(int).GetTypeInfo().IsNullable());
        }

        [Test]
        public void TypeExtensions_IsNullable_GetTypeInfo_nullable_int_ReturnTrue()
        {
            Assert.True(typeof(int?).GetTypeInfo().IsNullable());
        }
    }
}