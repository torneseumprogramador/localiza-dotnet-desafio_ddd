using System;
using NUnit.Framework;
using System.Data;
using Infrastructure.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Infrastructure.Database
{
    public class Customer
    {
        [Key]
        [Column("id")]
        public int Id {get;set;}

        [Column("name")]
        public string Name { get;set;}

        [Column("phone")]
        public string Phone { get;set;}
    }
    
    public class MapTableTest
    {
        [Test]
        public void BuldGenericMapInsert()
        {
            var sql = MapTable.BuilderInsert(new Customer(){ Name = "Danilo" });
            Assert.AreEqual(sql, "insert into customers (name) values (@name) SELECT SCOPE_IDENTITY()");
        }

        [Test]
        public void BuldGenericMapDelete()
        {
            var sql = MapTable.BuilderDelete(new Customer(){ Id = 1 });
            Assert.AreEqual(sql, "delete from customers where id=1");
        }

        [Test]
        public void BuldGenericMapSelect()
        {
            var sql = MapTable.BuilderSelect<Customer>();
            Assert.AreEqual(sql, "select customers.* from customers");
        }

        [Test]
        public void BuldGenericMapSelectWhere()
        {
            var sql = MapTable.BuilderSelect<Customer>("where nome like '%Danilo%'");
            Assert.AreEqual(sql, "select customers.* from customers where nome like '%Danilo%'");
        }

        [Test]
        public void BuldGenericMapUpdate()
        {
            var sql = MapTable.BuilderUpdate(new Customer(){ Id = 1, Name = "Danilo" });
            Assert.AreEqual(sql, "update customers set name=@name where id=@id");
        }

        [Test]
        public void BuldGenericMapParameter()
        {
            var parameters = MapTable.BuilderParameters(new Customer(){ Name = "Danilo", Phone = "(11)99999-9999" });
            Assert.AreEqual(parameters.Count, 2);

            Assert.AreEqual(parameters[0].ParameterName, "@name");
            Assert.AreEqual(parameters[0].SqlDbType, SqlDbType.VarChar);

            Assert.AreEqual(parameters[1].ParameterName, "@phone");
            Assert.AreEqual(parameters[1].SqlDbType, SqlDbType.VarChar);
        }

        [Test]
        public void BuldGenericMapParameterNull()
        {
            var parameters = MapTable.BuilderParameters(new Customer(){ Name = "Danilo", Phone = null });
            Assert.AreEqual(parameters.Count, 1);

            Assert.AreEqual(parameters[0].ParameterName, "@name");
            Assert.AreEqual(parameters[0].SqlDbType, SqlDbType.VarChar);
        }

        [Test]
        public void CreateTable()
        {
            var sql = MapTable.CreateTable<Customer>();
            Assert.AreEqual(sql, "IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[customers]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)DROP TABLE customers;CREATE TABLE customers(id  int  IDENTITY(1,1) NOT NULL, name  varchar(255), phone  varchar(255), )");
        }

    }
}