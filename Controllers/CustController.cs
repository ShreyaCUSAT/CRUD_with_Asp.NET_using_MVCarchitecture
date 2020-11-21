using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using DataAccess;


namespace MVCform.Controllers
{
    public class CustController : Controller
    {
        SqlConnection con = new SqlConnection(@"Data Source=desktop-6jc2ojm\sqlexpress;Initial Catalog=Customer-Info;Integrated Security=True");

        [HttpGet]
        public ActionResult Index()
        {
            
            CustAccess ca = new CustAccess();
            List<Customer> customers = ca.GetCust.ToList();
            return View(customers);
        }
        [HttpPost]
        public ActionResult AddCustomer(string fullname, string Company, string Country, string Email)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText ="INSERT INTO CUSTOMERS VALUES('" + fullname + "','" + Company + "','" + Country + "','" + Email + "')";
            cmd.ExecuteNonQuery();

            CustAccess ca = new CustAccess();
            List<Customer> customers = ca.GetCust.ToList();
            return RedirectToAction("Index", "Cust");
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //int roll = Convert.ToInt32(rollno);
            cmd.CommandText = "delete from CUSTOMERS where rollno=@rollno";
            SqlParameter param = new SqlParameter("@rollno", id);
            cmd.Parameters.Add(param);
            cmd.ExecuteNonQuery();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ActionName("Edit")]
        public ActionResult Edit(int id)
        {
            
            ViewBag.roll = id;
            CustAccess ca = new CustAccess();
            List<Customer> customers = ca.GetCust.ToList();
            return View(customers);
        }
        
        [HttpPost]
        [ActionName("Update")]
        public ActionResult Update(int id, string full_name, string company, string Country, string email)
        {
            string updateQuery = "Update CUSTOMERS SET Full_Name = @Full_Name,  " +
                    "Company = @Company, Country = @Country, Email = @Email WHERE rollno = @rollno";
            SqlCommand cmd = new SqlCommand(updateQuery, con);
            SqlParameter paramOriginalEmployeeId = new
                SqlParameter("@rollno",id);
            cmd.Parameters.Add(paramOriginalEmployeeId);
            SqlParameter paramName = new SqlParameter("@Full_Name", full_name);
            cmd.Parameters.Add(paramName);
            SqlParameter paramGender = new SqlParameter("@Company", company);
            cmd.Parameters.Add(paramGender);
            SqlParameter paramCity = new SqlParameter("@Country", Country);
            cmd.Parameters.Add(paramCity);
            SqlParameter paramEmail = new SqlParameter("@Email", email);
            cmd.Parameters.Add(paramEmail);
            con.Open();
            cmd.ExecuteNonQuery();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Search(string search)
        {
            ViewBag.srch = search;
            TempData["Srch"] = search;
            CustAccess ca = new CustAccess();
            List<Customer> Searchedcustomers = ca.GetSearchResults(search).ToList();
            return View(Searchedcustomers);
        }
        [HttpGet]
        [ActionName("EditSearch")]
        public ActionResult EditSearch(int id)
        {
            ViewBag.roll = id;
            string s = TempData["Srch"].ToString();
            CustAccess ca = new CustAccess();
            List<Customer> customers = ca.GetSearchResults(s).ToList();
            TempData["Srch"] = s;
            return View(customers);
        }

        [HttpGet]
        [ActionName("SearchCancel")]
        public ActionResult SearchCancel()
        {
            string s = TempData["Srch"].ToString();
            CustAccess ca = new CustAccess();
            List<Customer> Searchedcustomers = ca.GetSearchResults(s).ToList();
            TempData["Srch"] = s;
            return View(Searchedcustomers);
        }

        [HttpPost]
        [ActionName("UpdateSearch")]
        public ActionResult UpdateSearch(int id, string full_name, string company, string Country, string email)
        {
            string updateQuery = "Update CUSTOMERS SET Full_Name = @Full_Name,  " +
                    "Company = @Company, Country = @Country, Email = @Email WHERE rollno = @rollno";
            SqlCommand cmd = new SqlCommand(updateQuery, con);
            SqlParameter paramOriginalEmployeeId = new
                SqlParameter("@rollno", id);
            cmd.Parameters.Add(paramOriginalEmployeeId);
            SqlParameter paramName = new SqlParameter("@Full_Name", full_name);
            cmd.Parameters.Add(paramName);
            SqlParameter paramGender = new SqlParameter("@Company", company);
            cmd.Parameters.Add(paramGender);
            SqlParameter paramCity = new SqlParameter("@Country", Country);
            cmd.Parameters.Add(paramCity);
            SqlParameter paramEmail = new SqlParameter("@Email", email);
            cmd.Parameters.Add(paramEmail);
            con.Open();
            cmd.ExecuteNonQuery();

            return RedirectToAction("SearchCancel");
        }
        [HttpGet]
        [ActionName("DeleteSearch")]
        public ActionResult DeleteSearch(int id)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            //int roll = Convert.ToInt32(rollno);
            cmd.CommandText = "delete from CUSTOMERS where rollno=@rollno";
            SqlParameter param = new SqlParameter("@rollno", id);
            cmd.Parameters.Add(param);
            cmd.ExecuteNonQuery();
            return RedirectToAction("SearchCancel");

        } 
    }
}