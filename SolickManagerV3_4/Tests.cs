using NUnit.Framework;
using SolickManagerV3_4.Pages;
using SolickManagerV3_4.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolickManagerV3_4
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DBNotNull()
        {
            var DbInstance = DB.Instance;

            Assert.AreNotSame(DbInstance, null);
        }
        [Test]
        public void WorkersNotNull()
        {
            var workers = DB.Instance.Workers.ToList();

            CollectionAssert.AllItemsAreNotNull(workers);
        }

        [Test]
        public void SuccessAddProductInOtherFunctionsListProducts()
        {
            var oldProducts = OtherFunctons.Products.ToList();

            OtherFunctons.Instance.AddProduct(new DTO.Product() { Model = "Тест" });

            var NewProducts = OtherFunctons.Products.ToList();



            CollectionAssert.AreNotEqual(oldProducts, NewProducts);
        }
        [Test]
        public void SuccessRemoveProductInOtherFunctionsListProducts()
        {
            OtherFunctons.Instance.AddProduct(new DTO.Product() { Model = "Тест" });
            var oldProducts = OtherFunctons.Products.ToList();

            OtherFunctons.Instance.RemoveProduct(OtherFunctons.Products.First());

            var NewProducts = OtherFunctons.Products.ToList();



            CollectionAssert.AreNotEqual(oldProducts, NewProducts);
        }
        [Test]
        public void SuccessAddProductInGroupInOtherFunctionsListAssemblyProducts()
        {
            var oldProducts = OtherFunctons.AssemblyProducts.ToList();

            OtherFunctons.Instance.AddProductInGroup(new DTO.Product() { Model = "Тест" });

            var NewProducts = OtherFunctons.AssemblyProducts.ToList();


            CollectionAssert.AreNotEqual(oldProducts, NewProducts);
        }


        [Test]
        public void ListServiceNotNullInApplication()
        {
            var Application = DB.Instance.Applications.FirstOrDefault(s => s.Id == 11);

            var Services = Application.ListService;

            CollectionAssert.AreNotEqual(Services, null);
        }
        [Test]
        public void ListProductNotNullInAssembly()
        {
            var Assembly = DB.Instance.Assemblies.FirstOrDefault(s => s.Id == 3);

            var Products = Assembly.Products;

            CollectionAssert.AreNotEqual(Products, null);
        }

        [Test]
        public void AllPriceService0WhereIdApplication11()
        {
            var AllPriceService = DB.Instance.Applications.FirstOrDefault(s  => s.Id == 11).AllPriceService;

            Assert.AreEqual(AllPriceService, 0);
        }
        [Test]
        public void ProductView_result_WhereIdProduct_25()
        {
            var ProductView = DB.Instance.Products.FirstOrDefault(s => s.Id == 25).ProductView;
            var result = "HYPERX FURY HX316C10FB/4 900,00руб.";

            Assert.AreEqual(ProductView, result);

        }
        [Test]
        public void ProviderTitle_result_WhereShipmentId_15()
        {
            var ProviderTitle = DB.Instance.Shipments.FirstOrDefault(s => s.Id == 15).ProviderTitle;
            var result = "Продавец с Farpost";

            Assert.AreEqual(ProviderTitle, result);
        }
    }
}
