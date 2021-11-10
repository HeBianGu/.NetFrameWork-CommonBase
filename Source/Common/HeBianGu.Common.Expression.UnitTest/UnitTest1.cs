using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic;

namespace HeBianGu.Common.Expression.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

        }

        [TestMethod]
        public void TestMethod4()
        {
            var result = ExpressionService.ParseFunc<int>("1+3");

            System.Diagnostics.Debug.WriteLine(result.Invoke());
        }


        [TestMethod]
        public void TestMethod6()
        {
            var result = ExpressionService.ParseFunc<int, int>("1+3+x", new Parameter<int>("x"));

            System.Diagnostics.Debug.WriteLine(result.Invoke(6));
        }

        [TestMethod]
        public void TestMethod7()
        {
            var result = ExpressionService.ParseFunc<int, int, int>("1+3+x*y", new Parameter<int>("x"), new Parameter<int>("y"));

            System.Diagnostics.Debug.WriteLine(1 + 3 + 6 * 7);
            System.Diagnostics.Debug.WriteLine(result.Invoke(6, 7));
        }

        [TestMethod]
        public void TestMethod8()
        {
            var result = ExpressionService.ParseFunc<double, double, double, double>("1+3+x*y*z", new Parameter<double>("x"), new Parameter<double>("y"), new Parameter<double>("z"));

            Assert.AreEqual(1 + 3 + 6 * 7 * 6, result.Invoke(6, 7, 6));
        }


        [TestMethod]
        public void TestMethod9()
        {
            var result = ExpressionService.ParseLambda<int, int>("1+3+x*y*z", new Parameter<int>("x"), new Parameter<int>("y"), new Parameter<int>("z"));

            Assert.AreEqual(1 + 3 + 6 * 7 * 6, result.Compile().DynamicInvoke(6, 7, 6));
        }

        [TestMethod]
        public void TestMethod10()
        {
            var result = ExpressionService.ParseLambdaLambdaExpression<double>("1+3+x*y*z", new Parameter<double>("x"), new Parameter<int>("y"), new Parameter<int>("z"));

            Assert.AreEqual(1 + 3 + 6 * 7 * 6.0, result.Compile().DynamicInvoke(6, 7, 6));
        }

        [TestMethod]
        public void TestMethod11()
        {
            var result = ExpressionService.ParseFunc<double, double, double>("Math.Pow(x, y) + 5", new Parameter<double>("x"), new Parameter<double>("y"));

            Assert.AreEqual(Math.Pow(6, 7) + 5, result.Invoke(6, 7));
        }


        [TestMethod]
        public void TestMethod12()
        {
            var result = ExpressionService.ParseFunc<string, int>("arg.Length", new Parameter<string>("arg"));

            Assert.AreEqual(8, result.Invoke("hebiangu"));
        }

        [TestMethod]
        public void TestMethod13()
        {
            var result = ExpressionService.ParseFunc<string, string, int>("arg1.Length + arg2.Length", new Parameter<string>("arg1"), new Parameter<string>("arg2"));

            Assert.AreEqual(16, result.Invoke("hebiangu", "hebiangu"));
        }

        /// <summary>
        /// ��Ĭ�ϲ���
        /// </summary>
        [TestMethod]
        public void TestMethod14()
        {
            var result = ExpressionService.ParseDelegate<Func<double, double>>("arg+1+3");

            Assert.AreEqual(1 + 3 + 6, result.Invoke(6));
        }

        /// <summary>
        /// ��Ĭ�ϲ���
        /// </summary>
        [TestMethod]
        public void TestMethod15()
        {
            var result = ExpressionService.ParseDelegate<Func<string>>("(22).ToString()");

            Assert.AreEqual("22", result.Invoke());
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        [TestMethod]
        public void TestMethod16()
        {
            List<string> array = new List<string>();

            array.Add("1111");
            array.Add("2222");

            //var result = ExpressionService.ParseDelegate<Func<List<string>,string>>("array[0]");

            var result = ExpressionService.ParseFunc<List<string>, string>("array[1]", new Parameter<List<string>>("array"));

            var r = result.Invoke(array);

            System.Diagnostics.Debug.WriteLine(r); 

            Assert.AreEqual("array", result.Invoke(array));
        }


        /// <summary>
        /// ��ȡ����
        /// </summary>
        [TestMethod]
        public void TestMethod17()
        {
            List<string> arr = new List<string>();

            arr.Add("1111");
            arr.Add("2222"); 

            var func = ExpressionService.ParseLambda("array[0]", typeof(string), System.Linq.Expressions.Expression.Parameter(typeof(List<string>), "array"));

            var r = func.Compile().DynamicInvoke(arr);

            System.Diagnostics.Debug.WriteLine(r);
        }

    }
}
