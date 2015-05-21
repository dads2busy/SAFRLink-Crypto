using EDU.VT.IPG.VTIntegrate.SchroederPadAndChaff;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace VTIntegrate.CypherStoredProceduresTest
{
    
    
    /// <summary>
    ///This is a test class for UserDefinedFunctionsTest and is intended
    ///to contain all UserDefinedFunctionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UserDefinedFunctionsTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        [TestMethod()]
        public void VT_Cypher_Person_NameTest1()
        {
            string key = "QS6F0JEFTHQB7MGQJHPHPR0WF6XO563232HR";
            string personName = "NUÑEZ-CABALLERO";
            string expected = "3CJJPLEGTS1FO0";
            string actual;
            actual = UserDefinedFunctions.VT_Cypher_Person_Name(key, personName);
            Assert.AreEqual(expected, actual);

            // NUNEZ-CABALLERO should result in same cypher as NUÑEZ-CABALLERO
            personName = "NUNEZ-CABALLERO";
            actual = UserDefinedFunctions.VT_Cypher_Person_Name(key, personName);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void VT_Cypher_Person_NameTest2()
        {
            string key = "QS6F0JEFTHQB7MGQJHPHPR0WF6XO563232HR";
            string personName = "NU±EZ-CABALLERO";
            string expected = "3CA42JFF4SUSL";
            string actual;
            actual = UserDefinedFunctions.VT_Cypher_Person_Name(key, personName);
            Assert.AreEqual(expected, actual);

            // NUEZ-CABALLERO should result in same cypher as NU±EZ-CABALLERO
            personName = "NUEZ-CABALLERO";
            actual = UserDefinedFunctions.VT_Cypher_Person_Name(key, personName);
            Assert.AreEqual(expected, actual);

            // NUªEZ-CABALLERO should result in same cypher as well
            //char superA = 'ª';
            //Console.WriteLine(CharUnicodeInfo.GetUnicodeCategory(superA));
            personName = "NUªEZ-CABALLERO";
            actual = UserDefinedFunctions.VT_Cypher_Person_Name(key, personName);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void VT_Cypher_Person_NameTest3()
        {
            string key = "QS6F0JEFTHQB7MGQJHPHPR0WF6XO563232HR";
            string personName = "SHARNß";
            string expected = "8Z6WD";
            string actual;
            actual = UserDefinedFunctions.VT_Cypher_Person_Name(key, personName);
            Assert.AreEqual(expected, actual);

            // SHARN should result in same cypher as SHARNß
            personName = "SHARN";
            actual = UserDefinedFunctions.VT_Cypher_Person_Name(key, personName);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void VT_Cypher_Collision_Test()
        {
            string key = "C1HS6JV7FULGCYP32KJ1Q7DMODXKBOB7W3WK";
            string actual1 = UserDefinedFunctions.VT_Cypher_String(key, "6B000018Q");
            string actual2 = UserDefinedFunctions.VT_Cypher_String(key, "6B00001K0");
            string actual3 = UserDefinedFunctions.VT_Cypher_String(key, "6B00001Y0");

            Assert.AreNotEqual(actual1, actual2);
            Assert.AreNotEqual(actual1, actual3);
        }
    }
}
