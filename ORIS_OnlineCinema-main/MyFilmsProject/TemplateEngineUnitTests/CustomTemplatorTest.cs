﻿using MyHTTPServer.Temlator;
using MyHTTPServer.models;
using TemplateEngineUnitTests.Models;

namespace MyHttpServer.unitTest.Core.Templator;

[TestClass]
public class CustomTemplatorTest
{
    [TestMethod]
    public void GetHtmlByTemplate_When_NameIsNotNull_ResultSuccess()
    {
        // Arrange
        ICustomTemlator customTemplator = new CustomTemplator();
        string template = "<label>name</label><p>{name}</p>";
        string name = "Almaz";
        // Act
        var result = customTemplator.GetHtmlByTemplate(template, name);
        // Assert
        Assert.AreEqual("<label>name</label><p>Bulat</p>", result);
    }
    
    [TestMethod]
    public void GetHtmlByTemplate_When_ObjectIsnotNull_ResultSuccess()
    {
        // Arrange
        ICustomTemlator customTemplator = new CustomTemplator();
        string template = "Привет {Name}! <p>Ваш логин: {Login}; Ваш пароль: {Password}; Мы очень рады с вами познакомится дорогой {Name}!</p>";
        var person = new Person
        {
            Login = "test@test.ru",
            Password = "passWord",
            Name = "Иван"
        };

        // Act
        var result = customTemplator.GetHtmlByTemplate<Person>(template, person);

        // Assert
        Assert.AreEqual("Привет Иван! <p>Ваш логин: test@test.ru; Ваш пароль: passWord; Мы очень рады с вами познакомится дорогой Иван!</p>", result);
    }
    
}