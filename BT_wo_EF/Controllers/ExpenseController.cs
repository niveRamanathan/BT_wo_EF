using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using BT_wo_EF.Models;
using System.Collections;

namespace BT_wo_EF.Controllers
{
    public class ExpenseController : Controller
    {
        string connectionString = @"Data Source = SYSLP930; Initial Catalog = BT; Integrated Security= True";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtblExpenseT = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("AmountViewAll", sqlCon);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.Fill(dtblExpenseT);
            }
            return View(dtblExpenseT);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new ExpenseModel());
        }

        // POST: Expense/Create
        [HttpPost]
        public ActionResult Create(ExpenseModel expenseModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                //sql query//string query = "INSERT INTO ExpenseT VALUES(@Title, @Type, @Amount)";
                SqlCommand sqlCmd = new SqlCommand("AmountAdd", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@Id", expenseModel.Id);
                sqlCmd.Parameters.AddWithValue("@Title", expenseModel.Title);
                sqlCmd.Parameters.AddWithValue("@Type", expenseModel.Type);
                sqlCmd.Parameters.AddWithValue("@Amount", expenseModel.Amount);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Expense/Edit/5
        public ActionResult Edit(int id)
        {
            ExpenseModel expenseModel = new ExpenseModel();
            DataTable dtblExpenseT = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("AmountViewById", sqlCon);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("@Id", id);
                sqlDa.Fill(dtblExpenseT);
            }
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
                if (dtblExpenseT.Rows.Count == 1)
                {
                    sqlCon.Open();
                    SqlDataAdapter sqlDa = new SqlDataAdapter("AmountEdit", sqlCon);
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    expenseModel.Id = Convert.ToInt32(dtblExpenseT.Rows[0][0].ToString());
                    expenseModel.Title = dtblExpenseT.Rows[0][1].ToString();
                    expenseModel.Type = dtblExpenseT.Rows[0][2].ToString();
                    expenseModel.Amount = Convert.ToInt32(dtblExpenseT.Rows[0][3].ToString());
                    return View(expenseModel);
                }
                else
                    return RedirectToAction("Index");
        }
        // POST: Expense/Edit/5
        [HttpPost]
        public ActionResult Edit(ExpenseModel expenseModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "UPDATE ExpenseT SET Title=@Title, Type=@Type, Amount=@Amount WHERE Id=@Id";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Id", expenseModel.Id);
                sqlCmd.Parameters.AddWithValue("@Title", expenseModel.Title);
                sqlCmd.Parameters.AddWithValue("@Type", expenseModel.Type);
                sqlCmd.Parameters.AddWithValue("@Amount", expenseModel.Amount);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Expense/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                //string query = "DELETE FROM ExpenseT WHERE Id=@Id";
                SqlCommand sqlCmd = new SqlCommand("AmountDelete", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("Id", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

    }
}

