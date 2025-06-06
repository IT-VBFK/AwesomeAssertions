﻿using System;
using FluentAssertions.Common;
using FluentAssertions.Types;
using Xunit;
using Xunit.Sdk;

namespace FluentAssertions.Specs.Types;

public class MethodInfoSelectorAssertionSpecs
{
    public class BeVirtual
    {
        [Fact]
        public void When_asserting_methods_are_virtual_and_they_are_it_should_succeed()
        {
            // Arrange
            var methodSelector = new MethodInfoSelector(typeof(ClassWithAllMethodsVirtual));

            // Act
            Action act = () =>
                methodSelector.Should().BeVirtual();

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void When_asserting_methods_are_virtual_but_non_virtual_methods_are_found_it_should_throw()
        {
            // Arrange
            var methodSelector = new MethodInfoSelector(typeof(ClassWithNonVirtualPublicMethods));

            // Act
            Action act = () =>
                methodSelector.Should().BeVirtual();

            // Assert
            act.Should().Throw<XunitException>();
        }

        [Fact]
        public void
            When_asserting_methods_are_virtual_but_non_virtual_methods_are_found_it_should_throw_with_descriptive_message()
        {
            // Arrange
            var methodSelector = new MethodInfoSelector(typeof(ClassWithNonVirtualPublicMethods));

            // Act
            Action act = () =>
                methodSelector.Should().BeVirtual("we want to test the error {0}", "message");

            // Assert
            act.Should().Throw<XunitException>()
                .WithMessage("Expected all selected methods" +
                    " to be virtual because we want to test the error message," +
                    " but the following methods are not virtual:*" +
                    "Void FluentAssertions*ClassWithNonVirtualPublicMethods.PublicDoNothing*" +
                    "Void FluentAssertions*ClassWithNonVirtualPublicMethods.InternalDoNothing*" +
                    "Void FluentAssertions*ClassWithNonVirtualPublicMethods.ProtectedDoNothing");
        }
    }

    public class NotBeVirtual
    {
        [Fact]
        public void When_asserting_methods_are_not_virtual_and_they_are_not_it_should_succeed()
        {
            // Arrange
            var methodSelector = new MethodInfoSelector(typeof(ClassWithNonVirtualPublicMethods));

            // Act
            Action act = () =>
                methodSelector.Should().NotBeVirtual();

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void When_asserting_methods_are_not_virtual_but_virtual_methods_are_found_it_should_throw()
        {
            // Arrange
            var methodSelector = new MethodInfoSelector(typeof(ClassWithAllMethodsVirtual));

            // Act
            Action act = () =>
                methodSelector.Should().NotBeVirtual();

            // Assert
            act.Should().Throw<XunitException>();
        }

        [Fact]
        public void
            When_asserting_methods_are_not_virtual_but_virtual_methods_are_found_it_should_throw_with_descriptive_message()
        {
            // Arrange
            var methodSelector = new MethodInfoSelector(typeof(ClassWithAllMethodsVirtual));

            // Act
            Action act = () =>
                methodSelector.Should().NotBeVirtual("we want to test the error {0}", "message");

            // Assert
            act.Should().Throw<XunitException>()
                .WithMessage("Expected all selected methods" +
                    " not to be virtual because we want to test the error message," +
                    " but the following methods are virtual" +
                    "*ClassWithAllMethodsVirtual.PublicVirtualDoNothing" +
                    "*ClassWithAllMethodsVirtual.InternalVirtualDoNothing" +
                    "*ClassWithAllMethodsVirtual.ProtectedVirtualDoNothing*");
        }
    }

    public class BeDecoratedWith
    {
        [Fact]
        public void When_injecting_a_null_predicate_into_BeDecoratedWith_it_should_throw()
        {
            // Arrange
            var methodSelector = new MethodInfoSelector(typeof(ClassWithAllMethodsDecoratedWithDummyAttribute));

            // Act
            Action act = () =>
                methodSelector.Should().BeDecoratedWith<DummyMethodAttribute>(isMatchingAttributePredicate: null);

            // Assert
            act.Should().ThrowExactly<ArgumentNullException>()
                .WithParameterName("isMatchingAttributePredicate");
        }

        [Fact]
        public void When_asserting_methods_are_decorated_with_attribute_and_they_are_it_should_succeed()
        {
            // Arrange
            var methodSelector = new MethodInfoSelector(typeof(ClassWithAllMethodsDecoratedWithDummyAttribute));

            // Act
            Action act = () =>
                methodSelector.Should().BeDecoratedWith<DummyMethodAttribute>();

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void When_asserting_methods_are_decorated_with_attribute_but_they_are_not_it_should_throw()
        {
            // Arrange
            MethodInfoSelector methodSelector =
                new MethodInfoSelector(typeof(ClassWithMethodsThatAreNotDecoratedWithDummyAttribute))
                    .ThatArePublicOrInternal;

            // Act
            Action act = () =>
                methodSelector.Should().BeDecoratedWith<DummyMethodAttribute>();

            // Assert
            act.Should().Throw<XunitException>();
        }

        [Fact]
        public void
            When_asserting_methods_are_decorated_with_attribute_but_they_are_not_it_should_throw_with_descriptive_message()
        {
            // Arrange
            var methodSelector = new MethodInfoSelector(typeof(ClassWithMethodsThatAreNotDecoratedWithDummyAttribute));

            // Act
            Action act = () =>
                methodSelector.Should().BeDecoratedWith<DummyMethodAttribute>("because we want to test the error {0}", "message");

            // Assert
            act.Should().Throw<XunitException>()
                .WithMessage("Expected all selected methods to be decorated with" +
                    " FluentAssertions*DummyMethodAttribute because we want to test the error message," +
                    " but the following methods are not:*" +
                    "Void FluentAssertions*ClassWithMethodsThatAreNotDecoratedWithDummyAttribute.PublicDoNothing*" +
                    "Void FluentAssertions*ClassWithMethodsThatAreNotDecoratedWithDummyAttribute.ProtectedDoNothing*" +
                    "Void FluentAssertions*ClassWithMethodsThatAreNotDecoratedWithDummyAttribute.PrivateDoNothing");
        }
    }

    public class NotBeDecoratedWith
    {
        [Fact]
        public void When_injecting_a_null_predicate_into_NotBeDecoratedWith_it_should_throw()
        {
            // Arrange
            var methodSelector = new MethodInfoSelector(typeof(ClassWithMethodsThatAreNotDecoratedWithDummyAttribute));

            // Act
            Action act = () =>
                methodSelector.Should().NotBeDecoratedWith<DummyMethodAttribute>(isMatchingAttributePredicate: null);

            // Assert
            act.Should().ThrowExactly<ArgumentNullException>()
                .WithParameterName("isMatchingAttributePredicate");
        }

        [Fact]
        public void When_asserting_methods_are_not_decorated_with_attribute_and_they_are_not_it_should_succeed()
        {
            // Arrange
            var methodSelector = new MethodInfoSelector(typeof(ClassWithMethodsThatAreNotDecoratedWithDummyAttribute));

            // Act
            Action act = () =>
                methodSelector.Should().NotBeDecoratedWith<DummyMethodAttribute>();

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void When_asserting_methods_are_not_decorated_with_attribute_but_they_are_it_should_throw()
        {
            // Arrange
            MethodInfoSelector methodSelector =
                new MethodInfoSelector(typeof(ClassWithAllMethodsDecoratedWithDummyAttribute))
                    .ThatArePublicOrInternal;

            // Act
            Action act = () =>
                methodSelector.Should().NotBeDecoratedWith<DummyMethodAttribute>();

            // Assert
            act.Should().Throw<XunitException>();
        }

        [Fact]
        public void
            When_asserting_methods_are_not_decorated_with_attribute_but_they_are_it_should_throw_with_descriptive_message()
        {
            // Arrange
            var methodSelector = new MethodInfoSelector(typeof(ClassWithAllMethodsDecoratedWithDummyAttribute));

            // Act
            Action act = () => methodSelector.Should()
                    .NotBeDecoratedWith<DummyMethodAttribute>("because we want to test the error {0}", "message");

            // Assert
            act.Should().Throw<XunitException>()
                .WithMessage(
                    "Expected all selected methods to not be decorated*DummyMethodAttribute*because we want to test the error message" +
                    "*ClassWithAllMethodsDecoratedWithDummyAttribute.PublicDoNothing*" +
                    "*ClassWithAllMethodsDecoratedWithDummyAttribute.PublicDoNothingWithSameAttributeTwice*" +
                    "*ClassWithAllMethodsDecoratedWithDummyAttribute.ProtectedDoNothing*" +
                    "*ClassWithAllMethodsDecoratedWithDummyAttribute.PrivateDoNothing");
        }
    }

    public class Be
    {
        [Fact]
        public void When_all_methods_have_specified_accessor_it_should_succeed()
        {
            // Arrange
            var methodSelector = new MethodInfoSelector(typeof(ClassWithPublicMethods));

            // Act
            Action act = () =>
                methodSelector.Should().Be(CSharpAccessModifier.Public);

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void When_not_all_methods_have_specified_accessor_it_should_throw()
        {
            // Arrange
            var methodSelector = new MethodInfoSelector(typeof(GenericClassWithNonPublicMethods<int>));

            // Act
            Action act = () =>
                methodSelector.Should().Be(CSharpAccessModifier.Public);

            // Assert
            act.Should().Throw<XunitException>()
                .WithMessage("Expected all selected methods to be Public" +
                    ", but the following methods are not:*" +
                    "void FluentAssertions*.GenericClassWithNonPublicMethods<int>.PublicDoNothing*" +
                    "void FluentAssertions*.GenericClassWithNonPublicMethods<int>.DoNothingWithParameter*" +
                    "void FluentAssertions*.GenericClassWithNonPublicMethods<int>.DoNothingWithAnotherParameter");
        }

        [Fact]
        public void When_not_all_methods_have_specified_accessor_it_should_throw_with_descriptive_message()
        {
            // Arrange
            var methodSelector = new MethodInfoSelector(typeof(GenericClassWithNonPublicMethods<int>));

            // Act
            Action act = () =>
                methodSelector.Should().Be(CSharpAccessModifier.Public, "we want to test the error {0}", "message");

            // Assert
            act.Should().Throw<XunitException>()
                .WithMessage("Expected all selected methods to be Public" +
                    " because we want to test the error message" +
                    ", but the following methods are not:*" +
                    "Void FluentAssertions*.GenericClassWithNonPublicMethods<int>.PublicDoNothing*" +
                    "Void FluentAssertions*.GenericClassWithNonPublicMethods<int>.DoNothingWithParameter*" +
                    "Void FluentAssertions*.GenericClassWithNonPublicMethods<int>.DoNothingWithAnotherParameter");
        }
    }

    public class NotBe
    {
        [Fact]
        public void When_all_methods_does_not_have_specified_accessor_it_should_succeed()
        {
            // Arrange
            var methodSelector = new MethodInfoSelector(typeof(GenericClassWithNonPublicMethods<string>));

            // Act
            Action act = () =>
                methodSelector.Should().NotBe(CSharpAccessModifier.Public);

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void When_any_method_have_specified_accessor_it_should_throw()
        {
            // Arrange
            var methodSelector = new MethodInfoSelector(typeof(ClassWithPublicMethods));

            // Act
            Action act = () =>
                methodSelector.Should().NotBe(CSharpAccessModifier.Public);

            // Assert
            act.Should().Throw<XunitException>()
                .WithMessage("Expected all selected methods to not be Public" +
                    ", but the following methods are:*" +
                    "Void FluentAssertions*ClassWithPublicMethods.PublicDoNothing*");
        }

        [Fact]
        public void When_any_method_have_specified_accessor_it_should_throw_with_descriptive_message()
        {
            // Arrange
            var methodSelector = new MethodInfoSelector(typeof(ClassWithPublicMethods));

            // Act
            Action act = () =>
                methodSelector.Should().NotBe(CSharpAccessModifier.Public, "we want to test the error {0}", "message");

            // Assert
            act.Should().Throw<XunitException>()
                .WithMessage("Expected all selected methods to not be Public" +
                    " because we want to test the error message" +
                    ", but the following methods are:*" +
                    "Void FluentAssertions*ClassWithPublicMethods.PublicDoNothing*");
        }
    }

    public class BeAsync
    {
        [Fact]
        public void When_asserting_methods_are_async_and_they_are_then_it_succeeds()
        {
            // Arrange
            var methodSelector = new MethodInfoSelector(typeof(ClassWithAllMethodsAsync));

            // Act
            Action act = () => methodSelector.Should().BeAsync();

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void When_asserting_methods_are_async_but_non_async_methods_are_found_it_should_throw_with_descriptive_message()
        {
            // Arrange
            var methodSelector = new MethodInfoSelector(typeof(ClassWithNonAsyncMethods));

            // Act
            Action act = () => methodSelector.Should().BeAsync("we want to test the error {0}", "message");

            // Assert
            act.Should().Throw<XunitException>()
                .WithMessage(
                    """
                    Expected all selected methods to be async because we want to test the error message, but the following methods are not:
                    Task FluentAssertions.Specs.Types.ClassWithNonAsyncMethods.PublicDoNothing
                    Task FluentAssertions.Specs.Types.ClassWithNonAsyncMethods.InternalDoNothing
                    Task FluentAssertions.Specs.Types.ClassWithNonAsyncMethods.ProtectedDoNothing
                    """);
        }
    }

    public class NotBeAsync
    {
        [Fact]
        public void When_asserting_methods_are_not_async_and_they_are_not_then_it_succeeds()
        {
            // Arrange
            var methodSelector = new MethodInfoSelector(typeof(ClassWithNonAsyncMethods));

            // Act
            Action act = () => methodSelector.Should().NotBeAsync();

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void When_asserting_methods_are_not_async_but_async_methods_are_found_it_should_throw_with_descriptive_message()
        {
            // Arrange
            var methodSelector = new MethodInfoSelector(typeof(ClassWithAllMethodsAsync));

            // Act
            Action act = () => methodSelector.Should().NotBeAsync("we want to test the error {0}", "message");

            // Assert
            act.Should().Throw<XunitException>()
                .WithMessage("""
                    Expected all selected methods not to be async because we want to test the error message, but the following methods are:
                    Task FluentAssertions.Specs.Types.ClassWithAllMethodsAsync.PublicAsyncDoNothing
                    Task FluentAssertions.Specs.Types.ClassWithAllMethodsAsync.InternalAsyncDoNothing
                    Task FluentAssertions.Specs.Types.ClassWithAllMethodsAsync.ProtectedAsyncDoNothing
                    """);
        }
    }
}
